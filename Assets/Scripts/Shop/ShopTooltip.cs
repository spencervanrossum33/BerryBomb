using TMPro;
using UnityEngine;

public class ShopTooltip : MonoBehaviour
{
    //Declaring the various text that makes up the tooltip
    public static ShopTooltip Instance;
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private TMP_Text statText;
    
    //Initialize and make tooltip invisible
    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    //Show tooltip when hovering over button
    public void Show(string title, string description, int cost, string stat)
    {
        titleText.text = title;
        descriptionText.text = description;
        costText.text = cost.ToString() + " coins";
        statText.text = stat;

        panel.SetActive(true);
    }

    //Hide tooltip when stop hovering over button
    public void Hide()
    {
        panel.SetActive(false);
    }
}
