using UnityEngine;

//Enum for upgrade buttons
public enum ShopUpgradeType {
    PointyGloveDamage,
    WormDamage,
    WormSpeed,
    MilkTime,
    FlowerCriticalChance,
    FlowerCriticalDamage,
    BlenderDamage,
    BlenderSpeed,
    FruitSlicerDamage,
    FertilizerBonus,
    ClockTime,
    ClockMultiplier,
    LuckyCoinMultiplier,
    MagnetMultiplier,
    CloverMulitipler,
    WINNER
}

//Class for housing player data for implementation of purchased upgrades
public class PlayerStats : MonoBehaviour
{
    public int pointyGloveDamage = 0;
    public int wormDamage = 0;
    public float wormIntervalStat = 0f;
    public float milkTimeStat = 0f;
    public float flowerCritStat = 0f;
    public float flowerCritDamageMult = 0f;
    public int blenderDamage = 0;
    public float blenderIntervalStat = 0f;
    public int fruitSlicerFraction = 0;
    public int fertilizerBonus = 0;
    public float clockTimeStat = 0f;
    public float clockMult = 0f;
    public float luckyCoinMult = 0f;
    public float magnetMult = 0f;
    public float cloverMult = 0f;
}
