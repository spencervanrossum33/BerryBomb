using UnityEngine;

public class Dud : MonoBehaviour, IClickable
{
    //Declare Managers and Bomb
    GameObject gameManager;
    private DamageManager damageManager;
    private UpgradeManager upgradeManager;

    private GameObject bomb;
    private Bomb bombScript;

    //Initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        upgradeManager = gameManager.GetComponent<UpgradeManager>();
        damageManager = gameManager.GetComponent<DamageManager>();
        bomb = GameObject.Find("Bomb");
        bombScript = bomb.GetComponent<Bomb>();
    }

    //Main Function
    public void OnClick()
    {
        //Have to check to see if we have any duds before activating and removing one
        if (upgradeManager.GetUpgradeList().Exists(u => u.type == UpgradeType.Dud))
        {
            bombScript.bombFuse += 150f;
            bombScript.labelTunnel();
            upgradeManager.RemoveUpgrade(UpgradeType.Dud);
        }
    }
}
