using UnityEngine;

public class Cloud : MonoBehaviour, IClickable
{
    //Declare Managers
    GameObject gameManager;
    private DamageManager damageManager;
    private UpgradeManager upgradeManager;

    //Initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        upgradeManager = gameManager.GetComponent<UpgradeManager>();
        damageManager = gameManager.GetComponent<DamageManager>();
    }

    //Main Function
    public void OnClick()
    {
        //Have to check to see if we have any clouds before activating and removing one
        if (upgradeManager.GetUpgradeList().Exists(u => u.type == UpgradeType.Cloud))
        {
            damageManager.ActivateCloud();
            upgradeManager.RemoveUpgrade(UpgradeType.Cloud);
        }
    }
}
