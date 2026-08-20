using UnityEngine;

public static class ConfigLoader
{
    public static DoofusDiary Load()
    {
        TextAsset json = Resources.Load<TextAsset>("DoofusDiary");
        if (json == null)
        {
            Debug.LogError("DoofusDiary.json not found in Resources folder!");
            return null;
        }
        return JsonUtility.FromJson<DoofusDiary>(json.text);
    }
}