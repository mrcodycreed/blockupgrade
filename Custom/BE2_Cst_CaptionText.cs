using UnityEngine;
using MG_BlocksEngine2.Block.Instruction;

public class BE2_Cst_Caption :
    BE2_InstructionBase,
    I_BE2_Instruction
{
    CaptionManager captionManager;

    public new void Function()
    {
        if (captionManager == null)
            captionManager = Object.FindFirstObjectByType<CaptionManager>();

        if (captionManager == null)
        {
            ExecuteNextInstruction();
            return;
        }

        string message = Section0Inputs[0].StringValue;
        string position = Section0Inputs[1].StringValue;

        float duration = 3f;

        if (Section0Inputs.Length > 2)
            float.TryParse(Section0Inputs[2].StringValue.Trim(), out duration);

        if (duration <= 0)
            duration = 3f;

        captionManager.ShowCaption(message, position, duration);

        ExecuteNextInstruction();
    }
}