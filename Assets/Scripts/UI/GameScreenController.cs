using UnityEngine;
using UnityEngine.UI;


public class GameScreenController : MonoBehaviour
{
    private static GameScreenController instance;
    public ProgressBar healthBar;
    public ProgressBar rageBar;
    public ProgressBar xpBar;
    public Text levelText;
    public Text skillPoints;

    public static GameScreenController Instance
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

    private void Update()
    {
        SetHealth();
        SetRage();
        SetXP();
        SetLevel();
        SetSkillPoints();
    }

    private void SetHealth()
    {
        float health = PlayerController.Instance.GetHealthPoints();
        healthBar.BarValue = health;
    }

    private void SetRage()
    {
        float rage = PlayerController.Instance.GetRage();
        rageBar.BarValue = rage;
    }

    private void SetXP()
    {
        float xp = PlayerController.Instance.GetXp();
        xpBar.BarValue = xp;
    }

    private void SetLevel()
    {
        int level = PlayerController.Instance.GetLevel();
        this.levelText.text = "Level: " + level;
    }

    private void SetSkillPoints()
    {
        float sp = PlayerController.Instance.GetSkillPoints();
        skillPoints.text = "Skill Points: " + sp;
    }
}
