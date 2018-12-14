using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;
    private int xp, targetXP, rage, skillPoints, level;
    private bool rageFull, rageMode;
    private float healthPoints, timeLeft, rageDuration, lightAttackDamage, heavyAttackDamage;
    private Animator anim;
    private GameObject enemy, chest;
    private ThirdPersonCharacter character;
    public bool won, nearChest;

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

    public float MaxHealthPoints
    {
        get;
        set;
    }

    public bool Dead
    {
        get;
        set;
    }

    public float MaxRage
    {
        get;
        set;
    }

    private void Start()
    {
        character = GetComponent<ThirdPersonCharacter>();
        anim = GetComponent<Animator>();
        MaxHealthPoints = 100.0f;
        xp = 0;
        targetXP = 500;
        level = 1;
        rage = 0;
        rageMode = false;
        rageDuration = 50.0f;
        MaxRage = 10;
        skillPoints = 2;
        nearChest = false;
        healthPoints = MaxHealthPoints;
        lightAttackDamage = 10.0f;
        heavyAttackDamage = 30.0f;
    }

    private void Update()
    {
        if (!(GameController.Instance.GamePaused || GameController.Instance.GameOver))
        {
            CheckNearChest();
            CheckDeath();
            CheckAttackInput();
            CheckRage(); //If the rage meter is full --> rageFull = true
            CheckXP();
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

    private void CheckAttackInput()
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
    }

    private void CheckDeath()
    {
        if (healthPoints <= 0)
        {
            Dead = true;
            anim.SetBool("Dead", true);  //For the death animation to work
        }
    }

    private void CheckNearChest()
    {
        if (nearChest && Input.GetKeyDown(KeyCode.X) && !chest.GetComponent<ChestScript>().GetChestOpened())
        {
            chest.GetComponent<ChestScript>().SetOpen(true);
            RestoreHealth();
        }
    }

    // A method to check if the rage meter is full
    private void CheckRage()
    {
        rageFull |= rage >= MaxRage;

        if (Input.GetKey(KeyCode.R) && rageFull)  //if the user presses on R (and the rage is full) --> the rage mode: ON
        {
            timeLeft = rageDuration;
            rageMode = true;
            rage = 0;
            rageFull = false;
            lightAttackDamage *= 2;
            heavyAttackDamage *= 2;
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
                heavyAttackDamage /= 2;
            }
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

    public void KratosGotHit()
    {
        Debug.Log("Etdarabt");
        //when an enemy hits kratos, health points decreases by 10:
        healthPoints -= 10.0f;
        anim.SetTrigger("Hit");
    }

    public void UpgradeMovement()
    {
        character.IncreaseSpeed();
        skillPoints--;
    }

    public void UpgradeAttack()
    {
        lightAttackDamage *= 1.1f;
        heavyAttackDamage *= 1.1f;
        skillPoints--;
    }

    public void UpgradeHealth()
    {
        healthPoints *= 1.1f;
        MaxHealthPoints *= 1.1f;
        skillPoints--;
    }

    //After Kratos opens the chest--> Health is restored:
    public void RestoreHealth()
    {
        healthPoints = MaxHealthPoints;
        //Destroy(chest);
        //chest = null;
    }

    //Getters for the variables that should appear on the screen: health, rage, level, xp, skill points:
    public float GetHealthPoints()
    {
        return healthPoints;
    }

    public float GetSpeed()
    {
        return character.GetSpeed();
    }

    public int GetRage()
    {
        return rage;
    }

    public void SetRage(int rage)
    {
        this.rage = rage;
    }

    public int GetLevel()
    {
        return level;
    }

    public int GetXp()
    {
        return xp;
    }

    public void IncreaseXP()
    {
        xp += 50;
    }

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
        return heavyAttackDamage;
    }

    public void Reset()
    {
        RestoreHealth();
        rage = 0;
    }
}
