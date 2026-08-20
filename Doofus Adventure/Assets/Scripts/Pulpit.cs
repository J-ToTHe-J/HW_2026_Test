using UnityEngine;
using TMPro;

public class Pulpit : MonoBehaviour
{
    public float lifeTime;
    public float spawnTriggerTime;

    private bool hasTriggeredSpawn = false;
    private bool isOccupied = false;

    public TextMeshPro timerText; // child object, world-space TMP text

    public event System.Action<Pulpit> OnShouldSpawnNext;
    public event System.Action<Pulpit> OnExpired;

    void Update()
    {
        lifeTime -= Time.deltaTime;

        if (timerText != null)
            timerText.text = Mathf.Max(lifeTime, 0f).ToString("F2");

        if (!hasTriggeredSpawn && lifeTime <= spawnTriggerTime)
        {
            hasTriggeredSpawn = true;
            OnShouldSpawnNext?.Invoke(this);
        }

        if (lifeTime <= 0f)
        {
            OnExpired?.Invoke(this);
            Destroy(gameObject);
        }
    }

    // Called by Doofus when it steps onto this pulpit
    public void SetOccupied(bool occupied)
    {
        isOccupied = occupied;
    }

    public bool IsOccupied => isOccupied;
}