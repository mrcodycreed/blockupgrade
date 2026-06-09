using UnityEngine;

public class StoryPositionManager : MonoBehaviour
{
    public Transform[] positions;

    public Transform GetPosition(string positionName)
    {
        string wanted = Clean(positionName);

        foreach (Transform pos in positions)
        {
            if (pos == null)
                continue;

            if (Clean(pos.name) == wanted)
                return pos;
        }

        Debug.LogWarning("Position not found: " + positionName);
        return null;
    }

    string Clean(string value)
    {
        return value.ToLower()
            .Replace(" ", "")
            .Replace("_", "")
            .Replace("-", "");
    }
}