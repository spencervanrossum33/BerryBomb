using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Declare Managers and all Berry Prefabs
    private UIManager uiManager;
    public GameObject bomb;
    private Bomb bombScript;
    private Dictionary<BerryType, GameObject> berryPrefabs;
    [SerializeField] private GameObject blueberryPrefab;
    [SerializeField] private GameObject applePrefab;
    [SerializeField] private GameObject pearPrefab;
    [SerializeField] private GameObject cherryPrefab;
    [SerializeField] private GameObject grapePrefab;
    [SerializeField] private GameObject bananaPrefab;
    [SerializeField] private GameObject strawberryPrefab;
    [SerializeField] private GameObject watermelonPrefab;
    [SerializeField] private GameObject pumpkinPrefab;


    //Initilization
    void Start()
    {
        uiManager = GetComponentInParent<UIManager>();
        bombScript = bomb.GetComponent<Bomb>();
    }

    //Put Berry Prefabs into dictionary for easier lookup using BerryType Enum
    void Awake()
    {
        berryPrefabs = new Dictionary<BerryType, GameObject>
        {
            {BerryType.Blueberry, blueberryPrefab},
            {BerryType.Apple, applePrefab},
            {BerryType.Pear, pearPrefab},
            {BerryType.Cherry, cherryPrefab},
            {BerryType.Grape, grapePrefab},
            {BerryType.Banana, bananaPrefab},
            {BerryType.Strawberry, strawberryPrefab},
            {BerryType.Watermelon, watermelonPrefab},
            {BerryType.Pumpkin, pumpkinPrefab}
        };
    }

    //Used by ScreenManager to respawn berries when transitions happen
    public void RespawnBerry()
    {
        Berry berry = FindAnyObjectByType<Berry>();
        Destroy(berry.gameObject);

        Instantiate(blueberryPrefab, Vector3.zero, Quaternion.identity);

        int maxHealth = blueberryPrefab.GetComponent<Berry>().maxHealth;
        uiManager.changeHealthLabel(maxHealth, maxHealth); //Berries always respawn with max health
    }

    //Used by other Managers to respawn a berry when it's killed or want to swap to a different berry
    public void RespawnBerry(BerryType berryType)
    {
        Berry[] berryScripts = FindObjectsByType<Berry>();
        foreach (Berry berryScript in berryScripts) {
            Destroy(berryScript.gameObject);
        }

        GameObject berry = Instantiate(berryPrefabs[berryType], Vector3.zero, Quaternion.identity);
        int maxHealth = berry.GetComponent<Berry>().maxHealth;
        uiManager.changeHealthLabel(maxHealth, maxHealth);
    }

    //Restart Bomb Method for changing back to main screen
    public void RestartBomb()
    {
        bombScript.bombFuse = 150f;
        bombScript.labelTunnel();
    }
}
