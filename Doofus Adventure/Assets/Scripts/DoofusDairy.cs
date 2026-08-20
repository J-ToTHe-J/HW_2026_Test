[System.Serializable]
public class PlayerData
{
    public float speed;
}

[System.Serializable]
public class PulpitData
{
    public float min_pulpit_destroy_time; // y
    public float max_pulpit_destroy_time; // z
    public float pulpit_spawn_time;       // x — time remaining when next pulpit spawns
}

[System.Serializable]
public class DoofusDiary
{
    public PlayerData player_data;
    public PulpitData pulpit_data;
}