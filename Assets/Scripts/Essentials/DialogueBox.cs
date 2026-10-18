using System;
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
    private Action onComplete; // runs once when the dialogue finishes
    private int progress;
    private int lastProfile = -1;
    private int consecutiveSameProfiles = 0;
    private Animator anim;

    public static bool IsActive => singleton != null && singleton.currentInteraction != null;

    #region singleton
    public static DialogueBox singleton;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            Application.targetFrameRate = 60;
        }
        else
        {
            Debug.LogError("MORE THAN ONE DIALOGUE BOX EXISTS");
        }
    }
    #endregion

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void DialogueInteract(DialogueInteraction toInvoke, Action onComplete = null)
    {
        InvokeDialogue(toInvoke, onComplete);
        ContinueDialogue();
    }

    public void InvokeDialogue(DialogueInteraction toInvoke, Action onComplete = null)
    {
        if (currentInteraction == null)
        {
            currentInteraction = toInvoke;
            this.onComplete = onComplete;
            //animate open the dialogue box
            anim.SetBool("IsOpen", true);
        }
    }

    public void ContinueDialogue()
    {
        if (currentInteraction == null) return;

        if (progress < currentInteraction.dialogueSet.Length)
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
        else
        {
            //dialogue finished: reset state first
            progress = 0;
            lastProfile = -1;
            consecutiveSameProfiles = 0;
            currentInteraction = null;

            //animate close the dialoguebox
            anim.SetBool("IsOpen", false);

            //then fire the callback (copy it first so it can't run twice)
            Action callback = onComplete;
            onComplete = null;
            callback?.Invoke();
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
    Evan,
}
