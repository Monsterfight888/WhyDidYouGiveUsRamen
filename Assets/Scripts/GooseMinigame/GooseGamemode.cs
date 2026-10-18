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
    }
    private void Start()
    {
        scoreText = GetComponent<Text>();
    }

    private void Update()
    {
        scoreText.text = "Eggs: " + score;
        if (score >= 5)
        {
            SceneManager.LoadScene("");
        }
    }
}
