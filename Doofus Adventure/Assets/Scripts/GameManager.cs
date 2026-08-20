using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject gameOverScreen;
    public GameObject gameplayRoot;

    private bool isGameActive = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        gameOverScreen.SetActive(false);
        gameplayRoot.SetActive(true);
        ScoreManager.Instance.ResetScore();
        isGameActive = true;
        PulpitSpawner.Instance.BeginSpawning();
    }

    public void TriggerGameOver()
    {
        if (!isGameActive) return;
        isGameActive = false;
        gameOverScreen.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // exact scene name
    }
}