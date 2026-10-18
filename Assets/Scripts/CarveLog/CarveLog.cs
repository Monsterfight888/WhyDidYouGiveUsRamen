using UnityEngine;

public class CarveLog : Interactable
{
   public Sprite[] stages; // log_0 through log_6
   public int pressesPerBite = 2;
   public DialogueInteraction doneDialogue;

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
         DialogueBox.singleton.DialogueInteract(doneDialogue);
         return;
      }

      presses++;
      if (presses >= pressesPerBite)
      {
         presses = 0;
         stage++;
         sr.sprite = stages[stage];
         if (stage == stages.Length - 1)
            DialogueBox.singleton.DialogueInteract(doneDialogue);
      }
   }
}