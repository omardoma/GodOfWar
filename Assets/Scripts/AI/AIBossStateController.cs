using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class AIBossStateController : MonoBehaviour {

    [Tooltip("Distance between Player and Enemy at which the enemy will start charging the player.")]
    public float ChargingDistance;
    [Tooltip("Indicates whether the enemy uses ranged attacks.")]
    public bool isRangedAttack = false;
    [Tooltip("Indicates whether the enemy uses melee attacks.")]
    public bool isMeleeAttack = true;

    private enum State { Patrol, Charge, Attack, Dead };
    private State state;

    List<GameObject> weakPoints = new List<GameObject>();

    private UnityStandardAssets.Characters.ThirdPerson.AICharacterControl ChargingScript;
    private AIBossPatrol PatrollingScript;
    private NavMeshAgent Agent;
    private AIBossAttack AttackScript;
    private Rigidbody m_Rigidbody;

    private bool stunned = false;
    private Vector3 stunPosition;

    private Transform playerTransform;
    private float lastAttackTime = 0;

    Animator m_Animator;

    void Start () {
        ChargingScript = GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();
        PatrollingScript = GetComponent<AIBossPatrol>();
        playerTransform = GameObject.FindWithTag("Player").transform;
        Agent = GetComponent<NavMeshAgent>();
        AttackScript = GetComponent<AIBossAttack>();

        ChangeStateToPatrol();

        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    public void RegisterWeakPoint(GameObject weakPoint)
    {
        weakPoints.Add(weakPoint);
    }

    void Update () {
        // Move from Patrol state to Charge state.
        Vector3 DistanceBetweenEnemyAndPlayer = playerTransform.position - transform.position;
        float DistanceBetweenEnemyAndPlayerMagnitude = DistanceBetweenEnemyAndPlayer.sqrMagnitude;
        if ((DistanceBetweenEnemyAndPlayerMagnitude <= ChargingDistance) && state == State.Patrol) {
            ChangeStateToCharge();
        }

        // Move from Charge state to Attack state.
        if (canAttack()) {
            if (DistanceBetweenEnemyAndPlayerMagnitude <= Agent.stoppingDistance && isMeleeAttack ) {
                ChangeStateToAttack();
            }
        }

        if (state == State.Charge && !stunned)
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

        if (stunned) {
            transform.position = stunPosition;
        }
    }

    bool canAttack(){
        return !stunned && state == State.Charge && (System.Math.Abs(lastAttackTime) < 0.1 ? true : Time.time >= lastAttackTime + AttackScript.AttackCooldownInSecs * 60 * Time.deltaTime);
    }

    void ChangeStateToCharge() {
        PatrollingScript.enabled = false;
        ChargingScript.enabled = true;
        state = State.Charge;
    }

    void ChangeStateToPatrol() {
        PatrollingScript.enabled = true;
        ChargingScript.enabled = false;
        state = State.Patrol;
    }

    void ChangeStateToAttack() {
        PatrollingScript.enabled = false;
        ChargingScript.enabled = false;
        m_Animator.ResetTrigger("enemyRunningAnimation");
        m_Animator.ResetTrigger("enemyIdleAnimation");
        state = State.Attack;
        int attackIndex = Random.Range(0, weakPoints.Count + 2);
        if (attackIndex == weakPoints.Count)
        {
            m_Animator.SetTrigger("enemyAttackAnimation");
            AttackScript.Attack();
            Debug.Log("normal attack");
        }
        else {
            if (attackIndex == weakPoints.Count + 1 ){
                if (AttackScript.AttackCooldownInSecs != 1){
                    // Rage Mode
                    Debug.Log("Rage Mode yabaaaa");
                    m_Animator.SetTrigger("enemyRageAnimation");
                    AttackScript.AttackCooldownInSecs = 1;
                    Invoke("ExitRageMode", 7);
                }
            }
            else 
            {
                GameObject weakPoint = weakPoints[attackIndex];
                //Shaghal animation elattack depending 3la no3 elattack
                m_Animator.SetTrigger(weakPoint.GetComponent<AIBossWeakPoint>().attackAnimationTrigger);
                AttackScript.Attack(weakPoint);
                Debug.Log("abbbbbnormal attack");
            }
        }
        
        lastAttackTime = Time.time;
        ChangeStateToCharge();
    }

    public void ChangeStateToDead(){
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

    public void RemoveWeakPoint(GameObject weakPoint)
    {   
        weakPoints.Remove(weakPoint);
        stunned = true;
        stunPosition = transform.position;
        m_Animator.ResetTrigger("enemyAttackAnimation");
        m_Animator.ResetTrigger("enemyRunningAnimation");
        m_Animator.SetTrigger("enemyIdleAnimation");
        Invoke("ExitStunMode", 7);
    }

    private void ExitStunMode()
    {
        stunned = false;
        m_Animator.ResetTrigger("enemyIdleAnimation");
        m_Animator.SetTrigger("enemyRunningAnimation");
    }

    private void ExitRageMode()
    {
        AttackScript.AttackCooldownInSecs = 5;
    }
}
