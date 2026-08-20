using UnityEngine;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score = 0;
    private HashSet<Pulpit> landedPulpits = new HashSet<Pulpit>();

    public TMPro.TextMeshProUGUI scoreText; // assign in Inspector

    void Awake()
    {
        Instance = this;
    }

    public void RegisterPulpit(Pulpit pulpit, Vector3 pos)
    {
        // optional: track spawn if needed later
    }

    public void RegisterLanding(Pulpit pulpit)
    {
        if (!landedPulpits.Contains(pulpit))
        {
            landedPulpits.Add(pulpit);
            score++;
            if (scoreText != null)
                scoreText.text = score.ToString();
        }
    }

    public void ResetScore()
    {
        score = 0;
        landedPulpits.Clear();
        if (scoreText != null)
            scoreText.text = "0";
    }
}