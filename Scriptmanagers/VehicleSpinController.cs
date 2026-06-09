using UnityEngine;

public class VehicleSpinController : MonoBehaviour
{
    [System.Serializable]
    public class SpinPart
    {
        public Transform part;
        public Vector3 axis = Vector3.right;

        [System.NonSerialized]
        public Vector3 normalizedAxis = Vector3.right;
    }

    public SpinPart[] spinParts;

    public bool isSpinning = false;
    public float speed = 360f;
    public int direction = 1; // 1 = forward, -1 = backward

    void Awake()
    {
        CacheSpinAxes();
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        CacheSpinAxes();
    }
#endif

    void Update()
    {
        if (!isSpinning || spinParts == null)
            return;

        float angle = speed * direction * Time.deltaTime;

        foreach (SpinPart spinPart in spinParts)
        {
            if (spinPart == null || spinPart.part == null)
                continue;

            spinPart.part.Rotate(
                spinPart.normalizedAxis,
                angle,
                Space.Self
            );
        }
    }

    public void SetSpin(string spinDirection, float newSpeed)
    {
        isSpinning = true;
        speed = Mathf.Abs(newSpeed);

        string dir = spinDirection.Trim().ToLower();

        if (dir == "backward")
            direction = -1;
        else
            direction = 1;
    }

    public void StopSpin()
    {
        isSpinning = false;
    }

    void CacheSpinAxes()
    {
        if (spinParts == null)
            return;

        foreach (SpinPart spinPart in spinParts)
        {
            if (spinPart == null)
                continue;

            Vector3 axis = spinPart.axis;

            if (axis == Vector3.zero)
                axis = Vector3.right;

            spinPart.normalizedAxis = axis.normalized;
        }
    }
}
