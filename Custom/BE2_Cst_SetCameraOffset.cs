using UnityEngine;
using MG_BlocksEngine2.Block.Instruction;

public class BE2_Cst_SetCameraOffset : BE2_InstructionBase, I_BE2_Instruction
{
    StoryCameraManager cameraManager;

    public new void Function()
    {
        if (cameraManager == null)
            cameraManager = Object.FindFirstObjectByType<StoryCameraManager>();

        if (cameraManager == null)
        {
            Debug.LogWarning("No StoryCameraManager found.");
            ExecuteNextInstruction();
            return;
        }

        string setting = Section0Inputs[0].StringValue;
        float value = Section0Inputs[1].FloatValue;

        cameraManager.SetCameraOffsetValue(setting, value);

        ExecuteNextInstruction();
    }
}