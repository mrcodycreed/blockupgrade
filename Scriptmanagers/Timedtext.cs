using UnityEngine;
using CartoonFX;

public class FloatingDialogueText : MonoBehaviour
{
    public CFXR_ParticleText dynamicParticleText;

    private Camera targetCamera;

    void Awake()
    {
        if (dynamicParticleText == null)
            dynamicParticleText = GetComponentInChildren<CFXR_ParticleText>(true);
    }

    void Start()
    {
        targetCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (targetCamera == null)
            targetCamera = Camera.main;

        if (targetCamera == null)
            return;

        transform.forward = targetCamera.transform.forward;
    }

    public void Say(string message, float size, Color color)
    {
        if (dynamicParticleText != null)
            dynamicParticleText.UpdateText(message, size, color);
    }
}