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
        m_Animator = GetComponent<Animator>();
    }

    public void EnemyHit(float damage)
    {
        if (health > 0)
        {

            health -= (int)damage;
            if (health > 0)
            {
                m_Animator.SetTrigger("enemyHitAnimation");
            }
            else
            {
                stateController.ChangeStateToDead();
            }

        }

    }
}
