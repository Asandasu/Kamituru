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

    [Header("衝突時の反転（地上型のみ）")]
    [SerializeField] private float collisionTurnTime = 0.5f;

    [Header("衝突時の方向転換")]
    [SerializeField] private float collisionTurnDuration = 0.5f;

    private float collisionTurnTimer;

    [Header("足場の端の判定")]
    [SerializeField] private float groundCheckDistance = 0.5f;
    [SerializeField] private float edgeCheckOffset = 0.05f;

    private Collider2D bodyCollider;
    private bool waitingForGroundAfterTurn = false;

    [Header("Sprite反転")]
    [SerializeField] private bool isfliping = false;

    private Rigidbody2D rb;

    private float moveDirection = 1f;
    private float jumpTimer;
    private bool isGrounded;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        bodyCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

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

        if (collisionTurnTimer > 0f)
        {
            collisionTurnTimer -= Time.fixedDeltaTime;
        }

        bool shouldChase = false;

        if (chasePlayer && player != null &&
            collisionTurnTimer <= 0f)
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

    private void UpdateFacing(float directionX)
    {
        if (spriteRenderer == null)
            return;

        if (directionX > 0.01f)
        {
            if (isfliping)
            {
                spriteRenderer.flipX = false;
            }
            else if (!isfliping)
            {
                spriteRenderer.flipX = true;
            }
        }
        else if (directionX < -0.01f)
        {
            if (isfliping)
            {
                spriteRenderer.flipX = true;
            }
            else if (!isfliping)
            {
                spriteRenderer.flipX = false;
            }
        }
    }

    private void GroundMove(bool shouldChase)
    {
        if (shouldChase)
        {
            float direction =
                Mathf.Sign(player.position.x - transform.position.x);

            if (Mathf.Abs(player.position.x - transform.position.x) > 0.01f)
                moveDirection = direction;
        }
        else
        {
            if (transform.position.x <= minX)
                moveDirection = 1f;
            else if (transform.position.x >= maxX)
                moveDirection = -1f;

            if (isGrounded)
            {
                bool hasGround = HasGroundAhead();

                if (hasGround)
                {
                    // 前方に地面が確認できたら再反転を許可
                    waitingForGroundAfterTurn = false;
                }
                else if (!waitingForGroundAfterTurn)
                {
                    // 足場がなければ一度だけ反転
                    moveDirection *= -1f;
                    waitingForGroundAfterTurn = true;
                }
            }
        }

        rb.velocity = new Vector2(
            moveDirection * speed,
            rb.velocity.y
        );

        UpdateFacing(moveDirection);

        if (jumpEnabled && isGrounded)
        {
            jumpTimer += Time.fixedDeltaTime;

            if (jumpTimer >= jumpInterval)
            {
                rb.velocity = new Vector2(
                    rb.velocity.x,
                    jumpPower
                );

                isGrounded = false;
                jumpTimer = 0f;
            }
        }
    }

    private bool HasGroundAhead()
    {
        if (bodyCollider == null)
            return true;

        Bounds bounds = bodyCollider.bounds;

        Vector2 origin = new Vector2(
            bounds.center.x +
            moveDirection * (bounds.extents.x + edgeCheckOffset),
            bounds.min.y + edgeCheckOffset
        );

        RaycastHit2D hit = Physics2D.Raycast(
            origin,
            Vector2.down,
            groundCheckDistance
        );

        Debug.DrawRay(
            origin,
            Vector2.down * groundCheckDistance,
            Color.red
        );

        return hit.collider != null &&
               hit.collider.CompareTag("Ground");
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

        // 移動方向に合わせて向きを変更
        UpdateFacing(rb.velocity.x);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (movementType != MovementType.Ground)
            return;

        // プレイヤーとの接触は無視
        if (collision.gameObject.CompareTag("Player"))
            return;

        // 敵同士の衝突か確認
        EnemyMove otherEnemy =
            collision.gameObject.GetComponentInParent<EnemyMove>();

        bool hitEnemy = otherEnemy != null && otherEnemy != this;

        // Groundタグの壁との衝突か確認
        bool hitWall = collision.gameObject.CompareTag("Ground");

        if (!hitEnemy && !hitWall)
            return;

        // 横方向から衝突したか確認
        bool hitSide = false;

        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (Mathf.Abs(contact.normal.x) > 0.5f)
            {
                hitSide = true;
                break;
            }
        }

        if (!hitSide)
            return;

        // 現在の進行方向を反転
        float currentDirection =
            Mathf.Abs(rb.velocity.x) > 0.01f
            ? Mathf.Sign(rb.velocity.x)
            : moveDirection;

        moveDirection = -currentDirection;

        // 衝突後は一定時間追跡を中断
        collisionTurnTimer = collisionTurnDuration;

        rb.velocity = new Vector2(
            moveDirection * speed,
            rb.velocity.y
        );

        UpdateFacing(moveDirection);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }
}