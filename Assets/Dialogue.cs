using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
   public TMP_Text text;

   public void Say(string s)
   {
      text.text = s;
   }
}