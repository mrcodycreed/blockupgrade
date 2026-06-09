using UnityEngine;
using MG_BlocksEngine2.Block.Instruction;
using Polyperfect.People;

public class BE2_Cst_SetThing : BE2_InstructionBase, I_BE2_Instruction
{
    StoryCharacterManager characterManager;
    StoryAnimalManager animalManager;
    StoryVehicleManager vehicleManager;

    public new void Function()
    {
        if (Section0Inputs.Length < 3)
        {
            Debug.LogWarning("SetThing block needs Type, Number, and Thing.");
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
        string thingName = Section0Inputs[2].StringValue;

        string slotName = category + " " + number;

        switch (category)
        {
            case "Actor":
                if (characterManager != null)
                    characterManager.AssignCharacterToActor(slotName, thingName);
                break;

            case "Animal":
                if (animalManager != null)
                    animalManager.SetAnimal(slotName, thingName);
                break;

            case "Vehicle":
                if (vehicleManager != null)
                    vehicleManager.SetVehicle(slotName, thingName);
                break;

            default:
                Debug.LogWarning("Unknown category: " + category);
                break;
        }

        ExecuteNextInstruction();
    }
}