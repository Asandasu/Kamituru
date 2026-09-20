using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    [SerializeField] private int damage = 10;

    [SerializeField]private Collider2D hitbox;

    [SerializeField] private GameObject slashPrefab;
    [SerializeField] private GameObject slashPrefabStrong;
    [SerializeField] private GameObject bloodWave;
    [SerializeField] private Transform slashSpawnPoint;
    [SerializeField] private BloodFireMode bloodMode;

    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        hitbox.enabled = false;
    }

    public void AttackStart()
    {
        hitbox.enabled = true;
    }

    public void AttackEnd()
    {
        hitbox.enabled = false;
    }
    public void ShootSlash()
    {
        float direction = spriteRenderer.flipX ? -1f : 1f;

        GameObject slashfab;

        if(bloodMode.isActive)
        {
            slashfab = slashPrefabStrong;
        }
        else
        {
            slashfab = slashPrefab;
        }

        GameObject slash = Instantiate(slashfab,slashSpawnPoint.position,Quaternion.identity);

        BloodBlade projectile = slash.GetComponent<BloodBlade>();

        if (projectile != null)
        {
            projectile.SetDirection(direction);
        }
    }

    public void WaveShot()
    {
        if(bloodMode.isActive)
        {
            GameObject wave = Instantiate(bloodWave, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyHP enemy = collision.GetComponent<EnemyHP>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}