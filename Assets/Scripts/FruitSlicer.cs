using UnityEngine;

public class FruitSlicer : MonoBehaviour, IClickable
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
        //Have to check to see if we have any fruit slicers before activating and removing one
        if (upgradeManager.GetUpgradeList().Exists(u => u.type == UpgradeType.FruitSlicer))
        {
            damageManager.UseFruitSlicer();
            upgradeManager.RemoveUpgrade(UpgradeType.FruitSlicer);
        }
    }
}
