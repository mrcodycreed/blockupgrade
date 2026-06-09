using UnityEngine;

public class VehicleSpinController : MonoBehaviour
{
    [System.Serializable]
    public class SpinPart
    {
        public Transform part;
        public Vector3 axis = Vector3.right;
    }

    public SpinPart[] spinParts;

    public bool isSpinning = false;
    public float speed = 360f;
    public int direction = 1; // 1 = forward, -1 = backward

    void Update()
    {
        if (!isSpinning)
            return;

        foreach (SpinPart spinPart in spinParts)
        {
            if (spinPart == null || spinPart.part == null)
                continue;

            Vector3 axis = spinPart.axis;

            if (axis == Vector3.zero)
                axis = Vector3.right;

            spinPart.part.Rotate(
                axis.normalized,
                speed * direction * Time.deltaTime,
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
}