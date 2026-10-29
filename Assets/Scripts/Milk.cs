using System.Collections.Generic;
using UnityEngine;

public class Milk : MonoBehaviour, IClickable
{
    //Declare Managers
    GameObject gameManager;
    private DamageManager damageManager;
    private UpgradeManager upgradeManager;
    
    //Initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        damageManager = gameManager.GetComponent<DamageManager>();
        upgradeManager = gameManager.GetComponent<UpgradeManager>();
    }

    //Main Function
    public void OnClick()
    {
        //Have to check to see if we have any milks before activating and removing one
        if (upgradeManager.GetUpgradeList().Exists(u => u.type == UpgradeType.Milk))
        {
            damageManager.ActivateMilk();
            upgradeManager.RemoveUpgrade(UpgradeType.Milk);
        }
    }
}
