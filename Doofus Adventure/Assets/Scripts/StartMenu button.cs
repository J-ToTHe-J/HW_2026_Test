
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenubutton : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadSceneAsync("GameScene");
    }
    public void Instructions()
    {
        SceneManager.LoadSceneAsync("Instructions");
    }
}
