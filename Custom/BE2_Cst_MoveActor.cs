using UnityEngine;
using MG_BlocksEngine2.Block.Instruction;
using Polyperfect.People;

public class BE2_Cst_MoveActor : BE2_InstructionBase, I_BE2_Instruction
{
    StoryCharacterManager characterManager;
    StoryAnimalManager animalManager;
    StoryVehicleManager vehicleManager;

    public new void Function()
    {
        if (Section0Inputs.Length < 4)
        {
            Debug.LogWarning("Move block needs Type, Number, Axis, Amount.");
            ExecuteNextInstruction();
            return;
        }

        if (characterManager == null)
            characterManager = Object.FindFirstObjectByType<StoryCharacterManager>();

        if (animalManager == null)
            animalManager = Object.FindFirstObjectByType<StoryAnimalManager>();

        if (vehicleManager == null)
            vehicleManager = Object.FindFirstObjectByType<StoryVehicleManager>();

        string category = Section0Inputs[0].StringValue;
        string number = Section0Inputs[1].StringValue;
        string moveType = Section0Inputs[2].StringValue;
        float amount = Section0Inputs[3].FloatValue;

        Transform target = GetTargetTransform(category, number);

        if (target == null)
        {
            Debug.LogWarning("Move target not found: " + category + " " + number);
            ExecuteNextInstruction();
            return;
        }

        Vector3 moveVector = Vector3.zero;

        switch (moveType)
        {
            case "Move":
                moveVector = target.forward * amount;
                break;

            case "X":
                moveVector = Vector3.right * amount;
                break;

            case "Y":
                moveVector = Vector3.up * amount;
                break;

            case "Z":
                moveVector = Vector3.forward * amount;
                break;

            default:
                Debug.LogWarning("Unknown move type: " + moveType);
                break;
        }

        target.position += moveVector;

        ExecuteNextInstruction();
    }

    Transform GetTargetTransform(string category, string number)
    {
        string slotName = category + " " + number;

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