using System.Collections.Generic;
using UnityEngine;

namespace Polyperfect.People
{
    public class StoryCharacterManager : MonoBehaviour
    {
        [Header("Actor Animators")]
        public Animator actor1Animator;
        public Animator actor2Animator;
        public Animator actor3Animator;
        public Animator actor4Animator;

        [Header("Character Mesh Roots")]
        public GameObject actor1Rig;
        public GameObject actor2Rig;
        public GameObject actor3Rig;
        public GameObject actor4Rig;

        [Header("Environments")]
        public List<GameObject> environments = new List<GameObject>();

        [Header("Settings")]
        public float animationBlendSpeed = 0.2f;

string MapActorAnimation(string animationName)
{
    switch (animationName)
    {
        case "idle":
        case "Idle":
            return "Idle_Generic";

        case "walk":
        case "Walk":
            return "walk";

        case "run":
        case "Run":
            return "Ninja_Run";

        case "attack":
        case "Attack":
            return "Boxer_Punch_1";

        case "death":
        case "Death":
            return "Laying_Down_Sleeping";

        default:
            return animationName;
    }
}

        public void AssignCharacterToActor(string actorSlot, string characterName)
        {
            GameObject rig = GetActorRig(actorSlot);

            if (rig == null)
            {
                Debug.LogWarning("No rig found for " + actorSlot);
                return;
            }

            SkinnedMeshRenderer[] meshes =
                rig.GetComponentsInChildren<SkinnedMeshRenderer>(true);

            bool found = false;

            foreach (var mesh in meshes)
            {
                bool isSelected = mesh.name == characterName;
                mesh.gameObject.SetActive(isSelected);

                if (isSelected)
                    found = true;
            }

            if (!found)
                Debug.LogWarning("Character not found: " + characterName);
        }

        public void PlayStoryAnimationOnActor(string actorSlot, string friendlyName)
        {
            Animator animator = GetActorAnimator(actorSlot);

            if (animator == null)
            {
                Debug.LogWarning("No animator found for " + actorSlot);
                return;
            }

            string animationName = GetHumanAnimationName(friendlyName);

            animator.enabled = true;
            animator.CrossFade(animationName, animationBlendSpeed);
        }

        public void SelectEnvironmentByName(string environmentName)
        {
            if (environments == null || environments.Count == 0)
            {
                Debug.LogWarning("No environments assigned.");
                return;
            }

            bool found = false;

            foreach (var environment in environments)
            {
                bool isSelected = environment.name == environmentName;
                environment.SetActive(isSelected);

                if (isSelected)
                    found = true;
            }

            if (!found)
                Debug.LogWarning("Environment not found: " + environmentName);
        }

        public Transform GetActorTransform(string actorSlot)
        {
            GameObject rig = GetActorRig(actorSlot);
            return rig != null ? rig.transform : null;
        }

        Animator GetActorAnimator(string actorSlot)
        {
            switch (actorSlot)
            {
                case "Actor 1": return actor1Animator;
                case "Actor 2": return actor2Animator;
                case "Actor 3": return actor3Animator;
                case "Actor 4": return actor4Animator;
                default:
                    Debug.LogWarning("Unknown actor slot: " + actorSlot);
                    return null;
            }
        }

       GameObject GetActorRig(string actorSlot)

{

    switch (actorSlot)

    {

        case "Actor 1":

        case "1":

            return actor1Rig;

        case "Actor 2":

        case "2":

            return actor2Rig;

        case "Actor 3":

        case "3":

            return actor3Rig;

        case "Actor 4":

        case "4":

            return actor4Rig;

        default:

            Debug.LogWarning("Unknown actor slot: " + actorSlot);

            return null;

    }

}

        string GetHumanAnimationName(string friendlyName)
        {
            switch (friendlyName)
            {
                case "Idle": return "Idle_Generic";
                case "Stand": return "Standing_Idle";
                case "Walk": return "Walking";
                case "Run": return "Ninja_Run";
                case "Wave": return "Idle_Waving";
                case "Dance": return "Standing_Dancing";
                case "Talk": return "Standing_Talking";
                case "Sit": return "Sitting_Sofa_Idle";
                case "Sleep": return "Laying_Down_Sleeping";
                default: return friendlyName;
            }
        }
    }
}