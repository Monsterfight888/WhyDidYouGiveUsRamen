using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct DialogueSprites
{
    public Sprite[] sprites;
}

public class DialogueBox : MonoBehaviour
{
    //to set
    public Text boxText;
    public Text nameText;
    public Image profileImage;

    //image resources
    public DialogueSprites[] dialogueSprites;
    public string[] profileNames; //can replace, not great..


    DialogueInteraction currentInteraction;
    private int progress;
    private int lastProfile = -1;
    private int consecutiveSameProfiles = 0;
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

            if (lastProfile == (int)currentBlurb.profile)
            {
                //same profile as last, increment.
                consecutiveSameProfiles++;
                if (consecutiveSameProfiles >= dialogueSprites[(int)currentBlurb.profile].sprites.Length)
                {
                    consecutiveSameProfiles = 0;
                }
            }
            else
            {
                consecutiveSameProfiles = 0;
            }

            //text set
            boxText.text = currentBlurb.text;
            nameText.text = profileNames[(int)currentBlurb.profile];

            //image set
            profileImage.sprite = dialogueSprites[(int)currentBlurb.profile].sprites[consecutiveSameProfiles];

            

            progress++;
            lastProfile = (int)currentBlurb.profile;
        }
        else if(progress >= currentInteraction.dialogueSet.Length)
        {
            progress = 0;
            currentInteraction = null;
            lastProfile = -1;
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
    James,
    Swarnya,
    Alice,
    Candra,
    Knight,
}