using UnityEngine;
using UnityEngine.UI;

public class UpgradeScreenController : MonoBehaviour
{
    private float skillPoints;
    public ProgressBar movementBar;
    public Button movementAdd;
    public ProgressBar attackBar;
    public Button attackAdd;
    public ProgressBar healthBar;
    public Button healthAdd;
    public Text skillPointsText;

    private void Start()
    {
        skillPoints = 1;
        SetSkillPoints();
        HideButtonsOnZero();
    }

    private void HideButtonsOnZero()
    {
        movementAdd.gameObject.SetActive(skillPoints > 0 && !movementBar.BarValue.Equals(100));
        attackAdd.gameObject.SetActive(skillPoints > 0 && !attackBar.BarValue.Equals(100));
        healthAdd.gameObject.SetActive(skillPoints > 0 && !healthBar.BarValue.Equals(100));
    }

    public void AddMovement()
    {
        movementBar.BarValue = movementBar.BarValue < 100 ? movementBar.BarValue + 10 : 100;
        DecreaseSkillPoints();
        HideButtonsOnZero();
    }

    public void AddAttack()
    {
        attackBar.BarValue = attackBar.BarValue < 100 ? attackBar.BarValue + 10 : 100;
        DecreaseSkillPoints();
        HideButtonsOnZero();
    }

    public void AddHealth()
    {
        healthBar.BarValue = healthBar.BarValue < 100 ? healthBar.BarValue + 10 : 100;
        DecreaseSkillPoints();
        HideButtonsOnZero();
    }

    public void SetSkillPoints()
    {
        skillPointsText.text = "Skill Points: " + skillPoints;
    }

    public void DecreaseSkillPoints()
    {
        skillPointsText.text = "Skill Points: " + (skillPoints.Equals(0) ? 0 : --skillPoints);
    }
}
