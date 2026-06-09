using UnityEngine;
using MG_BlocksEngine2.Block.Instruction;
using Polyperfect.People;

public class BE2_Cst_MoveActorTo : BE2_InstructionBase, I_BE2_Instruction
{
    StoryCharacterManager manager;

    public new void Function()
    {
        // Safety check
        if (Section0Inputs.Length < 4)
        {
            Debug.LogWarning(
                "MoveActorTo requires X, Y, Z, Actor inputs."
            );

            ExecuteNextInstruction();
            return;
        }

        // Find manager
        if (manager == null)
        {
            manager = Object.FindFirstObjectByType<StoryCharacterManager>();
        }

        if (manager == null)
        {
            Debug.LogWarning(
                "No StoryCharacterManager found in scene."
            );

            ExecuteNextInstruction();
            return;
        }

        // New order: X Y Z Actor
        float x = Section0Inputs[0].FloatValue;
        float y = Section0Inputs[1].FloatValue;
        float z = Section0Inputs[2].FloatValue;

        string actorSlot =
            NormalizeActorSlot(
                Section0Inputs[3].StringValue
            );

        // Find actor
        Transform actor =
            manager.GetActorTransform(actorSlot);

        if (actor == null)
        {
            Debug.LogWarning(
                "Could not find actor: " + actorSlot
            );

            ExecuteNextInstruction();
            return;
        }

        // Move actor
        actor.position = new Vector3(x, y, z);

        ExecuteNextInstruction();
    }

    string NormalizeActorSlot(string value)
    {
        switch (value)
        {
            case "1":
                return "Actor 1";

            case "2":
                return "Actor 2";

            case "3":
                return "Actor 3";

            case "4":
                return "Actor 4";

            default:
                return value;
        }
    }
}