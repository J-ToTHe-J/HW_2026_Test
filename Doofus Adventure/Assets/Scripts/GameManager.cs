using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject gameOverScreen;
    public GameObject gameplayRoot;
    public TextMeshProUGUI finalScoreText; // assign in Inspector

    private bool isGameActive = false;
    private bool isGameOverPending = false;

    public bool IsGameActive => isGameActive;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        gameOverScreen.SetActive(false);
        ScoreManager.Instance.ResetScore();
        isGameActive = true;
        isGameOverPending = false;
        PulpitSpawner.Instance.BeginSpawning();
    }

    public void TriggerGameOver()
    {
        if (!isGameActive || isGameOverPending) return;

        isGameOverPending = true;
        isGameActive = false;

        StartCoroutine(DelayedGameOver());
    }

    private System.Collections.IEnumerator DelayedGameOver()
    {
        yield return new WaitForSeconds(0.5f);

        if (finalScoreText != null)
            finalScoreText.text = "Final Score: " + ScoreManager.Instance.score;

        gameOverScreen.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}