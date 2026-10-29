using System;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    //Declare Managers and Stats
    private UIManager uiManager;
    private UpgradeManager upgradeManager;
    private PlayerStats playerStats;

    //Variables for storage and lookup tables
    public int coins = 0;
    public List<Upgrade> upgradeList;
    private Dictionary<BerryType, int> baseCoinAmounts;

    //Logic Variables for modifying amount of coins dropped
    private float baseLuckyCoinMult = 0.1f;
    private float magnetMult = 1.5f;

    //Initialization
    void Start()
    {
        uiManager = GetComponentInParent<UIManager>();
        upgradeManager = GetComponentInParent<UpgradeManager>();
        upgradeList = upgradeManager.GetUpgradeList();
        playerStats = GetComponentInParent<PlayerStats>();
    }

    //Creating Dictionary for berry coin amounts
    void Awake()
    {
        baseCoinAmounts = new Dictionary<BerryType, int>
        {
            {BerryType.Blueberry, 1},
            {BerryType.Apple, 3},
            {BerryType.Pear, 20},
            {BerryType.Cherry, 100},
            {BerryType.Grape, 325},
            {BerryType.Banana, 2150},
            {BerryType.Strawberry, 6500},
            {BerryType.Watermelon, 37500},
            {BerryType.Pumpkin, 225000}
        };
    }

    //Main function for giving the player coins
    public void DropCoins(BerryType berryType)
    {
        float modifier = 1.0f;
        //Natural Variation of 15%
        modifier += UnityEngine.Random.Range(-0.15f, 0.15f);
        //Lucky Coin Multiplier
        modifier += upgradeManager.GetUpgradeCount(UpgradeType.LuckyCoin) * (baseLuckyCoinMult + playerStats.luckyCoinMult);
        //Magnet Multiplier
        if (UnityEngine.Random.value < Math.Min(upgradeManager.GetUpgradeCount(UpgradeType.Magnet) * 0.01f, 0.75f))
        {
            modifier *= magnetMult + playerStats.magnetMult;
        }
        //Give coins to player and change labels
        coins += Mathf.RoundToInt(baseCoinAmounts[berryType] * modifier);
        uiManager.changeCoinLabel(coins);
    }
}
