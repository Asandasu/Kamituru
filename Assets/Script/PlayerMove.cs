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

    [SerializeField] private Conversation conversation;

    private int currentJumpCount;
    void Start()
    {
        playerRigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentJumpCount = jumpCount;
    }

    void Update()
    {
        if (conversation.isConversation)
        {
            playerRigid.velocity = Vector2.zero;
            return;
        }

        // ジャンプ
        if (controllerInput.jump && currentJumpCount > 0)
        {
            playerRigid.velocity = new Vector2(playerRigid.velocity.x,jumpPower);

            currentJumpCount--;
        }
    }

    void FixedUpdate()
    {
        if (conversation.isConversation)
        {
            playerRigid.velocity = Vector2.zero;
            return;
        }

        // 現在再生されているアニメーションを取得
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Attackアニメーション中か
        bool isAttacking = stateInfo.IsName("MitihimeDashAttack_Clip");

        // 攻撃中だけ速度アップ
        float currentSpeed = isAttacking ? attackSpeed : speedX;

        playerRigid.velocity = new Vector2(controllerInput.moveX * currentSpeed,playerRigid.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ground"))
            return;

        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                currentJumpCount = jumpCount;
                break;
            }
        }
    }
}