using UnityEngine;
using UnityEngine.UI;


public class GameScreenController : MonoBehaviour
{
    private static GameScreenController instance;
    public ProgressBar healthBar;
    public ProgressBar rageBar;
    public ProgressBar xpBar;
    public Text level;
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

    private void Start()
    {
        SetHealth(100);
        SetRage(0);
        SetXP(0);
        SetLevel(1);
        SetSkillPoints(0);
    }

    public void SetHealth(float health)
    {
        healthBar.BarValue = health < 0 ? 0 : health > 100 ? 100 : health;
    }

    public void SetRage(float rage)
    {
        rageBar.BarValue = rage < 0 ? 0 : rage > 100 ? 100 : rage;
    }

    public void SetXP(float xp)
    {
        xpBar.BarValue = xp < 0 ? 0 : xp > 100 ? 100 : xp;
    }

    public void SetLevel(float level)
    {
        this.level.text = "Level: " + (level < 1 ? 1 : level > 2 ? 2 : level);
    }

    public void SetSkillPoints(float sp)
    {
        skillPoints.text = "Skill Points: " + (sp < 0 ? 0 : sp);
    }
}
