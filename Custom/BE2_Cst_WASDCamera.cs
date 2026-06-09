using UnityEngine;
using MG_BlocksEngine2.Block.Instruction;
using DevLocker.CameraUtils;

public class BE2_Cst_WASDCamera :
    BE2_InstructionBase,
    I_BE2_Instruction
{
    public float defaultSpeed = 10f;

    public new void Function()
    {
        Camera mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogWarning("WASD Camera: No Main Camera found.");
            ExecuteNextInstruction();
            return;
        }

        FlyCamera flyCamera = mainCamera.GetComponent<FlyCamera>();
        StoryCameraManager storyCameraManager = mainCamera.GetComponent<StoryCameraManager>();

        if (flyCamera == null)
        {
            Debug.LogWarning("WASD Camera: Main Camera does not have FlyCamera.");
            ExecuteNextInstruction();
            return;
        }

        string mode = Section0Inputs[0].StringValue.Trim().ToLower();

        float speed = defaultSpeed;

        if (Section0Inputs.Length > 1)
        {
            float.TryParse(Section0Inputs[1].StringValue.Trim(), out speed);

            if (speed <= 0)
                speed = defaultSpeed;
        }

        flyCamera.MoveSpeed = speed;
        mainCamera.orthographic = false;

        if (mode == "enable")
        {
            flyCamera.enabled = true;

            // IMPORTANT:
            // Do NOT disable StoryCameraManager.
            // Keep it alive so Set Camera Follow can find it later.
            if (storyCameraManager != null)
            {
                storyCameraManager.enabled = true;
                storyCameraManager.target = null;
                storyCameraManager.sceneCamera = mainCamera;
            }
        }
        else
        {
            flyCamera.enabled = false;

            if (storyCameraManager != null)
            {
                storyCameraManager.enabled = true;
                storyCameraManager.sceneCamera = mainCamera;
                storyCameraManager.cameraMode = "3D Follow";
            }
        }

        ExecuteNextInstruction();
    }
}