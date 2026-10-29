using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

//Script to be put in every purchasable upgrade button
public class UpgradeNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //Declare Managers and data
    public ShopUpgradeData data;
    GameObject gameManager;
    private ShopManager shopManager;
    private TextMeshProUGUI maxText;

    //Initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        shopManager = gameManager.GetComponent<ShopManager>();
        maxText = GetComponentInChildren<TextMeshProUGUI>(true);
    }

    //Format Helper
    private string FormatStatString()
    {
        if (data.isPercentage)
        {
            return "Total: +" + shopManager.GetPlayerStatAmount(data) + "%";
        }
        else
        {
            return "Total: +" + shopManager.GetPlayerStatAmount(data);
        }
    }

    //Unity built in methods, using them to show/hide tooltips
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (data.currentLevel >= data.levelCosts.Length)
        {
            ShopTooltip.Instance.Show(data.upgradeName, data.description, data.levelCosts[data.levelCosts.Length - 1], FormatStatString());
        }
        else
        {
            ShopTooltip.Instance.Show(data.upgradeName, data.description, data.levelCosts[data.currentLevel], FormatStatString());
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        ShopTooltip.Instance.Hide();
    }

    //Button Clicked function, tries to purchase the upgrade
    public void Clicked()
    {
        shopManager.Purchase(data, this);
        if (data.currentLevel < data.levelCosts.Length)
        {
            ShopTooltip.Instance.Show(data.upgradeName, data.description, data.levelCosts[data.currentLevel], FormatStatString());
        }
    }

    //Displays the red "maxed" text when you maxed out an upgrade
    public void ShowMaxed()
    {
        maxText.gameObject.SetActive(true);
    }

}
