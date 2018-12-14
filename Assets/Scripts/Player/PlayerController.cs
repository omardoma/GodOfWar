using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;
    private int xp, targetXP, rage, skillPoints, maxRage, level;
    private bool rageFull, rageMode;
    private float speed, timeLeft, rageDuration, lightAttackDamage, HeavyAttackDamage;
    private Animator anim;
    private GameObject enemy, chest;
    public float healthPoints;
    public bool dead, won, nearChest;

    public static PlayerController Instance
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
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        xp = 0;
        targetXP = 500;
        level = 1;
        rage = 0;
        rageMode = false;
        rageDuration = 50.0f;
        maxRage = 10;
        skillPoints = 0;
        nearChest = false;
        healthPoints = 100.0f;
        lightAttackDamage = 10.0f;
        HeavyAttackDamage = 30.0f;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            DaggerScript.Instance.attackDoneType = 1;
            anim.SetTrigger("LightAttack");
        }

        if (Input.GetMouseButtonUp(1))
        {
            DaggerScript.Instance.attackDoneType = 2;
            anim.SetTrigger("HeavyAttack");
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            anim.SetTrigger("Evade");
        }

        CheckRage(); //If the rage meter is full --> rageFull = true
        CheckXP();

        if (nearChest && Input.GetKeyDown(KeyCode.X) && !chest.GetComponent<ChestScript>().GetChestOpened())
        {
            chest.GetComponent<ChestScript>().SetOpen(true);
            RestoreHealth();
        }

        // KRATOS DEATH:
        if (healthPoints <= 0)
        {
            dead = true;
            anim.SetBool("Dead", dead);  //For the death animation to work
        }

        if (Input.GetKey(KeyCode.R) && rageFull)  //if the user presses on R (and the rage is full) --> the rage mode: ON
        {
            timeLeft = rageDuration;
            rageMode = true;
            rage = 0;
            rageFull = false;
            lightAttackDamage *= 2;
            HeavyAttackDamage *= 2;
        }

        //Wait for seconds before rage mode is off
        if (rageMode)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                //reset everything after the rage time is over:
                rageMode = false;
                lightAttackDamage /= 2;
                HeavyAttackDamage /= 2;
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Chest")
        {
            nearChest = true;
            chest = collider.gameObject;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Chest")
        {
            nearChest = false;
        }
    }

    // A method to check if the rage meter is full
    private void CheckRage()
    {
        if (rage >= maxRage)
        {
            rageFull = true;
        }
    }

    // A method to check if the xp reached the target xp --> Got o the next level 
    private void CheckXP()
    {
        if (xp >= targetXP)  //when the xp reaches the taregt xp --> Kratos levels up, update the target and add to the skill point
        {
            level += 1;
            targetXP *= 2;
            skillPoints += 1;
        }
    }


    //----------------------------------------------PUBLIC METHODS--------------------------------------------------//

    //DOOBY WILL CALL THIS WHEN KRATOS IS HIT:
    public void KratosGotHit()
    {
        Debug.Log("Etdarabt");
        //when an enemy hits kratos, helath points decreases by 10:
        healthPoints -= 10.0f;
        anim.SetTrigger("Hit");
    }


    //OMAR DOMA WILL CALL THESE:

    //Upgrade skills:
    public void UpgradeMovement()
    {
        speed = speed * 1.1f;
        skillPoints--;
    }

    public void UpgradeAttack()
    {
        lightAttackDamage = lightAttackDamage * 1.1f;
        HeavyAttackDamage = HeavyAttackDamage * 1.1f;
        skillPoints--;
    }

    public void UpgradeHealth()
    {
        healthPoints = healthPoints * 1.1f;
        skillPoints--;
    }

    //After Kratos opens the chest--> Health is restored:
    public void RestoreHealth()
    {
        healthPoints = 100.0f;
        //Destroy(chest);
        //chest = null;
    }
    //Getters for the variables that should appear on the screen: health, rage, level, xp, skill points:
    public float GetHealthPoints()
    {
        return healthPoints;
    }

    public int GetRage()
    {
        return rage;
    }

    public void SetRage(int r)
    {
        rage = r;
    }

    public int GetLevel()
    {
        return level;
    }

    public int GetXp()
    {
        return xp;
    }

    public void SetXp(int x)
    {
        xp = x;
    }

    //Return skill points available to enable/disable "upgrade skill" buttons in pause screen
    public int GetSkillPoints()
    {
        return skillPoints;
    }

    public float GetLightDamage()
    {
        return lightAttackDamage;
    }

    public float GetHeavyDamage()
    {
        return HeavyAttackDamage;
    }
}
