using UnityEngine;

public class CameraZone : MonoBehaviour
{
    [SerializeField] private CameraFollow.CameraMode cameraMode;
    [SerializeField] private Transform cameraPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CameraFollow cameraFollow =
                Camera.main.GetComponent<CameraFollow>();

            if (cameraFollow != null)
            {
                Vector3 position = transform.position;

                if (cameraPosition != null)
                {
                    position = cameraPosition.position;
                }

                cameraFollow.SetMode(
                    cameraMode,
                    position
                );
            }
        }
    }
}