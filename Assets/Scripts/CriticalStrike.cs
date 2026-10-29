using TMPro;
using UnityEngine;

//Class for our critical strike text that rises from the berry when the player gets a critical hit
public class CriticalStrike : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 200f;
    [SerializeField] private float lifetime = 0.75f;

    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string value)
    {
        text.text = value;
    }

    //Main Function for moving text upwards until lifetime expires
    private void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }
}