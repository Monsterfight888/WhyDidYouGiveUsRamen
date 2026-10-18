using UnityEngine;

public class GoosePick : Interactable
{
    public float value = 1;
    public override void Interact(TopDownPlayer interacted)
    {
        GooseGamemode.instance.score += value;
        Destroy(gameObject);
    }
}
