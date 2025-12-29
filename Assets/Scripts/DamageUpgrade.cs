using UnityEngine;
using UnityEngine.UI;

public class DamageUpgrade : MonoBehaviour
{
    public WeaponStats weaponStats;

    public int level = 0;
    public int maxLevel = 5;

    public int damagePerLevel = 5;
    public int baseCost = 100;

    public Text levelText;
    public Text costText;


    public PlayerScript PlayerScript;

    private void Start()
    {
        UpdateUI();
        weaponStats.damage = weaponStats.baseDamage;
    }

    public void UpgradeDamage()
    {
        if (level >= maxLevel)
        {
            Debug.Log("Максимальний рівень!");
            return;
        }

        int cost = GetUpgradeCost();

        if (PlayerScript.playerMoney < cost)
        {
            Debug.Log("Недостатньо грошей!");
            return;
        }

        PlayerScript.playerMoney -= cost;
        level++;

        weaponStats.damage = weaponStats.baseDamage + level * damagePerLevel;

        UpdateUI();
    }

    int GetUpgradeCost()
    {
        return baseCost * (level + 1);
    }

    void UpdateUI()
    {
        levelText.text = $"Damage Lv. {level}";
        costText.text = level < maxLevel ? $"Price: {GetUpgradeCost()}" : "MAX";
    }
}
