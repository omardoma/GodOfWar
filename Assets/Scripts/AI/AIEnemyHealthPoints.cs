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



    public void EnemyHit(float damage)
    {
        if(health>0)
        {

            health -= (int)damage;
            if (health > 0)
            {
                m_Animator.SetTrigger("enemyHitAnimation");
            }else
            {
                stateController.changeStateToDead();
            }

        }

    }
}
