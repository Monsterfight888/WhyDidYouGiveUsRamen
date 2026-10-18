using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    //to set
    public Text boxText;
    public Text nameText;
    public Image profileImage;

    //image resources
    public Sprite[] profiles; //can replace, not great..
    public string[] profileNames; //can replace, not great..


    DialogueInteraction currentInteraction;
    private int progress;
    private Animator anim;

    #region singleton
    public static DialogueBox singleton;

    private void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
            Application.targetFrameRate = 60;

        }
        else
        {
            Debug.LogError("MORE THAN ONE DIALOGUE BOX EXITS");
        }
    }
    #endregion

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
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
            anim.SetBool("IsOpen", true);
        }
    }

    public void ContinueDialogue()
    {
        if (currentInteraction != null && progress < currentInteraction.dialogueSet.Length) 
        {
            
            DialogueBlurb currentBlurb = currentInteraction.dialogueSet[progress];
            boxText.text = currentBlurb.text;
            nameText.text = profileNames[(int)currentBlurb.profile];
            profileImage.sprite = profiles[(int)currentBlurb.profile];
            progress++;
        }
        else if(progress >= currentInteraction.dialogueSet.Length)
        {
            progress = 0;
            currentInteraction = null;
            //animate close the dialoguebox
            anim.SetBool("IsOpen", false);
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
    James,
    Swarnya,
    Alice,
    Candra
}