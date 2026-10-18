using UnityEngine;
using UnityEngine.SceneManagement;

public class CarveLog : Interactable
{
   public Sprite[] stages;              // log_0 ... log_6
   public int pressesPerBite = 2;
   public DialogueInteraction doneDialogue;
   public string sceneToLoad = "3-outside-hub";

   SpriteRenderer sr;
   int stage, presses;

   void Awake()
   {
      sr = GetComponent<SpriteRenderer>();
      sr.sprite = stages[0];
   }

   public override void Interact(TopDownPlayer player)
   {
      if (stage >= stages.Length - 1)
      {
         DialogueBox.singleton.DialogueInteract(doneDialogue, TransitionToNextScene);
         return;
      }

      presses++;
      if (presses >= pressesPerBite)
      {
         presses = 0;
         stage++;
         sr.sprite = stages[stage];
         if (stage == stages.Length - 1)
            DialogueBox.singleton.DialogueInteract(doneDialogue, TransitionToNextScene);
      }
   }

   void TransitionToNextScene()
   {
      if (ProgressionManager.singleton != null)
         ProgressionManager.singleton.quad = true;  
      SceneManager.LoadScene(sceneToLoad);
   }
}