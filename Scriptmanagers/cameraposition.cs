using TMPro;
using UnityEngine;

public class CameraPositionDisplay : MonoBehaviour
{
    public Camera targetCamera;
    public float updateInterval = 0.25f;

    TMP_Text textDisplay;
    Transform cameraTransform;
    float nextUpdateTime;
    Vector3 lastDisplayedPosition = new Vector3(float.NaN, float.NaN, float.NaN);

    void Start()
    {
        textDisplay = GetComponent<TMP_Text>();
        CacheCamera();
    }

    void Update()
    {
        if (textDisplay == null)
            return;

        if (targetCamera == null || cameraTransform == null)
            CacheCamera();

        if (targetCamera == null || cameraTransform == null)
        {
            if (textDisplay.text != "No Camera Found")
                textDisplay.text = "No Camera Found";

            return;
        }

        if (Time.unscaledTime < nextUpdateTime)
            return;

        nextUpdateTime = Time.unscaledTime + updateInterval;

        Vector3 pos = cameraTransform.position;
        Vector3 roundedPos = new Vector3(
            Mathf.Round(pos.x * 10f) * 0.1f,
            Mathf.Round(pos.y * 10f) * 0.1f,
            Mathf.Round(pos.z * 10f) * 0.1f
        );

        if (roundedPos == lastDisplayedPosition)
            return;

        lastDisplayedPosition = roundedPos;

        textDisplay.text =
            "X: " + roundedPos.x.ToString("F1") +
            "  Y: " + roundedPos.y.ToString("F1") +
            "  Z: " + roundedPos.z.ToString("F1");
    }

    void CacheCamera()
    {
        if (targetCamera == null)
            targetCamera = Camera.main;

        cameraTransform = targetCamera != null ? targetCamera.transform : null;
    }
}
