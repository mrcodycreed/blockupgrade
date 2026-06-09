using UnityEngine;
using MG_BlocksEngine2.Block.Instruction;
using Polyperfect.People;

public class BE2_Cst_PlayActorAnimation : BE2_InstructionBase, I_BE2_Instruction
{
    StoryCharacterManager manager;

    public new void Function()
    {
        if (manager == null)
            manager = Object.FindFirstObjectByType<StoryCharacterManager>();

        if (manager == null)
        {
            Debug.LogWarning("No StoryCharacterManager found in scene.");
            ExecuteNextInstruction();
            return;
        }

        string actorSlot = NormalizeActorSlot(Section0Inputs[0].StringValue);
        string animationName = Section0Inputs[1].StringValue;

        manager.PlayStoryAnimationOnActor(actorSlot, animationName);

        ExecuteNextInstruction();
    }

    string NormalizeActorSlot(string value)
    {
        switch (value)
        {
            case "1": return "Actor 1";
            case "2": return "Actor 2";
            case "3": return "Actor 3";
            case "4": return "Actor 4";
            default: return value;
        }
    }
}