using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    //to set
    public Text boxText;
    public Image profileImage;

    //image resources
    public Sprite[] profiles; //can replace, not great..


    DialogueInteraction currentInteraction;
    private int progress;

    #region singleton
    public static DialogueBox singleton;

    private void Awake()
    {
        if(singleton == null)
        {
            singleton = this;

        }
        else
        {
            Debug.LogError("MORE THAN ONE DIALOGUE BOX EXITS");
        }
    }
    #endregion

    public void DialogueInteract(DialogueInteraction toInvoke)
    {
        InvokeDialogue(toInvoke);
        ContinueDialogue();
    }

    public void InvokeDialogue(DialogueInteraction toInvoke)
    {
        if(currentInteraction == null)
        {
            currentInteraction = toInvoke;
            //animate open the dialogue box
        }
    }

    public void ContinueDialogue()
    {
        if (currentInteraction != null && progress < currentInteraction.dialogueSet.Length) 
        {
            
            DialogueBlurb currentBlurb = currentInteraction.dialogueSet[progress];
            boxText.text = currentBlurb.text;
            profileImage.sprite = profiles[(int)currentBlurb.profile];
        }
        else if(progress >= currentInteraction.dialogueSet.Length)
        {
            progress = 0;
            currentInteraction = null;
            //animate close the dialoguebox
        }
    }

    
    
}

[System.Serializable]
public struct DialogueBlurb
{
    public string text;
    public DialogueProfile profile;
}

public enum DialogueProfile
{
    Knight,
    Swarnya,
    Alice,
    Candra,
    James
}