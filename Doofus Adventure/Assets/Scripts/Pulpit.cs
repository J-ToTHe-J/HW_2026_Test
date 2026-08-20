using UnityEngine;

public class Pulpit : MonoBehaviour
{
    public float lifeTime;
    public float spawnTriggerTime;

    private bool hasTriggeredSpawn = false;
    private bool isOccupied = false;

    public event System.Action<Pulpit> OnShouldSpawnNext;
    public event System.Action<Pulpit> OnExpired;

    void Update()
    {
        lifeTime -= Time.deltaTime;

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

    public void SetOccupied(bool occupied)
    {
        isOccupied = occupied;
    }

    public bool IsOccupied => isOccupied;
}