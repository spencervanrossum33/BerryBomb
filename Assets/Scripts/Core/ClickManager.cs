using UnityEngine;

public class ClickManager : MonoBehaviour
{
    //Manager for using our Interfaces to determine if we are clicking things such as berries or usable upgrades
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null)
            {
                hit.collider.GetComponent<ITakeDmg>()?.takeDamage();
                hit.collider.GetComponent<IClickable>()?.OnClick();
            }
        }
    }
}
