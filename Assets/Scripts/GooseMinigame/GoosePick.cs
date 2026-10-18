using UnityEngine;

public class GoosePick : Interactable
{
    public float value = 1;
    public override void Start()
    {
        base.Start();
        Destroy(gameObject, 10.0f);
    }
    public override void Interact(TopDownPlayer interacted)
    {
        GooseGamemode.instance.score += value;
        Destroy(gameObject);
    }
}
