using UnityEngine;
using MG_BlocksEngine2.Block.Instruction;

public class BE2_Cst_SetAnimal :
    BE2_InstructionBase,
    I_BE2_Instruction
{
    StoryAnimalManager manager;

    public new void Function()
    {
        if (manager == null)
        {
            manager =
                Object.FindFirstObjectByType<StoryAnimalManager>();
        }

        if (manager == null)
        {
            Debug.LogWarning("No StoryAnimalManager found.");
            ExecuteNextInstruction();
            return;
        }

        if (Section0Inputs.Length < 2)
        {
            Debug.LogWarning(
                "SetAnimal block needs Animal Slot and Animal Name inputs."
            );

            ExecuteNextInstruction();
            return;
        }

        string animalSlot =
            Section0Inputs[0].StringValue;

        string animalName =
            Section0Inputs[1].StringValue;

        manager.SetAnimal(animalSlot, animalName);

        ExecuteNextInstruction();
    }
}