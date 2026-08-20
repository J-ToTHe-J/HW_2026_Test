
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenubutton : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadSceneAsync("GameScene");
    }
}
