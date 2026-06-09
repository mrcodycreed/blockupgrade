using UnityEngine;
using MG_BlocksEngine2.Block.Instruction;

public class BE2_Cst_SetVehicle : BE2_InstructionBase, I_BE2_Instruction
{
    StoryVehicleManager manager;

    public new void Function()
    {
        if (manager == null)
            manager = Object.FindFirstObjectByType<StoryVehicleManager>();

        if (manager == null)
        {
            Debug.LogWarning("No StoryVehicleManager found.");
            ExecuteNextInstruction();
            return;
        }

        if (Section0Inputs.Length < 2)
        {
            Debug.LogWarning("SetVehicle block needs Vehicle Slot and Vehicle Name inputs.");
            ExecuteNextInstruction();
            return;
        }

        string vehicleSlot = Section0Inputs[0].StringValue;
        string vehicleName = Section0Inputs[1].StringValue;

        manager.SetVehicle(vehicleSlot, vehicleName);

        ExecuteNextInstruction();
    }
}