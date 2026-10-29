using UnityEngine;


public class Berry : MonoBehaviour, ITakeDmg
{
    //Declare pertinent variables and managers
    public int health;
    public int maxHealth;
    public BerryType berryType;
    public bool isFertilized = false;

    GameObject gameManager;
    private DamageManager damageManager;
    

    //Initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        damageManager = gameManager.GetComponent<DamageManager>();
    }

    //Damage function call when berry is clicked with help from our interface
    public void takeDamage() {
        damageManager.Damage(ref health, gameObject);
    }
}
