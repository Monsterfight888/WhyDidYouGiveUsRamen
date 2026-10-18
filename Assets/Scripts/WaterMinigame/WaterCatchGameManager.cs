using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;

public class WaterCatchGameManager : MonoBehaviour
{
    public DialogueInteraction introDialogue;
    public DialogueInteraction outroDialogue;

    public GameObject bowlObject;
    public GameObject dropPrefab;
    public Transform[] spawnPoints; 

    public float spawnInterval = 1.0f;
    public int targetScore = 5;

    private int currentScore = 0;
    private bool gameActive = false;
    private Coroutine spawnRoutine;

    private void Start()
    {
        if (bowlObject.TryGetComponent(out BowlMovement bowl)) bowl.enabled = false;
        
        StartCoroutine(DelayedDialogueStart());
    }

    private IEnumerator DelayedDialogueStart()
    {
        yield return null; 
        
        DialogueBox.singleton.DialogueInteract(introDialogue, StartMiniGame);
    }

    private void Update()
    {
        if (DialogueBox.IsActive && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            DialogueBox.singleton.ContinueDialogue();
        }
        if (ProgressionManager.singleton.drumheller == true)
        {
            SceneManager.LoadScene("3-outside-hub");

        }
    }

    private void StartMiniGame()
    {
        gameActive = true;
        currentScore = 0;
        if (bowlObject.TryGetComponent(out BowlMovement bowl)) bowl.enabled = true;
        
        spawnRoutine = StartCoroutine(SpawnDropsRoutine());
    }

    private IEnumerator SpawnDropsRoutine()
    {
        while (gameActive)
        {
            yield return new WaitForSeconds(spawnInterval);
            if (spawnPoints.Length > 0 && dropPrefab != null)
            {
                Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(dropPrefab, randomPoint.position, Quaternion.identity);
            }
        }
    }

    public void AddScore()
    {
        if (!gameActive) return;

        currentScore++;
        if (currentScore >= targetScore)
        {
            EndMiniGame();
        }
    }

    private void EndMiniGame()
    {
        gameActive = false;
        
        if (spawnRoutine != null) StopCoroutine(spawnRoutine);
        
        if (bowlObject.TryGetComponent(out BowlMovement bowl)) bowl.enabled = false;

        GameObject[] remainingDrops = GameObject.FindGameObjectsWithTag("WaterDrop");
        foreach (GameObject drop in remainingDrops) Destroy(drop);

        DialogueBox.singleton.DialogueInteract(outroDialogue, TransitionToNextScene);
    }

    private void TransitionToNextScene()
    {
        ProgressionManager.singleton.drumheller = true;
        SceneManager.LoadScene("3-outside-hub");
    }
}

