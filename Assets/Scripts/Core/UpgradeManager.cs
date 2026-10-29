using System;
using System.Collections.Generic;
using UnityEngine;

//Enums for types of berries and upgrades
public enum BerryType 
{
    Blueberry,
    Apple,
    Pear,
    Cherry,
    Grape,
    Banana,
    Strawberry,
    Watermelon,
    Pumpkin
}
public enum UpgradeType
{
    Worm,
    Blender,
    LuckyCoin,
    Magnet,
    PointyGlove,
    FruitSlicer,
    Milk,
    Dud,
    Clock,
    Fertilizer,
    Flower,
    Clover,
    Cloud
}


//Upgrade Class for it's type and weight to determine how likely it will be dropped
[System.Serializable]
public class Upgrade
{
    public UpgradeType type;
    public float weight;
    
    public Upgrade(UpgradeType type, float weight)
    {
        this.type = type;
        this.weight = weight;
    }
}


public class UpgradeManager : MonoBehaviour
{
    //Declare Managers, Dictionary, and Audio
    public List<Upgrade> upgradeList = new List<Upgrade>();
    private UIManager uiManager;
    private DamageManager damageManager;
    private PlayerStats playerStats;
    private Dictionary<BerryType, float> baseUpgradeDropChance;
    public AudioSource audioSource;
    public AudioClip audioClip;

    //Logic Variable for Clover Upgrade implementation
    private float baseCloverMult = 0.01f;
    public float cloverMult;


    //Initialization
    void Start()
    {
        uiManager = GetComponentInParent<UIManager>();
        damageManager = GetComponentInParent<DamageManager>();
        playerStats = GetComponentInParent<PlayerStats>();
    }

    //Initializing Dictionary
    void Awake()
    {
        baseUpgradeDropChance = new Dictionary<BerryType, float>
        {
            {BerryType.Blueberry, 0.30f},
            {BerryType.Apple, 0.35f},
            {BerryType.Pear, 0.45f},
            {BerryType.Cherry, 0.55f},
            {BerryType.Grape, 0.65f},
            {BerryType.Banana, 0.70f},
            {BerryType.Strawberry, 0.85f},
            {BerryType.Watermelon, 0.95f},
            {BerryType.Pumpkin, 1f}
        };

        cloverMult = baseCloverMult;
    }

    //Public Getter Functions
    public List<Upgrade> GetUpgradeList()
    {
        return upgradeList;
    }
    public int GetUpgradeCount(UpgradeType type)
    {
        int count = 0;
        foreach (Upgrade upgrade in upgradeList)
        {
            if (upgrade.type == type)
            {
                count++;
            }
        }
        return count;
    }


    //Private Getter Functions
    private List<Upgrade> GetUpgradeTable(BerryType berryType) 
    {
        switch(berryType)
        {
            case BerryType.Blueberry:
                return new List<Upgrade>
                {
                    new Upgrade(UpgradeType.Worm, 4f),
                    new Upgrade(UpgradeType.PointyGlove, 4f)
                };

            case BerryType.Apple:
                return new List<Upgrade>
                {
                    new Upgrade(UpgradeType.Worm, 2f),
                    new Upgrade(UpgradeType.PointyGlove, 2f),
                    new Upgrade(UpgradeType.Milk, 4f),
                    new Upgrade(UpgradeType.Fertilizer, 3f)
                };

            case BerryType.Pear:
                return new List<Upgrade>
                {
                    new Upgrade(UpgradeType.Worm, 4f),
                    new Upgrade(UpgradeType.PointyGlove, 4f),
                    new Upgrade(UpgradeType.Milk, 4f),
                    new Upgrade(UpgradeType.Blender, 1f),
                    new Upgrade(UpgradeType.Fertilizer, 4f),
                    new Upgrade(UpgradeType.Flower, 1f),
                    new Upgrade(UpgradeType.LuckyCoin, 2f)
                };
            
            case BerryType.Cherry:
                return new List<Upgrade>
                {
                    new Upgrade(UpgradeType.Worm, 3f),
                    new Upgrade(UpgradeType.PointyGlove, 3f),
                    new Upgrade(UpgradeType.Milk, 3f),
                    new Upgrade(UpgradeType.Blender, 2f),
                    new Upgrade(UpgradeType.Fertilizer, 3f),
                    new Upgrade(UpgradeType.Flower, 2f),
                    new Upgrade(UpgradeType.LuckyCoin, 3f),
                    new Upgrade(UpgradeType.Clover, 1f),
                };

            case BerryType.Grape:
                return new List<Upgrade>
                {
                    new Upgrade(UpgradeType.Worm, 3f),
                    new Upgrade(UpgradeType.PointyGlove, 3f),
                    new Upgrade(UpgradeType.Milk, 4f),
                    new Upgrade(UpgradeType.Blender, 2f),
                    new Upgrade(UpgradeType.FruitSlicer, 1f),
                    new Upgrade(UpgradeType.Fertilizer, 4f),
                    new Upgrade(UpgradeType.Flower, 3f),
                    new Upgrade(UpgradeType.Clock, 1f),
                    new Upgrade(UpgradeType.LuckyCoin, 4f),
                    new Upgrade(UpgradeType.Clover, 1f),
                };

            case BerryType.Banana:
                return new List<Upgrade>
                {
                    new Upgrade(UpgradeType.Worm, 2f),
                    new Upgrade(UpgradeType.PointyGlove, 2f),
                    new Upgrade(UpgradeType.Milk, 3f),
                    new Upgrade(UpgradeType.Blender, 2f),
                    new Upgrade(UpgradeType.FruitSlicer, 1f),
                    new Upgrade(UpgradeType.Fertilizer, 4f),
                    new Upgrade(UpgradeType.Flower, 4f),
                    new Upgrade(UpgradeType.Clock, 2f),
                    new Upgrade(UpgradeType.LuckyCoin, 4f),
                    new Upgrade(UpgradeType.Clover, 2f),
                    new Upgrade(UpgradeType.Magnet, 3f),
                };

            case BerryType.Strawberry:
                return new List<Upgrade>
                {
                    new Upgrade(UpgradeType.Worm, 1f),
                    new Upgrade(UpgradeType.PointyGlove, 1f),
                    new Upgrade(UpgradeType.Milk, 2f),
                    new Upgrade(UpgradeType.Blender, 2f),
                    new Upgrade(UpgradeType.FruitSlicer, 1f),
                    new Upgrade(UpgradeType.Fertilizer, 2f),
                    new Upgrade(UpgradeType.Flower, 4f),
                    new Upgrade(UpgradeType.Clock, 4f),
                    new Upgrade(UpgradeType.LuckyCoin, 4f),
                    new Upgrade(UpgradeType.Clover, 4f),
                    new Upgrade(UpgradeType.Magnet, 4f),
                    new Upgrade(UpgradeType.Dud, 1f),
                    new Upgrade(UpgradeType.Cloud, 4f)
                };

            case BerryType.Watermelon:
                return new List<Upgrade>
                {
                    new Upgrade(UpgradeType.Worm, 1f),
                    new Upgrade(UpgradeType.PointyGlove, 1f),
                    new Upgrade(UpgradeType.Milk, 4f),
                    new Upgrade(UpgradeType.Blender, 4f),
                    new Upgrade(UpgradeType.FruitSlicer, 2f),
                    new Upgrade(UpgradeType.Fertilizer, 2f),
                    new Upgrade(UpgradeType.Flower, 4f),
                    new Upgrade(UpgradeType.Clock, 4f),
                    new Upgrade(UpgradeType.LuckyCoin, 4f),
                    new Upgrade(UpgradeType.Clover, 4f),
                    new Upgrade(UpgradeType.Magnet, 4f),
                    new Upgrade(UpgradeType.Dud, 1f),
                    new Upgrade(UpgradeType.Cloud, 4f)
                };

            case BerryType.Pumpkin:
                return new List<Upgrade>
                {
                    new Upgrade(UpgradeType.Worm, 1f),
                    new Upgrade(UpgradeType.PointyGlove, 1f),
                    new Upgrade(UpgradeType.Milk, 2f),
                    new Upgrade(UpgradeType.Blender, 2f),
                    new Upgrade(UpgradeType.FruitSlicer, 5f),
                    new Upgrade(UpgradeType.Fertilizer, 1f),
                    new Upgrade(UpgradeType.Flower, 4f),
                    new Upgrade(UpgradeType.Clock, 4f),
                    new Upgrade(UpgradeType.LuckyCoin, 3f),
                    new Upgrade(UpgradeType.Clover, 4f),
                    new Upgrade(UpgradeType.Magnet, 4f),
                    new Upgrade(UpgradeType.Dud, 4f),
                    new Upgrade(UpgradeType.Cloud, 4f)
                };

            default:
                return null;

        }
    }
    private Upgrade GetRandomUpgrade(List<Upgrade> upgradeTable)
    {
        float totalWeight = 0f;

        foreach (Upgrade upgrade in upgradeTable)
        {
            totalWeight += upgrade.weight;
        }
        float roll = UnityEngine.Random.Range(0, totalWeight);
        float currWeight = 0f;

        foreach (Upgrade upgrade in upgradeTable)
        {
            currWeight += upgrade.weight;
            if (roll < currWeight)
            {
                return upgrade;
            }
        }
        return null;
    }


    //Public Methods for Upgrade Management
    public void DropUpgrade(BerryType berryType)
    {
        //Determine clover's effect on drop chances
        float cloverModifier = GetUpgradeCount(UpgradeType.Clover) * (cloverMult + playerStats.cloverMult);
        float upgradeDropChance = baseUpgradeDropChance[berryType] + cloverModifier;
        //Random chance for upgrade to drop, unless the cloud is active
        if (UnityEngine.Random.value < Math.Min(upgradeDropChance, 0.95f) || damageManager.isCloudActive == true)
        {
            Upgrade randomUpgrade = GetRandomUpgrade(GetUpgradeTable(berryType));

            if (randomUpgrade == null)
            {
                Debug.LogError($"No upgrade selected for {berryType}");
                return;
            }

            upgradeList.Add(randomUpgrade);
            uiManager.changeUpgradeLables();
            audioSource.PlayOneShot(audioClip);
            Debug.Log(randomUpgrade.type);
        }
        else
        {
            Debug.Log("The upgrade did not drop");
        }
    }

    //Remove method for when upgrades are used
    public void RemoveUpgrade(UpgradeType upgradeType)
    {
        upgradeList.Remove(upgradeList.Find(u => u.type == upgradeType));
        uiManager.changeUpgradeLables();
    }

    //Tunnel Method
    public void UpgradeLabelTunnel()
    {
        uiManager.changeUpgradeLables();
    }
}
