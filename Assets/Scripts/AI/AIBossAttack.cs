using UnityEngine;

public class AIBossAttack : MonoBehaviour
{
    public float attackValue;
    public int AttackCooldownInSecs = 5;
    public float rangedAttackDistance;

    public void Attack(GameObject weakPoint = null)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                var damage = weakPoint == null ? attackValue : weakPoint.GetComponent<AIBossWeakPoint>().attackValue;
                Debug.Log("Attack happened");
                hit.collider.gameObject.GetComponent<PlayerController>().KratosGotHit();
                //hit.collider.gameObject.GetComponent<HealthPoints>().health -= damage;
                //Start player getting hit animation
            }
        }
    }
}
