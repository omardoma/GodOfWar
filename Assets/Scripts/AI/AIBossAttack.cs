using UnityEngine;

public class AIBossAttack : MonoBehaviour
{
    public float attackValue;
    public int AttackCooldownInSecs = 5;
    public float rangedAttackDistance;

    public void Attack(Transform target, GameObject weakPoint = null)
    {
        RotateTowards(target);

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

    private void RotateTowards(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 180);
    }
}
