using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public enum CameraMode
    {
        Follow,
        Horizontal,
        Fixed
    }

    [SerializeField] private Transform target;

    private CameraMode mode;
    private Vector3 fixedPosition;

    [SerializeField] private float minY = 0;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPosition = transform.position;

        switch (mode)
        {
            case CameraMode.Follow:
                targetPosition = new Vector3(target.position.x, Mathf.Max(target.position.y, minY), transform.position.z);
                break;

            case CameraMode.Horizontal:
                targetPosition = new Vector3(target.position.x, Mathf.Max(target.position.y, minY), transform.position.z);
                break;

            case CameraMode.Fixed:
                targetPosition = fixedPosition;
                targetPosition.y = Mathf.Max(targetPosition.y, minY);
                break;
        }

        transform.position = targetPosition; ;
    }

    public void SetMode(CameraMode newMode, Vector3 position)
    {
        mode = newMode;

        if (newMode == CameraMode.Fixed)
        {
            fixedPosition = new Vector3(
                position.x,
                Mathf.Max(position.y, minY),
                transform.position.z
            );
        }
    }
}