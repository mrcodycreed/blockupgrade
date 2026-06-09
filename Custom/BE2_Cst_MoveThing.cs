using UnityEngine;
using MG_BlocksEngine2.Block.Instruction;
using Polyperfect.People;

public class BE2_Cst_MoveThing :
    BE2_InstructionBase,
    I_BE2_Instruction
{
    StoryCharacterManager characterManager;
    StoryAnimalManager animalManager;
    StoryVehicleManager vehicleManager;

    public new void Function()
    {
        if (characterManager == null)
            characterManager = Object.FindFirstObjectByType<StoryCharacterManager>();

        if (animalManager == null)
            animalManager = Object.FindFirstObjectByType<StoryAnimalManager>();

        if (vehicleManager == null)
            vehicleManager = Object.FindFirstObjectByType<StoryVehicleManager>();

        string category = NormalizeCategory(Section0Inputs[0].StringValue);
        string number = Section0Inputs[1].StringValue.Trim();

        float x = ReadFloat(2);
        float y = ReadFloat(3);
        float z = ReadFloat(4);

        string slotName = category + " " + number;

        Transform target = GetTarget(category, slotName);

        Debug.Log("MoveThing fired");
        Debug.Log("Category: " + category);
        Debug.Log("Slot Name: " + slotName);
        Debug.Log("X: " + x + " Y: " + y + " Z: " + z);
        Debug.Log("Target: " + (target != null ? target.name : "NULL"));

        if (target != null)
            target.position += new Vector3(x, y, z);

        ExecuteNextInstruction();
    }

    string NormalizeCategory(string rawCategory)
    {
        string c = rawCategory.Trim().ToLower();

        if (c == "actor")
            return "Actor";

        if (c == "animal")
            return "Animal";

        if (c == "vehicle")
            return "Vehicle";

        return rawCategory.Trim();
    }

    float ReadFloat(int index)
    {
        if (Section0Inputs.Length <= index)
            return 0f;

        float value = 0f;
        float.TryParse(Section0Inputs[index].StringValue.Trim(), out value);
        return value;
    }

    Transform GetTarget(string category, string slotName)
    {
        switch (category)
        {
            case "Actor":
                return characterManager != null
                    ? characterManager.GetActorTransform(slotName)
                    : null;

            case "Animal":
                return animalManager != null
                    ? animalManager.GetAnimalTransform(slotName)
                    : null;

            case "Vehicle":
                return vehicleManager != null
                    ? vehicleManager.GetVehicleTransform(slotName)
                    : null;
        }

        Debug.LogWarning("Unknown category: " + category);
        return null;
    }
}