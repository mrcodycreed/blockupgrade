using UnityEngine;
using MG_BlocksEngine2.Block.Instruction;
using Polyperfect.People;

public class BE2_Cst_MoveThing :
    BE2_InstructionBase,
    I_BE2_Instruction
{
    public bool enableDebugLogs = false;

    StoryCharacterManager characterManager;
    StoryAnimalManager animalManager;
    StoryVehicleManager vehicleManager;

    public new void Function()
    {
        if (Section0Inputs.Length < 5)
        {
            Debug.LogWarning("MoveThing needs Type, Number, X, Y, and Z.");
            ExecuteNextInstruction();
            return;
        }

        if (characterManager == null)
            characterManager = Object.FindFirstObjectByType<StoryCharacterManager>();

        if (animalManager == null)
            animalManager = Object.FindFirstObjectByType<StoryAnimalManager>();

        if (vehicleManager == null)
            vehicleManager = Object.FindFirstObjectByType<StoryVehicleManager>();

        string category = NormalizeCategory(Section0Inputs[0].StringValue);
        string number = Section0Inputs[1].StringValue.Trim();

        float x = Section0Inputs[2].FloatValue;
        float y = Section0Inputs[3].FloatValue;
        float z = Section0Inputs[4].FloatValue;

        string slotName = category + " " + number;

        Transform target = GetTarget(category, slotName);

        if (enableDebugLogs)
            Debug.Log("MoveThing: " + slotName + " by " + new Vector3(x, y, z));

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
