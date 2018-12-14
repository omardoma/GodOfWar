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
        healthBar.BarValue = health < 0 ? 0 : health > 100 ? 100 : health;
    }

    private void SetRage()
    {
        float rage = PlayerController.Instance.GetRage();
        rageBar.BarValue = rage < 0 ? 0 : rage > 100 ? 100 : rage;
    }

    private void SetXP()
    {
        float xp = PlayerController.Instance.GetXp();
        xpBar.BarValue = xp < 0 ? 0 : xp > 100 ? 100 : xp;
    }

    private void SetLevel()
    {
        int level = PlayerController.Instance.GetLevel();
        this.levelText.text = "Level: " + (level < 1 ? 1 : level);
    }

    private void SetSkillPoints()
    {
        float sp = PlayerController.Instance.GetSkillPoints();
        skillPoints.text = "Skill Points: " + (sp < 0 ? 0 : sp);
    }
}
