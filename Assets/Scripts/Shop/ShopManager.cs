using UnityEngine;
using UnityEngine.Rendering;

public class ShopManager : MonoBehaviour
{
    //Declare Managers and Audio
    private CoinManager coinManager;
    private UIManager uiManager;
    private ScreenManager screenManager;
    private PlayerStats playerStats;
    public AudioSource audioSource;
    public AudioSource winSource;
    public AudioClip audioClip;
    public AudioClip winClip;
    
    //Initialization
    void Start()
    {
        coinManager = GetComponentInParent<CoinManager>();
        uiManager = GetComponentInParent<UIManager>();
        playerStats = GetComponentInParent<PlayerStats>();
        screenManager = GetComponentInParent<ScreenManager>();
    }

    //Method for actually changing damage/speed/stats when upgrades are purchased
    private void ApplyUpgrade(ShopUpgradeData data)
    {
        switch (data.type)
        {
            case ShopUpgradeType.PointyGloveDamage:
                playerStats.pointyGloveDamage += 1;
                break;
            case ShopUpgradeType.WormDamage:
                playerStats.wormDamage += 1;
                break;
            case ShopUpgradeType.WormSpeed:
                playerStats.wormIntervalStat += 0.25f;
                break;
            case ShopUpgradeType.MilkTime:
                playerStats.milkTimeStat += 2.5f;
                break;
            case ShopUpgradeType.FlowerCriticalChance:
                playerStats.flowerCritStat += 0.01f;
                break;
            case ShopUpgradeType.FlowerCriticalDamage:
                playerStats.flowerCritDamageMult += 0.5f;
                break;
            case ShopUpgradeType.BlenderDamage:
                playerStats.blenderDamage += 1;
                break;
            case ShopUpgradeType.BlenderSpeed:
                playerStats.blenderIntervalStat += 0.05f;
                break;
            case ShopUpgradeType.FruitSlicerDamage:
                playerStats.fruitSlicerFraction += 1;
                break;
            case ShopUpgradeType.FertilizerBonus:
                playerStats.fertilizerBonus += 1;
                break;
            case ShopUpgradeType.ClockTime:
                playerStats.clockTimeStat += 1f;
                break;
            case ShopUpgradeType.ClockMultiplier:
                playerStats.clockMult += 0.5f;
                break;
            case ShopUpgradeType.LuckyCoinMultiplier:
                playerStats.luckyCoinMult += 0.1f;
                break;
            case ShopUpgradeType.MagnetMultiplier:
                playerStats.magnetMult += 0.5f;
                break;
            case ShopUpgradeType.CloverMulitipler:
                playerStats.cloverMult += 0.02f;
                break;
            case ShopUpgradeType.WINNER:
                screenManager.ChangeScreenToWINNER();
                winSource.PlayOneShot(winClip);
                break;
        }
    }

    //Method for getting the player's stats to display when the tooltip is active
    public string GetPlayerStatAmount(ShopUpgradeData data)
    {
        switch (data.type)
        {
            case ShopUpgradeType.PointyGloveDamage:
                return playerStats.pointyGloveDamage.ToString();
            case ShopUpgradeType.WormDamage:
                return playerStats.wormDamage.ToString();
            case ShopUpgradeType.WormSpeed:
                return playerStats.wormIntervalStat.ToString();
            case ShopUpgradeType.MilkTime:
                return playerStats.milkTimeStat.ToString();
            case ShopUpgradeType.FlowerCriticalChance:
                return (playerStats.flowerCritStat * 100).ToString();
            case ShopUpgradeType.FlowerCriticalDamage:
                return playerStats.flowerCritDamageMult.ToString();
            case ShopUpgradeType.BlenderDamage:
                return playerStats.blenderDamage.ToString();
            case ShopUpgradeType.BlenderSpeed:
                return (playerStats.blenderIntervalStat * 100).ToString();
            case ShopUpgradeType.FruitSlicerDamage:
                return playerStats.fruitSlicerFraction.ToString();
            case ShopUpgradeType.FertilizerBonus:
                return playerStats.fertilizerBonus.ToString();
            case ShopUpgradeType.ClockTime:
                return playerStats.clockTimeStat.ToString();
            case ShopUpgradeType.ClockMultiplier:
                return (playerStats.clockMult * 100).ToString();
            case ShopUpgradeType.LuckyCoinMultiplier:
                return (playerStats.luckyCoinMult * 100).ToString();
            case ShopUpgradeType.MagnetMultiplier:
                return (playerStats.magnetMult * 100).ToString();
            case ShopUpgradeType.CloverMulitipler:
                return (playerStats.cloverMult * 100).ToString();
            case ShopUpgradeType.WINNER:
                return "WINNINGS";
            default:
                return null;
        }
    }

    //Main Method to purchase upgrades
    public void Purchase(ShopUpgradeData data, UpgradeNode node)
    {
        if (data.currentLevel >= data.levelCosts.Length)
        {
            Debug.Log("You are maxie!");
            return;
        }
        if (coinManager.coins < data.levelCosts[data.currentLevel])
        {
            Debug.Log("You are brokie!");
            return;
        }
        coinManager.coins -= data.levelCosts[data.currentLevel];
        uiManager.changeCoinLabel(coinManager.coins);
        data.currentLevel += 1;
        ApplyUpgrade(data);
        audioSource.PlayOneShot(audioClip);
        if (data.currentLevel == data.levelCosts.Length)
        {
            node.ShowMaxed();
        }
    }

}
