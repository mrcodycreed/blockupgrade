using UnityEngine;
using Polyperfect.People;

public class StoryCameraManager : MonoBehaviour
{
    public Camera sceneCamera;
    public StoryCharacterManager characterManager;

public StoryAnimalManager animalManager;

public StoryVehicleManager vehicleManager;

    public Transform target;
    public string cameraMode = "3D Follow";

    [Header("3D Follow")]
    public Vector3 followOffset3D = new Vector3(0, 5, -10);
    public bool rotateWithActor = true;

    [Header("Top Down")]
    public Vector3 topDownOffset = new Vector3(0, 18, -6);
    public Vector3 topDownRotation = new Vector3(65, 0, 0);

    [Header("Side Scroller")]
    public Vector3 sideScrollerOffset = new Vector3(0, 3, -14);
    public float sideDeadZoneX = 2f;
    public float sideDeadZoneY = 1f;
    public float sideOrthographicSize = 6f;

    [Header("Side Scroller Occlusion")]
public bool useSideOcclusion = true;
public float occlusionRadius = 0.5f;
public float occlusionDistance = 20f;
public LayerMask occlusionLayers;

Renderer lastHiddenRenderer;

    [Header("Smoothing")]
    public float positionSmoothness = 6f;
    public float rotationSmoothness = 8f;

    void Awake()
    {
        if (sceneCamera == null)
            sceneCamera = Camera.main;

        if (characterManager == null)
            characterManager = FindFirstObjectByType<StoryCharacterManager>();
    }

    void LateUpdate()
    {
        if (target == null || sceneCamera == null)
            return;

        if (cameraMode == "Top Down")
            UpdateTopDown();
        else if (cameraMode == "Side Scroller")
            UpdateSideScroller();
        else
            Update3DFollow();
    }

public void SetCameraOffsetValue(string setting, float value)
{
    switch (setting)
    {
        case "X":
            followOffset3D.x = value;
            topDownOffset.x = value;
            sideScrollerOffset.x = value;
            break;

        case "Y":
            followOffset3D.y = value;
            topDownOffset.y = value;
            sideScrollerOffset.y = value;
            break;

        case "Z":
            followOffset3D.z = value;
            topDownOffset.z = value;
            sideScrollerOffset.z = value;
            break;

        case "Pitch":
            topDownRotation.x = value;
            break;

        case "Yaw":
            topDownRotation.y = value;
            break;

        case "Roll":
            topDownRotation.z = value;
            break;

        case "Zoom":
            if (sceneCamera != null)
            {
                sceneCamera.orthographicSize = value;
                sceneCamera.fieldOfView = value;
            }

            sideOrthographicSize = value;
            break;

        case "Smoothness":
            positionSmoothness = value;
            break;
    }
}

   public void FollowThing(string category, string number, string mode)

{

    cameraMode = mode;

    if (characterManager == null)

        characterManager = FindFirstObjectByType<StoryCharacterManager>();

    if (animalManager == null)

        animalManager = FindFirstObjectByType<StoryAnimalManager>();

    if (vehicleManager == null)

        vehicleManager = FindFirstObjectByType<StoryVehicleManager>();

    string slotName = category + " " + number;

    if (category == "Actor" && characterManager != null)

        target = characterManager.GetActorTransform(slotName);

    if (category == "Animal" && animalManager != null)

        target = animalManager.GetAnimalTransform(slotName);

    if (category == "Vehicle" && vehicleManager != null)

        target = vehicleManager.GetVehicleTransform(slotName);

    if (sceneCamera == null)

        sceneCamera = Camera.main;

    sceneCamera.orthographic = mode == "Side Scroller";

}

    void Update3DFollow()
    {
        Vector3 offset = followOffset3D;

        if (rotateWithActor)
            offset = target.rotation * followOffset3D;

        Vector3 desiredPosition = target.position + offset;

        sceneCamera.transform.position = Vector3.Lerp(
            sceneCamera.transform.position,
            desiredPosition,
            Time.deltaTime * positionSmoothness
        );

        Quaternion desiredRotation =
            Quaternion.LookRotation(target.position - sceneCamera.transform.position);

        sceneCamera.transform.rotation = Quaternion.Slerp(
            sceneCamera.transform.rotation,
            desiredRotation,
            Time.deltaTime * rotationSmoothness
        );
    }

    void UpdateTopDown()
    {
        Vector3 desiredPosition = target.position + topDownOffset;

        sceneCamera.transform.position = Vector3.Lerp(
            sceneCamera.transform.position,
            desiredPosition,
            Time.deltaTime * positionSmoothness
        );

        Quaternion desiredRotation = Quaternion.Euler(topDownRotation);

        sceneCamera.transform.rotation = Quaternion.Slerp(
            sceneCamera.transform.rotation,
            desiredRotation,
            Time.deltaTime * rotationSmoothness
        );
    }

void HandleOcclusion()
{
    if (!useSideOcclusion || target == null || sceneCamera == null)
        return;

    if (lastHiddenRenderer != null)
    {
        lastHiddenRenderer.enabled = true;
        lastHiddenRenderer = null;
    }

    Vector3 direction = target.position - sceneCamera.transform.position;
    float distance = Mathf.Min(direction.magnitude, occlusionDistance);

    RaycastHit hit;

    if (Physics.SphereCast(
        sceneCamera.transform.position,
        occlusionRadius,
        direction.normalized,
        out hit,
        distance,
        occlusionLayers
    ))
    {
        if (!hit.transform.IsChildOf(target))
        {
            Renderer renderer = hit.collider.GetComponentInParent<Renderer>();

            if (renderer != null)
            {
                renderer.enabled = false;
                lastHiddenRenderer = renderer;
            }
        }
    }
}
    void UpdateSideScroller()
    {
        sceneCamera.orthographic = true;
        sceneCamera.orthographicSize = sideOrthographicSize;

        Vector3 desiredPosition = sceneCamera.transform.position;

        float xDifference = target.position.x - sceneCamera.transform.position.x;
        float yDifference = target.position.y - sceneCamera.transform.position.y;

        if (Mathf.Abs(xDifference) > sideDeadZoneX)
        {
            desiredPosition.x =
                target.position.x - Mathf.Sign(xDifference) * sideDeadZoneX;
        }

        if (Mathf.Abs(yDifference) > sideDeadZoneY)
        {
            desiredPosition.y =
                target.position.y - Mathf.Sign(yDifference) * sideDeadZoneY;
        }

        desiredPosition.z = target.position.z + sideScrollerOffset.z;
        desiredPosition.y += sideScrollerOffset.y;

        sceneCamera.transform.position = Vector3.Lerp(
            sceneCamera.transform.position,
            desiredPosition,
            Time.deltaTime * positionSmoothness
        );

        Quaternion desiredRotation = Quaternion.Euler(0, 0, 0);

        sceneCamera.transform.rotation = Quaternion.Slerp(
            sceneCamera.transform.rotation,
            desiredRotation,
            Time.deltaTime * rotationSmoothness
        );
    }
}