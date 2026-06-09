using UnityEngine;
using MG_BlocksEngine2.Block.Instruction;
using Polyperfect.People;

public class BE2_Cst_SetEnvironment : BE2_InstructionBase, I_BE2_Instruction
{
    StoryCharacterManager manager;

    public new void Function()
    {
        if (manager == null)
            manager = Object.FindAnyObjectByType<StoryCharacterManager>();

        if (manager == null)
        {
            Debug.LogWarning("No StoryCharacterManager found in scene.");
            ExecuteNextInstruction();
            return;
        }

        string environment =
            Section0Inputs[0].StringValue;

        manager.SelectEnvironmentByName(environment);

        ExecuteNextInstruction();
    }
}
