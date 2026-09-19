using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private PlayerInput controllerInput;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D playerRigid;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 歩行
        bool isWalking = Mathf.Abs(controllerInput.moveX) > 0.01f;

        animator.SetBool("walk", isWalking);

        // ジャンプ中
        bool isJumping = playerRigid.velocity.y > 0.1f;
        animator.SetBool("jump", isJumping);

        // 落下しているか
        bool isFalling = playerRigid.velocity.y < -0.1f;
        animator.SetBool("fall", isFalling);

        // 攻撃
        if (controllerInput.attack) 
        { 
            animator.SetTrigger("attack");
        }

        if (controllerInput.moveX > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if(controllerInput.moveX < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
}
