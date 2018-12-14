using UnityEngine;

public class DaggerScript : MonoBehaviour
{
    private static DaggerScript instance;
    private GameObject enemy;
    public int attackDoneType;
    float lightDamage, heavyDamage;

    public static DaggerScript Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        //DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        attackDoneType = 0;
    }

    private void Update()
    {

    }

    ////when kratos hits an enemy:
    //private void OnCollisionEnter(Collision other)
    //{

    //    //get the light damage and heavy damage to be passsed to the enemies:
    //    lightDamage = KratosController.Instance.GetLightDamage();
    //    heavyDamage = KratosController.Instance.GetHeavyDamage();

    //    enemy = other.gameObject;
    //    if (enemy.CompareTag("Foe"))
    //    {
    //        if (attackDoneType != 0)  //the collision was as a result of an attack
    //        {
    //            Debug.Log("youmna abyad");
    //            //Increase rage
    //            KratosController.Instance.SetRage(KratosController.Instance.GetRage() + 1);

    //            //check if kratos killed the bos:
    //            /*if (enemy.tag == ("Boss"))
    //            {
    //                if (attackDoneType == 1)
    //                    //enemy.GetComponent<EnemyScript>().EnemyHit(lightDamage, enemy); //enemy should have a method enemyHit 
    //                else if (attackDoneType == 2)
    //                    //enemy.GetComponent<EnemyScript>().EnemyHit(heavyDamage, enemy); //enemy should have a method enemyHit

    //                    /*if (collidedObject.GetComponent<AIBossHealthPoints>().health <= 0)
    //                    {
    //                        //BOSS DEAD:
    //                        //  Animtaion of dead boss
    //                        //  Game over
    //                        //  Credites
    //                    }

    //            }*/

    //            if (enemy.tag == ("Foe"))
    //            {
    //                if (attackDoneType == 1)
    //                    enemy.GetComponent<AIEnemyHealthPoints>().EnemyHit(lightDamage); //enemy should have a method enemyHit 
    //                else if (attackDoneType == 2)
    //                    enemy.GetComponent<AIEnemyHealthPoints>().EnemyHit(heavyDamage); //enemy should have a method enemyHit 


    //                //check if the foe DIED:
    //                if (enemy.GetComponent<AIEnemyHealthPoints>().health <= 0)
    //                {
    //                    KratosController.Instance.SetXp(KratosController.Instance.GetXp() + 50);
    //                }
    //            }

    //            attackDoneType = 0;

    //        }
    //        else
    //        {
    //            Debug.Log("yeb2a youmna eswed");
    //        }
    //    }
    //}

    //when kratos hits an enemy:
    private void OnTriggerEnter(Collider other)
    {
        //get the light damage and heavy damage to be passsed to the enemies:
        lightDamage = PlayerController.Instance.GetLightDamage();
        heavyDamage = PlayerController.Instance.GetHeavyDamage();

        enemy = other.gameObject;
        if (attackDoneType != 0)  //the collision was as a result of an attack
        {

            //Increase rage
            PlayerController.Instance.SetRage(PlayerController.Instance.GetRage() + 1);

            //check if kratos killed the bos:
            if (enemy.tag == ("Boss"))
            {
                if (attackDoneType == 1)
                {
                    Debug.Log("light and boss");
                    // enemy.GetComponent<EnemyScript>().EnemyHit(lightDamage, enemy); //enemy should have a method enemyHit 
                }

                else if (attackDoneType == 2)
                {
                    Debug.Log("high and boss");
                    //enemy.GetComponent<EnemyScript>().EnemyHit(heavyDamage, enemy); //enemy should have a method enemyHit
                }


                /*  if (collidedObject.GetComponent<AIBossHealthPoints>().health <= 0)
                  {
                      //BOSS DEAD:
                      //  Animtaion of dead boss
                      //  Game over
                      //  Credites
                  }
                  */
            }

            if (enemy.tag == ("Foe"))
            {
                if (attackDoneType == 1)
                {
                    Debug.Log("light and foe");
                    //  enemy.GetComponent<AIEnemyHealthPoints>().EnemyHit(lightDamage); //enemy should have a method enemyHit 

                }
                else if (attackDoneType == 2)
                {
                    Debug.Log("high and foe");
                    //   enemy.GetComponent<AIEnemyHealthPoints>().EnemyHit(heavyDamage); //enemy should have a method enemyHit 

                }


                //check if the foe DIED:
                /*   if (enemy.GetComponent<AIEnemyHealthPoints>().health <= 0)
                   {
                   KratosController.Instance.SetXp(KratosController.Instance.GetXp() + 50);

                   }
           } */

                attackDoneType = 0;

            }
        }
    }
}
