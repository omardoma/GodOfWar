using UnityEngine;

public class AIEnemyAttack : MonoBehaviour
{
    public float attackValue;
    public int AttackCooldownInSecs;
    public float rangedAttackDistance;

    public GameObject SpiderAttack;

    public void Attack(Transform target)
    {
        RotateTowards(target);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                hit.collider.gameObject.GetComponent<PlayerController>().KratosGotHit();
                // call el function beta3et 2l hit (blocking wala 3adi ? law 3adi yena2as el health w yesha8al el animation)
                //hit.collider.gameObject.GetComponent<HealthPoints>().health -= attackValue;
                //Start player getting hit animation
            }
        }
    }

    public void RangedAttack(Transform target)
    {

        RotateTowards(target);
        //hate3mel object w te directoh nahyet kratos
        //elobject da 3aleh already script, bytedect on collision
        //gowa elcollision detect

        Vector3 position = new Vector3(this.gameObject.transform.position.x, 0.5f, gameObject.transform.position.z);
        GameObject projectile = Instantiate(SpiderAttack, position, Quaternion.identity);
        //projectile.AddComponent<Rigidbody>();
        projectile.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
        projectile.GetComponent<Rigidbody>().useGravity = false;

    }


    private void RotateTowards(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 180);
    }
}
