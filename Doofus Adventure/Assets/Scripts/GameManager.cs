using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject startScreen;
    public GameObject gameOverScreen;
    public GameObject gameplayRoot; // parent of Doofus + Spawner, disabled until Play pressed

    private bool isGameActive = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        startScreen.SetActive(true);
        gameOverScreen.SetActive(false);
        gameplayRoot.SetActive(false);
    }

    public void StartGame()
    {
        startScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        gameplayRoot.SetActive(true);
        ScoreManager.Instance.ResetScore();
        isGameActive = true;
    }

    public void TriggerGameOver()
    {
        if (!isGameActive) return;
        isGameActive = false;
        gameplayRoot.SetActive(false);
        gameOverScreen.SetActive(true);
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}