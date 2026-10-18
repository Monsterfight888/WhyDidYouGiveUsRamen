using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressionManager : MonoBehaviour
{
    public static ProgressionManager singleton;

    public bool grove = false;
    public bool drumheller = false;
    public bool quad = false;
    private void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(this);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject); //only ONE!
        }


    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "3-outside-hub")
        {
            if (grove && drumheller /*&& quad*/)
            {
                GameObject.Find("toInside").GetComponent<InteractSceneChange>().sceneName = "6-After Finishing";
            } else if (grove || quad || drumheller)
            {
                GameObject.Find("toInside").GetComponent<InteractSceneChange>().sceneName = "2.X-Partial Completion";
            }
        }
    }


}
