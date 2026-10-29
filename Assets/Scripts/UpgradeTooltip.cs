using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTooltip : MonoBehaviour
{
    //Declare the text to be displayed for the main upgrade tooltip
    public static UpgradeTooltip Instance;
    [SerializeField] private GameObject panel;
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;

    //Initialize and make tooltip invisible 
    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    //Show tooltip
    public void Show(Sprite sprite, string title, string description)
    {
        icon.sprite = sprite;
        titleText.text = title;
        descriptionText.text = description;

        panel.SetActive(true);
    }

    //Hide tooltip
    public void Hide()
    {
        panel.SetActive(false);
    }
}