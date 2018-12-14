using UnityEngine;
using System.Collections;

public class AIEnemyHealthPoints : MonoBehaviour
{
    public int health;

    private AIEnemyStateController stateController;
    Animator m_Animator;

    void Start()
    {
        stateController = GetComponent<AIEnemyStateController>();
        m_Animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (health == 0) {
            stateController.changeStateToDead();
        }
    }

    public void EnemyHit(float damage)
    {
        if(health>0)
        {
            health -= (int)damage;
            m_Animator.ResetTrigger("enemyIdleAnimation");
            m_Animator.ResetTrigger("enemyRunningAnimation");
            m_Animator.ResetTrigger("enemyAttackAnimation");
            m_Animator.ResetTrigger("enemyDieAnimation");
            m_Animator.SetTrigger("enemyHitAnimation");
        }

    }
}
