using UnityEngine;
using System.Collections.Generic;

public class PulpitSpawner : MonoBehaviour
{
    public GameObject pulpitPrefab;
    public float pulpitSize = 9f;

    private float minLife, maxLife, spawnTriggerTime;
    private Queue<Pulpit> activePulpits = new Queue<Pulpit>();
    private Vector3 lastPos;
    private Vector3 secondLastPos;

    void Start()
    {
        var diary = ConfigLoader.Load();
        minLife = diary.pulpit_data.min_pulpit_destroy_time;
        maxLife = diary.pulpit_data.max_pulpit_destroy_time;
        spawnTriggerTime = diary.pulpit_data.pulpit_spawn_time;

        lastPos = Vector3.zero;
        SpawnPulpit(lastPos);
    }

    void SpawnPulpit(Vector3 pos)
    {
        GameObject go = Instantiate(pulpitPrefab, pos, Quaternion.identity);
        Pulpit pulpit = go.GetComponent<Pulpit>();
        pulpit.lifeTime = Random.Range(minLife, maxLife);
        pulpit.spawnTriggerTime = spawnTriggerTime;

        pulpit.OnShouldSpawnNext += HandleShouldSpawnNext;
        pulpit.OnExpired += HandleExpired;

        activePulpits.Enqueue(pulpit);
        ScoreManager.Instance.RegisterPulpit(pulpit, pos);
    }

    void HandleShouldSpawnNext(Pulpit prev)
    {
        Vector3 nextPos = GetAdjacentPosition(lastPos, secondLastPos);
        secondLastPos = lastPos;
        lastPos = nextPos;
        SpawnPulpit(nextPos);
    }

    void HandleExpired(Pulpit pulpit)
    {
        if (pulpit.IsOccupied)
        {
            GameManager.Instance.TriggerGameOver();
        }
    }

    Vector3 GetAdjacentPosition(Vector3 current, Vector3 previous)
    {
        Vector3[] directions = {
            Vector3.forward, Vector3.back,
            Vector3.left, Vector3.right
        };

        List<Vector3> valid = new List<Vector3>();
        foreach (var dir in directions)
        {
            Vector3 candidate = current + dir * pulpitSize;
            if (candidate != previous) // avoid going straight back
                valid.Add(candidate);
        }

        return valid[Random.Range(0, valid.Count)];
    }
}