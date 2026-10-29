using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    //Declare Canvases, Labels, and UpgradeManager
    public GameObject mainCanvas;
    public GameObject shopCanvas;
    private TextMeshProUGUI healthLabel;
    private TextMeshProUGUI bombLabel;
    private TextMeshProUGUI coinLabel;
    private TextMeshProUGUI shopCoinLabel;
    private Dictionary<UpgradeType, TextMeshProUGUI> upgradeLabels;
    private UpgradeManager upgradeManager;

    //Initialization
    void Start()
    {
        upgradeManager = GetComponentInParent<UpgradeManager>();
        healthLabel = mainCanvas.transform.Find("HealthLabel").GetComponent<TextMeshProUGUI>();
        bombLabel = mainCanvas.transform.Find("BombLabel").GetComponent<TextMeshProUGUI>();
        coinLabel = mainCanvas.transform.Find("CoinLabel").GetComponent<TextMeshProUGUI>();
        shopCoinLabel = shopCanvas.transform.Find("ShopCoinLabel").GetComponent<TextMeshProUGUI>();
        
        //Initializing every upgrade label
        upgradeLabels = new Dictionary<UpgradeType, TextMeshProUGUI>();
        foreach(UpgradeType type in System.Enum.GetValues(typeof(UpgradeType)))
        {
            TextMeshProUGUI label = GetLabel(type + "Label");
            if (label != null)
            {
                upgradeLabels[type] = label;
            }
        }
    }

    //Helper Function for Grabbing TMPUGUI components
    private TextMeshProUGUI GetLabel(string name)
    {
        Transform child = mainCanvas.transform.Find(name);
        if (child == null)
        {
            Debug.LogError("Child label couldn't be found");
            return null;
        }
        return child.GetComponent<TextMeshProUGUI>();
    }

    //Change Upgrade Label Method
    public void changeUpgradeLables()
    {
        foreach(UpgradeType type in upgradeLabels.Keys)
        {
            upgradeLabels[type].text = upgradeManager.GetUpgradeCount(type).ToSafeString();
        }
    }

    //Change Non-Upgrade Label Methods
    public void changeBombLabel(float fuse)
    {
        bombLabel.text = "" + fuse;
    }
    public void changeHealthLabel(int health)
    {
        string origText = healthLabel.text;
        int delimIndex = origText.IndexOf("/");

        healthLabel.text = "Health: " + health + origText.Substring(delimIndex);
    }
    public void changeHealthLabel(int health, int maxHealth)
    {
        healthLabel.text = "Health: " + health + "/" + maxHealth;
    }
    public void changeCoinLabel(int amnt)
    {
        coinLabel.text = FormatNumber(amnt);
        shopCoinLabel.text = FormatNumber(amnt);
    }

    //Formatting Helper Method
    public static string FormatNumber(long value)
    {
        if (value >= 1_000_000)
        {
            return (value / 1_000_000f).ToString("0.#") + "M";
        }
        if (value >= 1_000)
        {
            return value.ToString("N0");
        }
        return value.ToString();
    }
}
