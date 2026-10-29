using UnityEngine;

//Class that houses data about upgrades that will be displayed when hovered over in the main screen
public class UpgradeHover : MonoBehaviour
{
    [Header("Tooltip Data")]
    public Sprite upgradeIcon;
    public string upgradeName;
    [TextArea]
    public string description;

    //Show tooltip when hovering over upgrade
    private void OnMouseEnter()
    {
        UpgradeTooltip.Instance.Show(
            upgradeIcon,
            upgradeName,
            description
        );
    }

    //Hide tooltip when not hovering over upgrade
    private void OnMouseExit()
    {
        UpgradeTooltip.Instance.Hide();
    }
}
