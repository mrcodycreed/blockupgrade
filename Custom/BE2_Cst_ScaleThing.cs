using UnityEngine;
using MG_BlocksEngine2.Block.Instruction;

public class BE2_Cst_ScaleThing :
    BE2_InstructionBase,
    I_BE2_Instruction
{
    public new void Function()
    {
        string category =
            Section0Inputs[0].StringValue.Trim();

        string number =
            Section0Inputs[1].StringValue.Trim();

        float scale =
            Section0Inputs[2].FloatValue;

        Transform target =
            GetTarget(category, number);

        if (target == null)
        {
            Debug.LogWarning(
                "Scale target not found: "
                + category + " "
                + number
            );

            ExecuteNextInstruction();
            return;
        }

        target.localScale =
            Vector3.one * scale;

        Debug.Log(
            "Scaled "
            + target.name
            + " to "
            + scale
        );

        ExecuteNextInstruction();
    }

    Transform GetTarget(
        string category,
        string number
    )
    {
        string objectName =
            category + number;

        GameObject found =
            GameObject.Find(objectName);

        if (found != null)
            return found.transform;

        return null;
    }
}