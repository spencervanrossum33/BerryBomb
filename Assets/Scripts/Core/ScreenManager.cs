using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    //Declare Canvases and Managers
    public GameObject mainCanvas;
    public GameObject upgradeCanvas;
    public GameObject winnerCanvas;
    private SpawnManager spawnManager;
    private UpgradeManager upgradeManager;
    private ButtonManager buttonManager;
    
    //Initilization
    void Start()
    {
        spawnManager = GetComponentInParent<SpawnManager>();
        upgradeManager = GetComponentInParent<UpgradeManager>();
        buttonManager = GetComponentInParent<ButtonManager>();
    }

    //Changing Screen Methods
    public void ChangeScreenToShop()
    {
        Camera.main.transform.position = new Vector3(2000f, 0f, -10f);
        mainCanvas.SetActive(false);
        upgradeCanvas.SetActive(true);
        upgradeManager.upgradeList.Clear();
        upgradeManager.UpgradeLabelTunnel();
    }
    public void ChangeScreenToMain()
    {
        Camera.main.transform.position = new Vector3(0f, 0f, -10f);
        upgradeCanvas.SetActive(false);
        buttonManager.Reset();
        spawnManager.RespawnBerry();
        spawnManager.RestartBomb();
        mainCanvas.SetActive(true);
    }
    public void ChangeScreenToWINNER()
    {
        Camera.main.transform.position = new Vector3(4000f, 0f, -10f);
        upgradeCanvas.SetActive(false);
        winnerCanvas.SetActive(true);
    }

    //Function for the last button to play the game again
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
