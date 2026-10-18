using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractDialogue : Interactable
{
    public DialogueInteraction thisInteraction;

    // Leave empty for a dialogue with no scene change
    public string sceneToLoad;

    public override void Interact(TopDownPlayer interacted)
    {
        if (string.IsNullOrEmpty(sceneToLoad))
        {
            DialogueBox.singleton.DialogueInteract(thisInteraction);
        }
        else
        {
            DialogueBox.singleton.DialogueInteract(thisInteraction, () => SceneManager.LoadScene(sceneToLoad));
        }
    }
}