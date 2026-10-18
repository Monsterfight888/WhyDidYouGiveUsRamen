using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractSceneChange : Interactable
{
    public string sceneName;
    public override void Interact(TopDownPlayer player)
    {
        SceneManager.LoadScene(sceneName);
    }
}
