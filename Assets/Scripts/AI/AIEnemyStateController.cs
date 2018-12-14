using UnityEngine;
using UnityEngine.AI;

public class AIEnemyStateController : MonoBehaviour {

    [Tooltip("Distance between Player and Enemy at which the enemy will start charging the player.")]
    public float ChargingDistance;
    [Tooltip("Indicates whether the enemy uses ranged attacks.")]
    public bool isRangedAttack;
    [Tooltip("Indicates whether the enemy uses melee attacks.")]
    public bool isMeleeAttack;

    private enum State { Patrol, Charge, Attack, Dead };
    private State state;
        
    private UnityStandardAssets.Characters.ThirdPerson.AICharacterControl ChargingScript;
    private AIEnemyPatrol PatrollingScript;
    private NavMeshAgent Agent;
    private AIEnemyAttack AttackScript;
    private Rigidbody m_Rigidbody;

    private Transform playerTransform;
    private float lastAttackTime = 0;

    Animator m_Animator;

    void Start () {
        ChargingScript = GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();
        PatrollingScript = GetComponent<AIEnemyPatrol>();
        playerTransform = GameObject.FindWithTag("Player").transform;
        Agent = GetComponent<NavMeshAgent>();
        AttackScript = GetComponent<AIEnemyAttack>();

        changeStateToPatrol();

        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }
	
	void Update () {
        // Move from Patrol state to Charge state.
        Vector3 DistanceBetweenEnemyAndPlayer = playerTransform.position - transform.position;
        float DistanceBetweenEnemyAndPlayerMagnitude = DistanceBetweenEnemyAndPlayer.sqrMagnitude;
        if ((DistanceBetweenEnemyAndPlayerMagnitude <= ChargingDistance) && state == State.Patrol) {
            changeStateToCharge();
        }

        // Move from Charge state to Attack state.
        if (canAttack()) {
            if (DistanceBetweenEnemyAndPlayerMagnitude <= Agent.stoppingDistance && isMeleeAttack ) {
                changeStateToMeleeAttack();
            } else {
                if (DistanceBetweenEnemyAndPlayerMagnitude <= AttackScript.rangedAttackDistance && isRangedAttack) {
                    changeStateToRangedAttack();
                }
            }
        }

        if (state == State.Charge)
        {
            if (DistanceBetweenEnemyAndPlayerMagnitude <= Agent.stoppingDistance)
            {
                m_Animator.SetTrigger("enemyIdleAnimation");
                m_Animator.ResetTrigger("enemyRunningAnimation");
            }
            else
            {
                m_Animator.SetTrigger("enemyRunningAnimation");
                m_Animator.ResetTrigger("enemyIdleAnimation");
            }
        }
    }

    bool canAttack(){
        return state == State.Charge && (System.Math.Abs(lastAttackTime) < 0.1 ? true : Time.time >= lastAttackTime + AttackScript.AttackCooldownInSecs * 60 * Time.deltaTime);
    }

    void changeStateToCharge() {
        PatrollingScript.enabled = false;
        ChargingScript.enabled = true;
        state = State.Charge;
    }

    void changeStateToPatrol() {
        PatrollingScript.enabled = true;
        ChargingScript.enabled = false;
        state = State.Patrol;
    }

    void changeStateToMeleeAttack() {
        PatrollingScript.enabled = false;
        ChargingScript.enabled = false;
        m_Animator.SetTrigger("enemyAttackAnimation");
        m_Animator.ResetTrigger("enemyRunningAnimation");
        m_Animator.ResetTrigger("enemyIdleAnimation");
        state = State.Attack;
        AttackScript.Attack();
        lastAttackTime = Time.time;
        changeStateToCharge();
    }

    void changeStateToRangedAttack(){
        PatrollingScript.enabled = false;
        ChargingScript.enabled = false;
        m_Animator.SetTrigger("enemyAttackAnimation");
        m_Animator.ResetTrigger("enemyRunningAnimation");
        m_Animator.ResetTrigger("enemyIdleAnimation");
        state = State.Attack;
        // AttackScript.attack();
        AttackScript.RangedAttack();
        lastAttackTime = Time.time;
        changeStateToCharge();
    }

    public void changeStateToDead(){
        Invoke("DestroyEnemy", 5);
        PatrollingScript.enabled = false;
        ChargingScript.enabled = false;
        state = State.Dead;
        m_Rigidbody.constraints = RigidbodyConstraints.FreezePosition;
        m_Animator.ResetTrigger("enemyIdleAnimation");
        m_Animator.ResetTrigger("enemyRunningAnimation");
        m_Animator.ResetTrigger("enemyAttackAnimation");
        m_Animator.SetTrigger("enemyDieAnimation");
    }

    void DestroyEnemy(){
        Destroy(this.gameObject);
    }
}
