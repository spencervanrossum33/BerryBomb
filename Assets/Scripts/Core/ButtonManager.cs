using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    //Declare Managers
    private SpawnManager spawnManager;
    private UIManager uiManager;

    //List and Index Variables
    private BerryType currentBerry;
    private BerryType[] berries;

    //Initialization
    void Start()
    {
        spawnManager = GetComponentInParent<SpawnManager>();
        uiManager = GetComponentInParent<UIManager>();
        berries = (BerryType[])System.Enum.GetValues(typeof(BerryType));
    }

    //Reset function for currentBerry so that when transitioning it correctly restarts on blueberry
    public void Reset()
    {
        currentBerry = berries[0];
    }

    //Left Arrow Logic
    public void ChangeBerryLeftArrow()
    {
        int index = System.Array.IndexOf(berries, currentBerry);
        if (index > 0)
        {
            currentBerry = berries[index - 1];
            spawnManager.RespawnBerry(currentBerry);
        }
    }

    //Right Arrow Logic
    public void ChangeBerryRightArrow()
    {
        int index = System.Array.IndexOf(berries, currentBerry);
        if (index < berries.Length - 1)
        {
            currentBerry = berries[index + 1];
            spawnManager.RespawnBerry(currentBerry);
        }
    }
}
