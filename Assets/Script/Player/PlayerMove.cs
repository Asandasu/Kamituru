using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private PlayerInput controllerInput;
    private Rigidbody2D playerRigid;
    private Animator animator;


    [SerializeField] private float speedX = 3;
    [SerializeField] private float attackSpeed = 5;

    [SerializeField] private float jumpPower = 5;
    [SerializeField] private int jumpCount = 2;

    [SerializeField] private float fallSpeed = 8f;

    [SerializeField] private Conversation conversation;

    [SerializeField] private PlayerMoveActiver activer;

    private bool isGrounded = false;

    private int currentJumpCount;
    void Start()
    {
        playerRigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentJumpCount = jumpCount;
    }

    void Update()
    {
        if (!activer.isPlayerMove)
        {
            return;
        }

        if (controllerInput.jump && currentJumpCount > 0)
        {
            // 横方向の速度を維持してジャンプ
            playerRigid.velocity = new Vector2(
                playerRigid.velocity.x,
                jumpPower
            );

            currentJumpCount--;
        }
    }

    void FixedUpdate()
    {
        // 前回の物理判定で接地していたかを保存
        bool wasGrounded = isGrounded;

        // 次の物理判定に備えてリセット
        isGrounded = false;

        if (!activer.isPlayerMove)
        {
            return;
        }

        AnimatorStateInfo stateInfo =
            animator.GetCurrentAnimatorStateInfo(0);

        bool isAttacking =
            stateInfo.IsName("MitihimeDashAttack_Clip");

        float currentSpeed = isAttacking ? attackSpeed : speedX;

        Vector2 velocity = playerRigid.velocity;

        // 横移動
        velocity.x = controllerInput.moveX * currentSpeed;

        // 空中で落下速度の上限を設定
        if (!wasGrounded)
        {
            velocity.y = Mathf.Max(velocity.y, -fallSpeed);
        }

        playerRigid.velocity = velocity;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ground"))
            return;

        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                currentJumpCount = jumpCount;
                isGrounded = true;
                return;
            }
        }
    }
}