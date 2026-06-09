using UnityEngine;
using MG_BlocksEngine2.Block.Instruction;
using DevLocker.CameraUtils;

public class BE2_Cst_SetCameraFollow : BE2_InstructionBase, I_BE2_Instruction
{
    StoryCameraManager cameraManager;

    public new void Function()
    {
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            FlyCamera flyCamera = mainCamera.GetComponent<FlyCamera>();

            if (flyCamera != null)
                flyCamera.enabled = false;
        }

        if (cameraManager == null)
            cameraManager = Object.FindFirstObjectByType<StoryCameraManager>();

        if (cameraManager == null)
        {
            Debug.LogWarning("No StoryCameraManager found.");
            ExecuteNextInstruction();
            return;
        }

        cameraManager.enabled = true;

        string mode = Section0Inputs[0].StringValue;
        string category = Section0Inputs[1].StringValue;
        string number = Section0Inputs[2].StringValue;

        cameraManager.FollowThing(category, number, mode);

        ExecuteNextInstruction();
    }
}