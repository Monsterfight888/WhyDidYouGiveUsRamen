using UnityEngine;

public class InteractDialogue : Interactable
{
    public DialogueInteraction thisInteraction;
    public override void Interact(TopDownPlayer interacted)
    {
        DialogueBox.singleton.DialogueInteract(thisInteraction);
    }
}
