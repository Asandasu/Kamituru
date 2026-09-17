using UnityEngine;

public class CameraZone : MonoBehaviour
{
    [SerializeField]private CameraFollow.CameraMode cameraMode;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CameraFollow cameraFollow =
                Camera.main.GetComponent<CameraFollow>();

            if (cameraFollow != null)
            {
                cameraFollow.SetMode(
                    cameraMode,
                    transform.position
                );
            }
        }
    }
}