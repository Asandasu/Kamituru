using UnityEngine;

public class AcidoAttack : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject breath;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private Transform player;

    [SerializeField] private float bulletSpeed = 5f;

    void Start()
    {
        // Playerを自動取得
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }
    }

    // 攻撃アニメーションの途中で呼び出す
    public void ShootBullet()
    {
        if (player == null)
        {
            return;
        }

        if (bullet == null)
        {
            return;
        }

        // プレイヤーへの方向
        Vector2 direction =
            (player.position - bulletSpawnPoint.position).normalized;

        // 弾を生成
        GameObject newBullet = Instantiate(
            bullet,
            bulletSpawnPoint.position,
            Quaternion.identity
        );

        // Bulletスクリプトを取得
        AcidoBullet acidoBullet =
            newBullet.GetComponent<AcidoBullet>();

        if (acidoBullet != null)
        {
            acidoBullet.SetDirection(direction, bulletSpeed);
        }
    }

    // 攻撃アニメーションの途中で呼び出す
    public void ShootBreath()
    {
        if (player == null)
        {
            return;
        }

        if (breath == null)
        {
            return;
        }
        // 弾を生成
        GameObject newBullet = Instantiate(breath, bulletSpawnPoint.position, Quaternion.identity);
    }
}
