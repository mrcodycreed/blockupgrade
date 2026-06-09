using UnityEngine;
using MG_BlocksEngine2.Block.Instruction;
using Polyperfect.People;

public class BE2_Cst_SayDialogue :
    BE2_InstructionBase,
    I_BE2_Instruction
{
    public GameObject textEffectPrefab;

    public Vector3 offset = new Vector3(0, 2.5f, 0);
    public float textSize = 1f;
    public float defaultLifetime = 3f;
    public Color textColor = Color.white;

    StoryCharacterManager characterManager;
    StoryAnimalManager animalManager;
    StoryVehicleManager vehicleManager;

    public new void Function()
    {
        FindManagers();

        string category = Section0Inputs[0].StringValue.Trim();
        string number = Section0Inputs[1].StringValue.Trim();
        string message = Section0Inputs[2].StringValue.Trim();

        float duration = defaultLifetime;

        if (Section0Inputs.Length > 3)
        {
            duration = Section0Inputs[3].FloatValue;

            if (duration <= 0)
                duration = defaultLifetime;
        }

        Transform target = GetTarget(category, number);

        if (target == null || textEffectPrefab == null)
        {
            ExecuteNextInstruction();
            return;
        }

        GameObject effect = Object.Instantiate(
            textEffectPrefab,
            target.position + offset,
            Quaternion.identity
        );

        SetParticleDuration(effect, duration);

        FloatingDialogueText floatingText =
            effect.GetComponent<FloatingDialogueText>();

        if (floatingText == null)
            floatingText = effect.GetComponentInChildren<FloatingDialogueText>(true);

        if (floatingText != null)
            floatingText.Say(message, textSize, textColor);

        Object.Destroy(effect, duration + 0.25f);

        ExecuteNextInstruction();
    }

    void FindManagers()
    {
        if (characterManager == null)
            characterManager = Object.FindFirstObjectByType<StoryCharacterManager>();

        if (animalManager == null)
            animalManager = Object.FindFirstObjectByType<StoryAnimalManager>();

        if (vehicleManager == null)
            vehicleManager = Object.FindFirstObjectByType<StoryVehicleManager>();
    }

    void SetParticleDuration(GameObject effect, float duration)
    {
        ParticleSystem[] particles =
            effect.GetComponentsInChildren<ParticleSystem>(true);

        foreach (ParticleSystem ps in particles)
        {
            var main = ps.main;

            main.duration = duration;
            main.startLifetime = duration;
        }
    }

    Transform GetTarget(string category, string number)
    {
        string slotName = category + " " + number;

        switch (category)
        {
            case "Actor":
                return characterManager != null
                    ? characterManager.GetActorTransform(slotName)
                    : null;

            case "Animal":
                return animalManager != null
                    ? animalManager.GetAnimalTransform(slotName)
                    : null;

            case "Vehicle":
                return vehicleManager != null
                    ? vehicleManager.GetVehicleTransform(slotName)
                    : null;
        }

        return null;
    }
}