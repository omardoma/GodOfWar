using UnityEngine;

public class DaggerScript : MonoBehaviour
{
    private static DaggerScript instance;
    private GameObject enemy;
    private float lightDamage, heavyDamage;
    public int attackDoneType;

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
                AIBossHealthPoints bossCtrl = enemy.GetComponent<AIBossHealthPoints>();
                if (attackDoneType == 1)
                {
                    Debug.Log("light and boss");
                    bossCtrl.EnemyHit(lightDamage);
                }
                else if (attackDoneType == 2)
                {
                    Debug.Log("high and boss");
                    bossCtrl.EnemyHit(heavyDamage);
                }

                if (bossCtrl.health <= 0)
                {
                    //BOSS DEAD:
                    //  Animation of dead boss
                    //  Game over
                    //  Credites
                }
            }

            if (enemy.tag == ("Foe"))
            {
                AIEnemyHealthPoints foeCtrl = enemy.GetComponent<AIEnemyHealthPoints>();

                if (attackDoneType == 1)
                {
                    Debug.Log("light and foe");
                    foeCtrl.EnemyHit(lightDamage);
                }
                else if (attackDoneType == 2)
                {
                    Debug.Log("high and foe");
                    foeCtrl.EnemyHit(heavyDamage);
                }

                //check if the foe DIED:
                if (foeCtrl.health <= 0)
                {
                    PlayerController.Instance.IncreaseXP();
                }
            }
            attackDoneType = 0;
        }
    }
}
