using UnityEngine;
using MG_BlocksEngine2.Block.Instruction;
using Polyperfect.People;

public class BE2_Cst_PlayThingAnimation :
    BE2_InstructionBase,
    I_BE2_Instruction
{
    StoryCharacterManager characterManager;
    StoryAnimalManager animalManager;

    public new void Function()
    {
        if (Section0Inputs.Length < 3)
        {
            Debug.LogWarning("PlayThingAnimation needs Type, Number, and Animation.");
            ExecuteNextInstruction();
            return;
        }

        if (characterManager == null)
            characterManager = Object.FindFirstObjectByType<StoryCharacterManager>();

        if (animalManager == null)
            animalManager = Object.FindFirstObjectByType<StoryAnimalManager>();

        string category = Section0Inputs[0].StringValue.Trim();
        string number = Section0Inputs[1].StringValue.Trim();
        string friendlyAnimation = Section0Inputs[2].StringValue.Trim();

        string slotName = category + " " + number;

        if (category == "Actor")
        {
            if (characterManager == null)
            {
                Debug.LogWarning("No StoryCharacterManager found.");
                ExecuteNextInstruction();
                return;
            }

            characterManager.PlayStoryAnimationOnActor(
                slotName,
                friendlyAnimation
            );

            ExecuteNextInstruction();
            return;
        }

        if (category == "Animal")
        {
            if (animalManager == null)
            {
                Debug.LogWarning("No StoryAnimalManager found.");
                ExecuteNextInstruction();
                return;
            }

            animalManager.PlayAnimalAnimation(
                slotName,
                friendlyAnimation
            );

            ExecuteNextInstruction();
            return;
        }

        Debug.LogWarning("Unknown animation category: " + category);

        ExecuteNextInstruction();
    }
}