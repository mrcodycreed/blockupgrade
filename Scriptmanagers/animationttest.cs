using UnityEngine;
using Polyperfect.People;

public class AnimationTest : MonoBehaviour
{
    StoryCharacterManager manager;

    void Start()
    {
        manager = FindFirstObjectByType<StoryCharacterManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            manager.PlayStoryAnimationOnActor("Actor 1","Idle");

        if (Input.GetKeyDown(KeyCode.Alpha2))
            manager.PlayStoryAnimationOnActor("Actor 1","Stand");

        if (Input.GetKeyDown(KeyCode.Alpha3))
            manager.PlayStoryAnimationOnActor("Actor 1","Walk");

        if (Input.GetKeyDown(KeyCode.Alpha4))
            manager.PlayStoryAnimationOnActor("Actor 1","Run");

        if (Input.GetKeyDown(KeyCode.Alpha5))
            manager.PlayStoryAnimationOnActor("Actor 1","Wave");

        if (Input.GetKeyDown(KeyCode.Alpha6))
            manager.PlayStoryAnimationOnActor("Actor 1","Talk");

        if (Input.GetKeyDown(KeyCode.Alpha7))
            manager.PlayStoryAnimationOnActor("Actor 1","Dance");

        if (Input.GetKeyDown(KeyCode.Alpha8))
            manager.PlayStoryAnimationOnActor("Actor 1","Sit");

        if (Input.GetKeyDown(KeyCode.Alpha9))
            manager.PlayStoryAnimationOnActor("Actor 1","Sleep");
    }
}