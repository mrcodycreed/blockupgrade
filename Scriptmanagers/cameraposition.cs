using TMPro;
using UnityEngine;

public class CameraPositionDisplay : MonoBehaviour
{
    public Camera targetCamera;

    private TMP_Text textDisplay;

    void Start()
    {
        textDisplay = GetComponent<TMP_Text>();

        if (targetCamera == null)
            targetCamera = Camera.main;
    }

    void Update()
    {
        if (textDisplay == null)
            return;

        if (targetCamera == null)
        {
            textDisplay.text = "No Camera Found";
            return;
        }

        Vector3 pos = targetCamera.transform.position;

        textDisplay.text =
            "X: " + pos.x.ToString("F1") +
            "  Y: " + pos.y.ToString("F1") +
            "  Z: " + pos.z.ToString("F1");
    }
}