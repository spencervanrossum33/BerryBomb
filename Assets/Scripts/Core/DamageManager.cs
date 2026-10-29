using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    //Declare Managers and Stats
    public List<Upgrade> upgradeList;
    private SpawnManager spawnManager;
    private UIManager uiManager;
    private UpgradeManager upgradeManager;
    private CoinManager coinManager;
    private PlayerStats playerStats;

    //Declare variables for audio, particle system, and critical strike text management
    public AudioSource audioSource;
    public AudioClip pop1;
    public AudioClip pop2;
    public AudioClip pop3;
    public AudioClip pop4;
    public ParticleSystem particleSystem;
    public CriticalStrike criticalStrike;
    public Transform canvasTransform;

    //Worm Variables
    private float wormTimer;
    private float baseWormInterval = 2f;
    private float wormInterval;
    private int baseWormDamage = 1;
    private int wormDamage;

    //Milk Variables
    private bool isMilkActive = false;
    private float milkTimer;
    private float baseMilkTimeActive = 5f;
    private float milkTimeActive;

    //Flower Variables
    public float defaultCritChance = 0.01f;

    //Blender Variables
    private bool isBlenderActive = false;
    private float blenderTimer;
    private float blenderIntervalTimer;
    private float baseBlenderInterval = 0.25f;
    private float blenderInterval;
    private float blenderTimeActive = 5f;
    private int blenderDamage = 50;

    //Fertilizer Variables
    private int baseFertilizerBonusUpgrades = 2;
    private int fertilizerBonusUpgrades;

    //Clock Variables
    private bool isClockActive = false;
    private float clockMultiplier = 1f;
    private float baseClockTime = 5f;

    //Cloud Variables
    public bool isCloudActive = false;
    private float cloudTimer;
    private float cloudIntervalTimer;
    private float cloudInterval = 0.1f;
    private float cloudTimeActive = 10f;

    //Initialization
    void Start()
    {
        spawnManager = GetComponentInParent<SpawnManager>();
        uiManager = GetComponentInParent<UIManager>();
        upgradeManager = GetComponentInParent<UpgradeManager>();
        upgradeList = upgradeManager.GetUpgradeList();
        coinManager = GetComponentInParent<CoinManager>();
        playerStats = GetComponentInParent<PlayerStats>();
    }

    //Main Function for Upgrades that use time: worms, milk, blenders, clocks, and clouds
    void Update()
    {
        //Worm Logic
        wormTimer += Time.deltaTime;
        int wormCount = upgradeManager.GetUpgradeCount(UpgradeType.Worm);
        if (wormTimer >= GetWormInterval())
        {
            wormTimer -= GetWormInterval();
            if (wormCount > 0)
            {
                wormDamage = baseWormDamage + playerStats.wormDamage;
                Damage(wormCount * wormDamage);
            }
        }

        //Milk Logic
        if (isMilkActive == true)
        {
            milkTimer += Time.deltaTime;
            if (milkTimer >= milkTimeActive)
            {
                isMilkActive = false;
                milkTimeActive = baseMilkTimeActive;
                milkTimer = 0f;
            }
        }

        //Blender Logic
        if (isBlenderActive == true)
        {
            blenderTimer += Time.deltaTime;
            blenderIntervalTimer += Time.deltaTime;
            if (blenderTimer >= blenderTimeActive)
            {
                isBlenderActive = false;
                blenderTimer = 0f;
            }
            if (blenderIntervalTimer >= GetBlenderInterval())
            {
                blenderIntervalTimer -= blenderInterval;
                Damage(blenderDamage + playerStats.blenderDamage);
            }
        }

        //Cloud Logic
        if (isCloudActive == true)
        {
            cloudTimer += Time.deltaTime;
            cloudIntervalTimer += Time.deltaTime;
            if (cloudTimer >= cloudTimeActive)
            {
                isCloudActive = false;
                cloudTimer = 0f;
            }
            if (cloudIntervalTimer >= cloudInterval)
            {
                cloudIntervalTimer -= cloudInterval;
                CloudHeal();
            }
        }
    }

    //Calculate Damage Function, takes flat + multiplier bonuses into account
    public int CalculateDamage()
    {
        int baseDamage = 1;
        int damage = baseDamage;
        int pointyGloveDamage = upgradeManager.GetUpgradeCount(UpgradeType.PointyGlove) * (playerStats.pointyGloveDamage+1);
        //Flat Bonuses
        damage += pointyGloveDamage;

        //Pointy Glove Multiplier Required so it can help out early and late game
        float pointyGloveMult = 1f + upgradeManager.GetUpgradeCount(UpgradeType.PointyGlove) * 0.03f;
        damage = Mathf.RoundToInt(damage * pointyGloveMult);

        //Milk Multiplier
        if (isMilkActive == true)
        {
            damage *= 2;
        }

        //Crit Chance
        int flowerCount = upgradeManager.GetUpgradeCount(UpgradeType.Flower);
        float critChance = Math.Min(0.75f, flowerCount * (defaultCritChance + playerStats.flowerCritStat));
        if (UnityEngine.Random.value < critChance)
        {
            damage = Mathf.CeilToInt(damage * (2 + playerStats.flowerCritDamageMult)); //Crit Damage

            //Critical Strike Text Creation
            GameObject berry = FindAnyObjectByType<Berry>().gameObject;
            CriticalStrike text = Instantiate(criticalStrike, canvasTransform);
            Vector3 screenPos = Camera.main.WorldToScreenPoint(berry.transform.position);
            text.GetComponent<RectTransform>().position = screenPos;
        }

        return damage;
    }


    //Damage Function called by Berries
    public void Damage(ref int health, GameObject berry)
    {
        health -= CalculateDamage();
        uiManager.changeHealthLabel(health);
        if (health <= 0)
        {
            KillBerry(berry);
        }
    }
    //Overloaded Damage function for use with upgrades that do a certain amount of damage independently
    public void Damage(int amnt)
    {
        GameObject berry = FindAnyObjectByType<Berry>().gameObject;
        Berry berryScript = berry.GetComponent<Berry>();

        //Pointy Glove Multiplier Required so it can help out early and late game
        float pointyGloveMult = 1f + upgradeManager.GetUpgradeCount(UpgradeType.PointyGlove) * 0.03f;
        int damage = Mathf.RoundToInt(amnt * pointyGloveMult);

        berryScript.health -= damage;
        uiManager.changeHealthLabel(berryScript.health);
        if (berryScript.health <= 0)
        {
            KillBerry(berry);
        }
    }

    //Consolidating all logic for killing berries here for cleanliness
    public void KillBerry(GameObject berry)
    {
        Berry berryScript = berry.GetComponent<Berry>();

        //Fertilizer Check
        if (berryScript.isFertilized == true)
        {
            for (int i = 0; i < fertilizerBonusUpgrades; i++)
            {
                upgradeManager.DropUpgrade(berryScript.berryType);
            }
            spawnManager.RespawnBerry(berryScript.berryType);
        }
        //Regular Operation
        else
        {
            upgradeManager.DropUpgrade(berryScript.berryType);
            coinManager.DropCoins(berryScript.berryType);
            audioSource.PlayOneShot(GetPopSound(), 0.3f);
            particleSystem.Play();
            spawnManager.RespawnBerry(berryScript.berryType);
        }
    }

    //Function for randomizing the pop sound you hear when killing berries so it doesn't get too repetitive and annoying
    private AudioClip GetPopSound()
    {
        int soundIndex = UnityEngine.Random.Range(0,4);
        switch(soundIndex)
        {
            case 0:
                return pop1;
            case 1:
                return pop2;
            case 2:
                return pop3;
            case 3:
                return pop4;
            default:
                return null;
        }
    }


    //Upgrade Implementation Methods
    public void CloudHeal()
    {
        GameObject berry = FindAnyObjectByType<Berry>().gameObject;
        Berry berryScript = berry.GetComponent<Berry>();
        berryScript.health += Mathf.CeilToInt(berryScript.maxHealth * 0.01f);
        uiManager.changeHealthLabel(berryScript.health);
    }
    public void ActivateCloud()
    {
        isCloudActive = true;
    }
    public void DeactivateCloud()
    {
        isCloudActive = false;
    }
    public void ActivateMilk()
    {
        isMilkActive = true;
        milkTimeActive = (baseMilkTimeActive + playerStats.milkTimeStat) / clockMultiplier;
    }
    public void ActivateBlender()
    {
        isBlenderActive = true;
    }
    public void UseFruitSlicer()
    {
        GameObject berry = FindAnyObjectByType<Berry>().gameObject;
        Berry berryScript = berry.GetComponent<Berry>();
        Damage(berryScript.health / (8 - playerStats.fruitSlicerFraction));
    }
    public void UseFertilizer()
    {
        GameObject berry = FindAnyObjectByType<Berry>().gameObject;
        Berry berryScript = berry.GetComponent<Berry>();
        berryScript.health += berryScript.maxHealth / 2;
        uiManager.changeHealthLabel(berryScript.health);
        fertilizerBonusUpgrades = baseFertilizerBonusUpgrades + playerStats.fertilizerBonus;
    }
    public void ActivateClock()
    {
        if (isClockActive)
        {
            return;
        }
        StartCoroutine(ClockCoroutine());
    }

    //Needed this coroutine so that the clock could have proper effect
    private System.Collections.IEnumerator ClockCoroutine()
    {
        isClockActive = true;
        clockMultiplier = 2f + playerStats.clockMult;
        milkTimeActive = baseMilkTimeActive * clockMultiplier;
        yield return new WaitForSeconds(baseClockTime + playerStats.clockTimeStat);
        clockMultiplier = 1f;
        isClockActive = false;
    }

    //Interval Getter Methods (For Clock Upgrade)
    private float GetWormInterval()
    {
        return wormInterval = (baseWormInterval - playerStats.wormIntervalStat) / clockMultiplier;
    }

    private float GetBlenderInterval()
    {
        return blenderInterval = (baseBlenderInterval - playerStats.blenderIntervalStat) / clockMultiplier;
    }
}
