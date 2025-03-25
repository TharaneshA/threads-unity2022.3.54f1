using UnityEngine;

public class ChainButtonController : MonoBehaviour
{
    private Animator anim;
    private bool isDown = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Hover - Slightly move down the chain
    void OnMouseEnter()
    {
        if (!isDown)
        {
            anim.SetTrigger("Hover");
        }
    }

    // Reset back to idle when hover ends
    void OnMouseExit()
    {
        if (!isDown)
        {
            anim.SetTrigger("Reset");
        }
    }

    // Click to pull down or reset back up
    void OnMouseDown()
    {
        if (!isDown)
        {
            anim.SetTrigger("Click");  // Pulls down fabric store
            isDown = true;
        }
        else
        {
            anim.SetTrigger("Reset");  // Resets and moves back up
            isDown = false;
        }
    }
}