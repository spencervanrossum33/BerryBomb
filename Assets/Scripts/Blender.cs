using UnityEngine;

public class Blender : MonoBehaviour, IClickable
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
        //Have to check to see if we have any blenders before activating and removing one
        if (upgradeManager.GetUpgradeList().Exists(u => u.type == UpgradeType.Blender))
        {
            damageManager.ActivateBlender();
            upgradeManager.RemoveUpgrade(UpgradeType.Blender);
        }
    }
}
