using UnityEngine;
using UnityEngine.AI;

public class AIEnemyStateController : MonoBehaviour
{

    [Tooltip("Distance between Player and Enemy at which the enemy will start charging the player.")]
    public float ChargingDistance;
    [Tooltip("Indicates whether the enemy uses ranged attacks.")]
    public bool isRangedAttack;
    [Tooltip("Indicates whether the enemy uses melee attacks.")]
    public bool isMeleeAttack;

    public enum State { Patrol, Charge, Attack, Dead };
    public State state;

    private UnityStandardAssets.Characters.ThirdPerson.AICharacterControl ChargingScript;
    private AIEnemyPatrol PatrollingScript;
    private NavMeshAgent Agent;
    private AIEnemyAttack AttackScript;
    private Rigidbody m_Rigidbody;
    private GameObject player;

    private Transform playerTransform;
    private float lastAttackTime = 0;

    Animator m_Animator;

    void Start()
    {
        ChargingScript = GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();
        PatrollingScript = GetComponent<AIEnemyPatrol>();
        player = GameObject.FindWithTag("Player");
        playerTransform = player.transform;
        ChargingScript.target = playerTransform;
        Agent = GetComponent<NavMeshAgent>();
        AttackScript = GetComponent<AIEnemyAttack>();

        changeStateToPatrol();

        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Move from Patrol state to Charge state.
        Vector3 DistanceBetweenEnemyAndPlayer = playerTransform.position - transform.position;
        float DistanceBetweenEnemyAndPlayerMagnitude = DistanceBetweenEnemyAndPlayer.sqrMagnitude;
        if ((DistanceBetweenEnemyAndPlayerMagnitude <= ChargingDistance) && state == State.Patrol && !PlayerController.Instance.isDead())
        {
            changeStateToCharge();
        }

        // Move from Charge state to Attack state.
        if (canAttack())
        {
            if (DistanceBetweenEnemyAndPlayerMagnitude <= (Agent.stoppingDistance + 0.01f) && isMeleeAttack)
            {
                changeStateToMeleeAttack();
            }
            else
            {
                if (DistanceBetweenEnemyAndPlayerMagnitude <= AttackScript.rangedAttackDistance && isRangedAttack)
                {
                    changeStateToRangedAttack();
                }
            }
        }

        if (state == State.Charge && !PlayerController.Instance.isDead())
        {
            if (Agent.velocity == Vector3.zero)
            {
                m_Animator.SetTrigger("enemyIdleAnimation");
                m_Animator.ResetTrigger("enemyRunningAnimation");
            }
            else
            {
                print(DistanceBetweenEnemyAndPlayerMagnitude);
                m_Animator.SetTrigger("enemyRunningAnimation");
                m_Animator.ResetTrigger("enemyIdleAnimation");
            }
        }
    }

    bool canAttack()
    {
        return state == State.Charge && !PlayerController.Instance.isDead() && (System.Math.Abs(lastAttackTime) < 0.1 || Time.time >= lastAttackTime + AttackScript.AttackCooldownInSecs * 60 * Time.deltaTime);
    }

    void changeStateToCharge()
    {
        PatrollingScript.enabled = false;
        ChargingScript.enabled = true;
        state = State.Charge;
    }

    void changeStateToPatrol()
    {
        PatrollingScript.enabled = true;
        ChargingScript.enabled = false;
        state = State.Patrol;
    }

    void changeStateToMeleeAttack()
    {
        PatrollingScript.enabled = false;
        ChargingScript.enabled = false;
        m_Animator.SetTrigger("enemyAttackAnimation");
        m_Animator.ResetTrigger("enemyRunningAnimation");
        m_Animator.ResetTrigger("enemyIdleAnimation");
        state = State.Attack;
        AttackScript.Attack(playerTransform);
        lastAttackTime = Time.time;
        if (PlayerController.Instance.isDead())
            changeStateToPatrol();
        else
            changeStateToCharge();
    }

    void changeStateToRangedAttack()
    {
        PatrollingScript.enabled = false;
        ChargingScript.enabled = false;
        m_Animator.SetTrigger("enemyAttackAnimation");
        m_Animator.ResetTrigger("enemyRunningAnimation");
        m_Animator.ResetTrigger("enemyIdleAnimation");
        state = State.Attack;
        // AttackScript.attack(playerTransform);
        AttackScript.RangedAttack(playerTransform);
        lastAttackTime = Time.time;
        if (PlayerController.Instance.isDead())
            changeStateToPatrol();
        else
            changeStateToCharge();
    }

    public void changeStateToDead()
    {
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

    void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }
}
