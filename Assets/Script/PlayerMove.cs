using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private PlayerInput controllerInput;
    private Rigidbody2D playerRigid;

    [SerializeField] private float speedX = 3;
    [SerializeField] private float jumpPower = 5;
    [SerializeField] private int jumpCount = 2;

    private int currentJumpCount;
    void Start()
    {
        playerRigid = GetComponent<Rigidbody2D>();
        currentJumpCount = jumpCount;
    }

    void Update()
    {
        // ジャンプ
        if (controllerInput.jump && currentJumpCount > 0)
        {
            playerRigid.velocity = new Vector2(playerRigid.velocity.x,jumpPower);

            currentJumpCount--;
        }
    }

    void FixedUpdate()
    {
        // 左右移動
        playerRigid.velocity = new Vector2(controllerInput.moveX * speedX,playerRigid.velocity.y);
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