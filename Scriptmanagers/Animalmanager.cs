using UnityEngine;

public class StoryAnimalManager : MonoBehaviour
{
    [Header("Animal Slots")]
    public Transform animal1Slot;
    public Transform animal2Slot;
    public Transform animal3Slot;
    public Transform animal4Slot;

    [Header("Settings")]
    public float animationBlendSpeed = 0.2f;

    Animator animal1Animator;
    Animator animal2Animator;
    Animator animal3Animator;
    Animator animal4Animator;

    Transform animal1Active;
    Transform animal2Active;
    Transform animal3Active;
    Transform animal4Active;

    public void SetAnimal(string animalSlot, string animalName)
    {
        Transform slot = GetAnimalSlot(animalSlot);

        if (slot == null)
        {
            Debug.LogWarning("No animal slot found: " + animalSlot);
            return;
        }

        string wanted = Clean(animalName);
        bool found = false;

        foreach (Transform child in slot)
        {
            bool selected =
                Clean(child.name).Contains(wanted) ||
                wanted.Contains(Clean(child.name));

            child.gameObject.SetActive(selected);

            if (selected)
            {
                found = true;

                Animator animator =
                    child.GetComponentInChildren<Animator>(true);

                StoreActiveAnimal(animalSlot, child, animator);

                Debug.Log("Set " + animalSlot + " to " + child.name);
            }
        }

        if (!found)
        {
            Debug.LogWarning("Animal not found: " + animalName);

            foreach (Transform child in slot)
                Debug.Log("Available animal: " + child.name);
        }
    }

    public void PlayAnimalAnimation(string animalSlot, string animationName)
    {
        Animator animator = GetAnimalAnimator(animalSlot);

        if (animator == null)
        {
            Debug.LogWarning("No animal animator found for: " + animalSlot);
            return;
        }

        string stateName = GetAnimationState(animationName);

        if (string.IsNullOrEmpty(stateName))
            return;

        animator.enabled = true;
        animator.CrossFade(stateName, animationBlendSpeed);
    }

    public Transform GetAnimalTransform(string animalSlot)
    {
        Transform active = GetActiveAnimal(animalSlot);

        if (active != null)
            return active;

        Transform slot = GetAnimalSlot(animalSlot);

        return slot;
    }

    Transform GetAnimalSlot(string animalSlot)
    {
        switch (animalSlot)
        {
            case "Animal 1":
            case "Animal1":
            case "1":
                return animal1Slot;

            case "Animal 2":
            case "Animal2":
            case "2":
                return animal2Slot;

            case "Animal 3":
            case "Animal3":
            case "3":
                return animal3Slot;

            case "Animal 4":
            case "Animal4":
            case "4":
                return animal4Slot;

            default:
                Debug.LogWarning("Unknown animal slot: " + animalSlot);
                return null;
        }
    }

    void StoreActiveAnimal(string animalSlot, Transform activeAnimal, Animator animator)
    {
        switch (animalSlot)
        {
            case "Animal 1":
            case "Animal1":
            case "1":
                animal1Active = activeAnimal;
                animal1Animator = animator;
                break;

            case "Animal 2":
            case "Animal2":
            case "2":
                animal2Active = activeAnimal;
                animal2Animator = animator;
                break;

            case "Animal 3":
            case "Animal3":
            case "3":
                animal3Active = activeAnimal;
                animal3Animator = animator;
                break;

            case "Animal 4":
            case "Animal4":
            case "4":
                animal4Active = activeAnimal;
                animal4Animator = animator;
                break;
        }
    }

    Transform GetActiveAnimal(string animalSlot)
    {
        switch (animalSlot)
        {
            case "Animal 1":
            case "Animal1":
            case "1":
                return animal1Active;

            case "Animal 2":
            case "Animal2":
            case "2":
                return animal2Active;

            case "Animal 3":
            case "Animal3":
            case "3":
                return animal3Active;

            case "Animal 4":
            case "Animal4":
            case "4":
                return animal4Active;

            default:
                return null;
        }
    }

    Animator GetAnimalAnimator(string animalSlot)
    {
        switch (animalSlot)
        {
            case "Animal 1":
            case "Animal1":
            case "1":
                return animal1Animator;

            case "Animal 2":
            case "Animal2":
            case "2":
                return animal2Animator;

            case "Animal 3":
            case "Animal3":
            case "3":
                return animal3Animator;

            case "Animal 4":
            case "Animal4":
            case "4":
                return animal4Animator;

            default:
                return null;
        }
    }

    string GetAnimationState(string animationName)
    {
        switch (animationName)
        {
            case "Idle": return "Idle";
            case "Walk": return "Walk";
            case "Run": return "Run";
            case "Attack": return "Attack";
            case "Death": return "Death";
            default: return animationName;
        }
    }

    string Clean(string value)
    {
        return value.ToLower()
            .Replace(" ", "")
            .Replace("_", "")
            .Replace("-", "")
            .Replace("(clone)", "")
            .Replace("legacy", "");
    }
}