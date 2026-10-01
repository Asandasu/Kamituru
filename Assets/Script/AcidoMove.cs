using UnityEngine;

public class AcidoMove : MonoBehaviour
{
    [Header("移動範囲")]
    [SerializeField] private float minX = -5f;
    [SerializeField] private float maxX = 5f;
    [SerializeField] private float centerX = 0;

    [Header("最低高度")]
    [SerializeField] private float minY = 2f;

    [Header("最大高度")]
    [SerializeField] private float maxY = 2f;

    [Header("移動速度")]
    [SerializeField] private float moveSpeed = 3f;

    [Header("1回の移動時間")]
    [SerializeField] private float moveInterval = 1f;

    [Header("上下移動の強さ")]
    [SerializeField] private float verticalMovePower = 0.3f;

    [Header("攻撃までの移動回数")]
    [SerializeField] private int minMoveCount = 2;
    [SerializeField] private int maxMoveCount = 3;

    [Header("攻撃Trigger")]
    [SerializeField] private string[] attackTriggers;

    [SerializeField] private Conversation conversation;

    [SerializeField] private BossMoveActiver moveActiver;

    private Rigidbody2D rb;
    private Animator animator;

    private int moveCount;
    private int targetMoveCount;
    private Vector2 moveDirection;

    private float timer;
    private bool isMoving = true;

    private SpriteRenderer sprite;
    private EnemyHP hp;
    void Start()
    {
        hp = GetComponent<EnemyHP>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        // 2～3回移動したら攻撃
        targetMoveCount = Random.Range(minMoveCount, maxMoveCount + 1);
        ChangeDirection();
    }

    void FixedUpdate()
    {
        if (!moveActiver.isBossMove)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        if (!isMoving) { rb.velocity = Vector2.zero; return; }
        timer += Time.fixedDeltaTime;
        // 1回分の移動が終了
        if (timer >= moveInterval)
        {
            timer = 0f; moveCount++;
            // 2～3回移動したら攻撃
            if (moveCount >= targetMoveCount) { StartAttack(); return; }
            // 次の方向へ移動
            ChangeDirection();
        }
        // 移動
        rb.velocity = moveDirection * moveSpeed;

        // 移動範囲制限
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.y = Mathf.Clamp(position.y, minY, maxY);
        transform.position = position;

        if(centerX > position.x)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }

    }

    private void ChangeDirection()
    {
        // 左右は必ず移動
        float x = Random.value < 0.5f ? -1f : 1f;
        // 上下は弱め
        float y = Random.Range(-0.3f, 0.3f);
        moveDirection = new Vector2(x, y).normalized;
    }

    private void StartAttack()
    {
        // 攻撃が登録されていなければ何もしない
        if (attackTriggers == null || attackTriggers.Length == 0)
        {
            EndAttack();
            return;
        }

        int attackIndex;
        // ランダムに攻撃を選択
        if (hp.CurrentHP > hp.maxHP / 2)
        {
            attackIndex = Random.Range(0, attackTriggers.Length - 2);
        }
        else
        {
            attackIndex = Random.Range(0, attackTriggers.Length);
        }
            
        string attackTrigger = attackTriggers[attackIndex];

        // 選んだ攻撃を再生
        animator.SetTrigger(attackTrigger);
    }

    // 攻撃アニメーション終了時に呼ぶ
    public void EndAttack()
    {
        moveCount = 0;
        // 次の攻撃までの移動回数を決める
        targetMoveCount = Random.Range(minMoveCount, maxMoveCount + 1);
        timer = 0f;
        isMoving = true;
        ChangeDirection();
    }
}
