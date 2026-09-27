using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidoBullerFour : MonoBehaviour
{
    [SerializeField] private float second = 0;
    [SerializeField] private float speed = 0;
    [SerializeField] private GameObject needle;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FourBullet());
    }

    private IEnumerator FourBullet()
    {
        yield return new WaitForSeconds(second);

        for (int i = 0; i < 4; i++)
        {
            float angle = i * 90f;

            GameObject bullet = Instantiate(
                needle,
                transform.position,
                Quaternion.Euler(0f, 0f, angle)
            );

            AcidoBullet bulletScript =
                bullet.GetComponent<AcidoBullet>();

            if (bulletScript != null)
            {
                Vector2 direction = new Vector2(
                    Mathf.Cos(angle * Mathf.Deg2Rad),
                    Mathf.Sin(angle * Mathf.Deg2Rad)
                );

                bulletScript.SetDirection(direction, speed);
            }
        }

        Destroy(gameObject);
    }
}
