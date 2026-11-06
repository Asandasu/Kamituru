using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public enum MovementType
    {
        Ground, // 地面を歩く
        Flying  // 空中を移動する
    }

    [Header("移動タイプ")]
    [SerializeField] private MovementType movementType;

    [Header("基本設定")]
    [SerializeField] private float speed = 2f;

    [Header("プレイヤー追跡")]
    [SerializeField] private bool chasePlayer = true;
    [SerializeField] private float distance = 5f;
    [SerializeField] private Transform player;

    [Header("移動範囲（ワールド座標）")]
    [SerializeField] private float minX = -5f;
    [SerializeField] private float maxX = 5f;
    [SerializeField] private float minY = 0f;
    [SerializeField] private float maxY = 3f;

    [Header("浮遊型の円形移動")]
    [SerializeField] private float circleRadius = 2f;
    [SerializeField] private float circleSpeed = 1f;
    [SerializeField] private Vector2 circleCenterOffset = Vector2.zero;

    private Vector2 circleCenter;
    private float circleAngle;

    [Header("ジャンプ設定（地上型のみ）")]
    [SerializeField] private bool jumpEnabled = false;
    [SerializeField] private float jumpInterval = 3f;
    [SerializeField] private float jumpPower = 5f;

    private Rigidbody2D rb;

    private float moveDirection = 1f;
    private float jumpTimer;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (player == null)
        {
            GameObject playerObject =
                GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
                player = playerObject.transform;
        }

        if (movementType == MovementType.Flying)
        {
            circleCenter = (Vector2)transform.position + circleCenterOffset;

            // 初期位置に応じて円周上の角度を決める
            Vector2 offset = (Vector2)transform.position - circleCenter;

            if (offset.sqrMagnitude > 0.001f)
                circleAngle = Mathf.Atan2(offset.y, offset.x);
        }
    }

    private void FixedUpdate()
    {
        if (rb == null)
            return;

        bool shouldChase = false;

        if (chasePlayer && player != null)
        {
            float playerDistance =
                Vector2.Distance(transform.position, player.position);

            shouldChase = playerDistance <= distance;
        }

        if (movementType == MovementType.Ground)
        {
            GroundMove(shouldChase);
        }
        else
        {
            FlyingMove(shouldChase);
        }
    }

    private void GroundMove(bool shouldChase)
    {
        Vector2 velocity = rb.velocity;

        if (shouldChase)
        {
            // プレイヤーのいる方向へ移動
            float direction =
                Mathf.Sign(player.position.x - transform.position.x);

            velocity.x = direction * speed;
        }
        else
        {
            // 指定範囲を左右に往復
            if (transform.position.x <= minX)
                moveDirection = 1f;
            else if (transform.position.x >= maxX)
                moveDirection = -1f;

            velocity.x = moveDirection * speed;
        }

        // Y方向の速度は重力に任せる
        rb.velocity = velocity;

        // 一定時間ごとにジャンプ
        if (jumpEnabled && isGrounded)
        {
            jumpTimer += Time.fixedDeltaTime;

            if (jumpTimer >= jumpInterval)
            {
                velocity = rb.velocity;
                velocity.y = jumpPower;
                rb.velocity = velocity;

                isGrounded = false;
                jumpTimer = 0f;
            }
        }
    }

    private void FlyingMove(bool shouldChase)
    {
        if (shouldChase)
        {
            // プレイヤーを追跡
            Vector2 direction =
                ((Vector2)player.position - rb.position).normalized;

            rb.velocity = direction * speed;
        }
        else
        {
            // 円形に移動
            circleAngle += circleSpeed * Time.fixedDeltaTime;

            Vector2 targetPosition = circleCenter + new Vector2(
                Mathf.Cos(circleAngle) * circleRadius,
                Mathf.Sin(circleAngle) * circleRadius
            );

            Vector2 direction = (targetPosition - rb.position).normalized;
            rb.velocity = direction * speed;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (movementType != MovementType.Ground)
            return;

        if (!collision.gameObject.CompareTag("Ground"))
            return;

        foreach (ContactPoint2D contact in collision.contacts)
        {
            // 地面の上に立っている場合だけ接地扱い
            if (contact.normal.y > 0.5f)
            {
                isGrounded = true;
                return;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }
}