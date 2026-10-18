using UnityEngine;

public class InteractGoose : Interactable
{
    private Goose toInteract;

    public override void Start()
    {
        base.Start();
        toInteract = GetComponent<Goose>();
    }

    public override void Interact(TopDownPlayer interacted)
    {
        toInteract.Catch();
    }
}
