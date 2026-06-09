using UnityEngine;

public class StoryVehicleManager : MonoBehaviour
{
    [Header("Vehicle Slots")]
    public Transform vehicle1Slot;
    public Transform vehicle2Slot;
    public Transform vehicle3Slot;
    public Transform vehicle4Slot;

    public void SetVehicle(string vehicleSlot, string vehicleName)
    {
        Transform slot = GetVehicleSlot(vehicleSlot);

        if (slot == null)
        {
            Debug.LogWarning("No vehicle slot found: " + vehicleSlot);
            return;
        }

        string wanted = Clean(vehicleName);
        bool found = false;

        foreach (Transform child in slot)
        {
            bool selected =
                Clean(child.name).Contains(wanted) ||
                wanted.Contains(Clean(child.name));

            child.gameObject.SetActive(selected);

            if (selected)
            {
                found = true;
                Debug.Log("Set " + vehicleSlot + " to " + child.name);
            }
        }

        if (!found)
        {
            Debug.LogWarning("Vehicle not found: " + vehicleName);

            foreach (Transform child in slot)
                Debug.Log("Available vehicle: " + child.name);
        }
    }

    public Transform GetVehicleTransform(string vehicleSlot)
    {
        Transform slot = GetVehicleSlot(vehicleSlot);

        if (slot == null)
            return null;

        foreach (Transform child in slot)
        {
            if (child.gameObject.activeSelf)
                return child;
        }

        return slot;
    }

    Transform GetVehicleSlot(string vehicleSlot)
{
    switch (vehicleSlot)
    {
        case "Vehicle 1":
        case "Vehicle1":
        case "1":
            return vehicle1Slot;

        case "Vehicle 2":
        case "Vehicle2":
        case "2":
            return vehicle2Slot;

        case "Vehicle 3":
        case "Vehicle3":
        case "3":
            return vehicle3Slot;

        case "Vehicle 4":
        case "Vehicle4":
        case "4":
            return vehicle4Slot;

        default:
            Debug.LogWarning("Unknown vehicle slot: " + vehicleSlot);
            return null;
    }
}

    string Clean(string value)
    {
        return value.ToLower()
            .Replace(" ", "")
            .Replace("_", "")
            .Replace("-", "")
            .Replace("(clone)", "");
    }
}