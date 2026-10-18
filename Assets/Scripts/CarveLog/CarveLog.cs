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
      switch (state)
      {
         case State.Intro:
            // first E opens it each E advances when it closes, carving starts
            DialogueBox.singleton.DialogueInteract(introDialogue, () => state = State.Carving);
            break;

         case State.Carving:
            presses++;
            if (presses >= pressesPerBite)
            {
               presses = 0;
               stage++;
               sr.sprite = stages[stage];
               if (stage == stages.Length - 1)
               {
                  state = State.Done;
                  if (doneDialogue == null) TransitionToNextScene();
                  else DialogueBox.singleton.DialogueInteract(doneDialogue, TransitionToNextScene);
               }
            }
            break;

         case State.Done:
            if (doneDialogue != null)
               DialogueBox.singleton.DialogueInteract(doneDialogue, TransitionToNextScene);
            break;
      }
   }

   void TransitionToNextScene()
   {
      if (ProgressionManager.singleton != null)
         ProgressionManager.singleton.quad = true;  
      SceneManager.LoadScene(sceneToLoad);
   }
}