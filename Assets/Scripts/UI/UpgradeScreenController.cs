using UnityEngine;
using UnityEngine.UI;

public class UpgradeScreenController : MonoBehaviour
{
    public ProgressBar movementBar;
    public Button movementAdd;
    public ProgressBar lightAttackBar;
    public ProgressBar heavyAttackBar;
    public Button attackAdd;
    public ProgressBar healthBar;
    public Button healthAdd;
    public Text skillPointsText;

    private void Start()
    {
        movementBar.BarValue = PlayerController.Instance.GetSpeed();
        lightAttackBar.BarValue = PlayerController.Instance.GetLightDamage();
        heavyAttackBar.BarValue = PlayerController.Instance.GetHeavyDamage();
        healthBar.BarValue = PlayerController.Instance.GetHealthPoints();
        SetSkillPoints();
        HideButtonsOnZero();
    }

    private void Update()
    {
        SetSkillPoints();
        HideButtonsOnZero();
    }

    private void HideButtonsOnZero()
    {
        bool hasLeft = PlayerController.Instance.GetSkillPoints() > 0;
        movementAdd.gameObject.SetActive(hasLeft);
        attackAdd.gameObject.SetActive(hasLeft);
        healthAdd.gameObject.SetActive(hasLeft);
    }

    public void AddMovement()
    {
        PlayerController.Instance.UpgradeMovement();
        movementBar.BarValue = PlayerController.Instance.GetSpeed();
        SetSkillPoints();
    }

    public void AddAttack()
    {
        PlayerController.Instance.UpgradeAttack();
        lightAttackBar.BarValue = PlayerController.Instance.GetLightDamage();
        heavyAttackBar.BarValue = PlayerController.Instance.GetHeavyDamage();
        SetSkillPoints();
    }

    public void AddHealth()
    {
        PlayerController.Instance.UpgradeHealth();
        healthBar.BarValue = PlayerController.Instance.GetHealthPoints();
        SetSkillPoints();
    }

    public void SetSkillPoints()
    {
        skillPointsText.text = "Skill Points: " + PlayerController.Instance.GetSkillPoints();
    }
}
