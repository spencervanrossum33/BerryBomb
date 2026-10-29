using UnityEngine;

//Data Class
[System.Serializable]
public class ShopUpgradeData
{
    public string upgradeName;
    public string description;
    public ShopUpgradeType type;
    public bool isPercentage;
    public int currentLevel;
    public int[] levelCosts;
}
