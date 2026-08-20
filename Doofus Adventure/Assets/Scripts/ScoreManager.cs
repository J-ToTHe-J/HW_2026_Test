using UnityEngine;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score = 0;
    private HashSet<Pulpit> landedPulpits = new HashSet<Pulpit>();

    public TMPro.TextMeshProUGUI scoreText;

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
        if (scoreText != null)
            scoreText.text = "Score: 0";
    }

    public void RegisterLanding(Pulpit pulpit)
    {
        if (!landedPulpits.Contains(pulpit))
        {
            landedPulpits.Add(pulpit);
            score++;
            if (scoreText != null)
                scoreText.text = "Score: " + score;
        }
    }

    public void ResetScore()
    {
        score = 0;
        landedPulpits.Clear();
        if (scoreText != null)
            scoreText.text = "Score: 0";
    }
}