using UnityEngine;
using MG_BlocksEngine2.Block.Instruction;
using Polyperfect.People;

public class BE2_Cst_PlaceThing : BE2_InstructionBase, I_BE2_Instruction
{
    StoryPositionManager positionManager;
    StoryCharacterManager characterManager;
    StoryAnimalManager animalManager;
    StoryVehicleManager vehicleManager;

    public new void Function()
    {
        if (positionManager == null)
            positionManager = Object.FindFirstObjectByType<StoryPositionManager>();

        if (characterManager == null)
            characterManager = Object.FindFirstObjectByType<StoryCharacterManager>();

        if (animalManager == null)
            animalManager = Object.FindFirstObjectByType<StoryAnimalManager>();

        if (vehicleManager == null)
            vehicleManager = Object.FindFirstObjectByType<StoryVehicleManager>();

        string category = Section0Inputs[0].StringValue.Trim();
        string number = Section0Inputs[1].StringValue.Trim();
        string locationName = Section0Inputs[2].StringValue.Trim();

        Transform target = GetTarget(category, number);
        Transform location = positionManager.GetPosition(locationName);

        if (target != null && location != null)
        {
            target.position = location.position;
            target.rotation = location.rotation;
        }

        ExecuteNextInstruction();
    }

    Transform GetTarget(string category, string number)
    {
        string slotName = category + " " + number;

        switch (category)
        {
            case "Actor":
                return characterManager != null ? characterManager.GetActorTransform(slotName) : null;

            case "Animal":
                return animalManager != null ? animalManager.GetAnimalTransform(slotName) : null;

            case "Vehicle":
                return vehicleManager != null ? vehicleManager.GetVehicleTransform(slotName) : null;
        }

        return null;
    }
}