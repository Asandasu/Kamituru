using UnityEngine;

public class AcidoBullet : MonoBehaviour
{
    private Vector2 direction;
    private float speed;

    [SerializeField] private int hpAmount = 10;

    public void SetDirection(Vector2 newDirection, float newSpeed)
    {
        direction = newDirection;
        speed = newSpeed;
    }

    void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHP hp = collision.GetComponent<PlayerHP>();

            hp.TakeDamage(hpAmount);

            // アイテムを消す
            Destroy(gameObject);
        }
    }
}