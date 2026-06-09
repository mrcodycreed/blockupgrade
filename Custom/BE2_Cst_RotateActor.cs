using UnityEngine;
using MG_BlocksEngine2.Block.Instruction;
using Polyperfect.People;

public class BE2_Cst_RotateActor : BE2_InstructionBase, I_BE2_Instruction
{
    StoryCharacterManager characterManager;
    StoryAnimalManager animalManager;
    StoryVehicleManager vehicleManager;

    public new void Function()
    {
        if (Section0Inputs.Length < 4)
        {
            Debug.LogWarning(
                "Rotate block needs Type, Number, Axis, Amount."
            );

            ExecuteNextInstruction();
            return;
        }

        if (characterManager == null)
            characterManager =
                Object.FindFirstObjectByType<StoryCharacterManager>();

        if (animalManager == null)
            animalManager =
                Object.FindFirstObjectByType<StoryAnimalManager>();

        if (vehicleManager == null)
            vehicleManager =
                Object.FindFirstObjectByType<StoryVehicleManager>();

        string category =
            Section0Inputs[0].StringValue;

        string number =
            Section0Inputs[1].StringValue;

        string rotateType =
            Section0Inputs[2].StringValue;

        float amount =
            Section0Inputs[3].FloatValue;

        Transform target =
            GetTargetTransform(category, number);

        if (target == null)
        {
            Debug.LogWarning(
                "Rotate target not found: "
                + category + " " + number
            );

            ExecuteNextInstruction();
            return;
        }

        Vector3 rotation = Vector3.zero;

        switch (rotateType)
        {
            case "Yaw":
                rotation = Vector3.up * amount;
                break;

            case "Pitch":
                rotation = Vector3.right * amount;
                break;

            case "Roll":
                rotation = Vector3.forward * amount;
                break;

            default:
                Debug.LogWarning(
                    "Unknown rotation type: "
                    + rotateType
                );
                break;
        }

        target.Rotate(rotation);

        ExecuteNextInstruction();
    }

    Transform GetTargetTransform(
        string category,
        string number
    )
    {
        string slotName =
            category + " " + number;

        switch (category)
        {
            case "Actor":
                if (characterManager != null)
                    return characterManager.GetActorTransform(slotName);
                break;

            case "Animal":
                if (animalManager != null)
                    return animalManager.GetAnimalTransform(slotName);
                break;

            case "Vehicle":
                if (vehicleManager != null)
                    return vehicleManager.GetVehicleTransform(slotName);
                break;
        }

        return null;
    }
}