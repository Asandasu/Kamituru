using System.Collections;
using UnityEngine;

public class AcidoBulletBreath : MonoBehaviour
{
    [Header("移動")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private float turnSpeed = 180f;
    [SerializeField] private float lifeTime = 5f;

    [Header("ダメージ")]
    [SerializeField] private int hpAmount = 10;
    [SerializeField] private float coolTimeSecond = 1.0f;

    private Transform player;
    private Vector2 direction;

    private bool isActive = true;

    void Start()
    {
        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;

            direction =
                (player.position - transform.position).normalized;
        }

        // 一定時間後に消滅
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        if (player != null)
        {
            // プレイヤーへの方向
            Vector2 targetDirection =
                (player.position - transform.position).normalized;

            // 現在の角度
            float currentAngle =
                Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // プレイヤー方向の角度
            float targetAngle =
                Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

            // 少しずつ追尾
            float newAngle =
                Mathf.MoveTowardsAngle(
                    currentAngle,
                    targetAngle,
                    turnSpeed * Time.deltaTime
                );

            direction =
                new Vector2(
                    Mathf.Cos(newAngle * Mathf.Deg2Rad),
                    Mathf.Sin(newAngle * Mathf.Deg2Rad)
                );
        }

        // 移動だけ行う
        transform.position +=
            (Vector3)(direction * speed * Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isActive)
        {
            PlayerHP hp =
                collision.GetComponent<PlayerHP>();

            if (hp != null)
            {
                hp.TakeDamage(hpAmount);
            }

            StartCoroutine(CoolTime());
        }
    }

    private IEnumerator CoolTime()
    {
        isActive = false;

        yield return new WaitForSeconds(coolTimeSecond);

        isActive = true;
    }
}