using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour {

    [SerializeField]
    private Text healthText;

    [SerializeField]
    private Text speedText;

    [SerializeField]
    private Text fireRateText;

    [SerializeField]
    private float healthMultiplier = 1.3f;

    [SerializeField]
    private float speedMultiplier = 1.3f;

    [SerializeField]
    private int upgradeCost = 50;

    private PlayerStats stats;

    private Weapon weapon;

    private void OnEnable()
    {
        stats = PlayerStats.instance;
        weapon = Weapon.instance;
        UpdateValues();
    }

    void UpdateValues()
    {
        healthText.text = "Health: " + stats.maxHealth.ToString();
        speedText.text = "Speed: " + stats.movementSpeed.ToString();
        fireRateText.text = "Fire Rate: " + weapon.fireRate.ToString();
    }

    public void UpgradeHealth()
    {
        if (GameMaster.Money < upgradeCost)
        {
            AudioManager.instance.PlaySound("NoMoney");
            return;
        }
            

        stats.maxHealth = (int)(stats.maxHealth * healthMultiplier);

        GameMaster.Money -= upgradeCost;
        AudioManager.instance.PlaySound("Money");

        UpdateValues();
    }

    public void UpgradeSpeed()
    {
        if (GameMaster.Money < upgradeCost)
        {
            AudioManager.instance.PlaySound("NoMoney");
            return;
        }

        stats.movementSpeed = Mathf.Round(stats.movementSpeed * speedMultiplier);

        GameMaster.Money -= upgradeCost;
        AudioManager.instance.PlaySound("Money");

        UpdateValues();
    }

    public void UpgradeFireRate()
    {
        if (GameMaster.Money < upgradeCost)
        {
            AudioManager.instance.PlaySound("NoMoney");
            return;
        }
        weapon.fireRate += 2;

        GameMaster.Money -= upgradeCost;
        AudioManager.instance.PlaySound("Money");

        UpdateValues();
    }
}
