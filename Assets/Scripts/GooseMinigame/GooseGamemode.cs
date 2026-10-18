using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GooseGamemode : MonoBehaviour
{
    public static GooseGamemode instance;
    public float score = 0;
    public Text scoreText;

    public void Awake()
    {
        instance = this;
        if (ProgressionManager.singleton.grove == true)
        {
            SceneManager.LoadScene("3-outside-hub");

        }
    }
    private void Start()
    {
        scoreText = GetComponent<Text>();
    }

    public void Update()
    {
        scoreText.text = "Eggs: " + score;
        if (score >= 2)
        {
            ProgressionManager.singleton.grove = true;
            SceneManager.LoadScene("3-outside-hub");
        }
    }
}
