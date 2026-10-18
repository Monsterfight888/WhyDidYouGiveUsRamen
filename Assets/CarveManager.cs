using UnityEngine;

public class CarveManager : MonoBehaviour
{
   public SpriteRenderer wood;
   public Sprite[] stages;       // log_0 ... log_6
   public Dialogue dialogue;
   public int pressesPerBite = 2;

   int stage;
   int presses;

   void Start()
   {
      wood.sprite = stages[0];
      dialogue.Say("Press E to carve!");
   }

   void Update()
   {
      if (stage >= stages.Length - 1) return;   // already finished

      if (Input.GetKeyDown(KeyCode.E))
      {
         presses++;
         if (presses >= pressesPerBite)
         {
            presses = 0;
            stage++;
            wood.sprite = stages[stage];
            if (stage == stages.Length - 1)
               dialogue.Say("You carved a bowl!");
         }
      }
   }
}