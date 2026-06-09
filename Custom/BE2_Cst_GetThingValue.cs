using System.Globalization;
using UnityEngine;
using MG_BlocksEngine2.Block.Instruction;
using Polyperfect.People;

public class BE2_Op_GetThingValue : BE2_InstructionBase, I_BE2_Instruction
{
    StoryCharacterManager characterManager;
    StoryAnimalManager animalManager;
    StoryVehicleManager vehicleManager;

    public new string Operation()
    {
        if (characterManager == null)
            characterManager =
                Object.FindFirstObjectByType<StoryCharacterManager>();

        if (animalManager == null)
            animalManager =
                Object.FindFirstObjectByType<StoryAnimalManager>();

        if (vehicleManager == null)
            vehicleManager =
                Object.FindFirstObjectByType<StoryVehicleManager>();

        string valueType =
            Section0Inputs[0].StringValue;

        string category =
            Section0Inputs[1].StringValue;

        string number =
            Section0Inputs[2].StringValue;

        Transform target =
            GetTargetTransform(category, number);

        if (target == null)
        {
            Debug.LogWarning(
                "GetThingValue target not found: "
                + category + " " + number
            );

            return "0";
        }

        float result = 0;

        switch (valueType)
        {
            case "X":
                result = target.position.x;
                break;

            case "Y":
                result = target.position.y;
                break;

            case "Z":
                result = target.position.z;
                break;

            case "Rotation X":
                result = target.eulerAngles.x;
                break;

            case "Rotation Y":
                result = target.eulerAngles.y;
                break;

            case "Rotation Z":
                result = target.eulerAngles.z;
                break;
        }

        return result.ToString(CultureInfo.InvariantCulture);
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