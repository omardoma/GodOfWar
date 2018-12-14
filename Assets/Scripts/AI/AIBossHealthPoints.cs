using UnityEngine;
using System.Collections;

public class AIBossHealthPoints : MonoBehaviour
{
    public int health = 200;
    Animator m_Animator;
    private AIBossStateController stateController;

    void Start()
    {
        stateController = GetComponent<AIBossStateController>();
    }

    void Update()
    {
        if (health == 0) {
            stateController.ChangeStateToDead();
        }
    }

    public void EnemyHit(float damage)
    {
        if (health > 0)
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
