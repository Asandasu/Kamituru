using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float moveTime = 2f;

    private float timer;

    private Rigidbody2D playerRigid;
    private PlayerAnimation animation;

    [SerializeField] private PlayerMoveActiver moveActiver;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        playerRigid = collision.GetComponent<Rigidbody2D>();
        animation = collision.GetComponent<PlayerAnimation>();

        moveActiver.PlayerMoveStopper();

        if (playerRigid != null)
        {
            timer = 0f;
        }
    }

    private void FixedUpdate()
    {
        if (playerRigid == null)
            return;

        // 右へ自動移動
        playerRigid.velocity = new Vector2(
            moveSpeed,
            0
        );

        animation.WalkMode();
        timer += Time.fixedDeltaTime;

        if (timer >= moveTime)
        {
            // Xだけ停止
            playerRigid.velocity = new Vector2(
                0f,0f
            );
            animation.WaitMode();
            Destroy(this);
        }
    }
}
