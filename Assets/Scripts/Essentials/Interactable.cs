using UnityEngine;

public class Interactable : MonoBehaviour
{
    private SpriteRenderer interactGFX;

    public virtual void Start()
    {
        interactGFX = transform.GetChild(0).GetComponent<SpriteRenderer>(); //first child of interactable is the interact gfx
        interactGFX.enabled = false;
    }
    public void OnHover(bool bisHovering)
    {

        interactGFX.enabled = bisHovering;
        if (bisHovering)
        {
            Debug.Log("E to interact open");
        }
        else
        {
            Debug.Log("E to interact close");

        }
    }

    public virtual void Interact(TopDownPlayer interacted)
    {
        Debug.Log("Interacted!");
    }
}
