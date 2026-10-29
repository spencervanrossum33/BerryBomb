using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bomb : MonoBehaviour
{
    //Declare pertinent variables, managers, and audio for when bomb explodes
    public float bombFuse = 10f;
    public float maxBombFuse = 300f;
    private float timer = 0f;
    GameObject gameManager;
    private ScreenManager screenManager;
    private UIManager uiManager;
    public AudioSource audioSource;
    public AudioClip audioClip;

    //Initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        screenManager = gameManager.GetComponent<ScreenManager>();
        uiManager = gameManager.GetComponent<UIManager>();
    }

    //Logic for decreasing fuse by 1 each second
    void Update()
    {
        if (bombFuse > 0f)
        {
            timer += Time.deltaTime;
            if (timer >= 1f)
            {
                timer -= 1f;
                bombFuse -= 1f;
                uiManager.changeBombLabel(bombFuse);
                if (bombFuse <= 0f)
                {
                    Explode();
                }
            }
        }
    }

    //Changes Screen and plays noise
    void Explode()
    {
        screenManager.ChangeScreenToShop();
        audioSource.PlayOneShot(audioClip);
    }

    //Using as a means for SpawnManager to instantly change the label so that it doesn't show 0 when changing screens
    public void labelTunnel()
    {
        uiManager.changeBombLabel(bombFuse);
    }
}
