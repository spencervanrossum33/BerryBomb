using UnityEngine;

public class Fertilizer : MonoBehaviour, IClickable
{
    //Declare managers
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
        //Have to check to see if we have any fertilizer before activating and removing one
        if (upgradeManager.GetUpgradeList().Exists(u => u.type == UpgradeType.Fertilizer))
        {
            GameObject berry = FindAnyObjectByType<Berry>().gameObject;
            Berry berryScript = berry.GetComponent<Berry>();
            berryScript.isFertilized = true;
            damageManager.UseFertilizer();
            upgradeManager.RemoveUpgrade(UpgradeType.Fertilizer);
        }
    }
}
