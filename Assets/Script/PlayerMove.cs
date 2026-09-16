using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]private PlayerInput controllerInput;
    Rigidbody2D playerRigid;

    [SerializeField]private float speedX = 3;
    // Start is called before the first frame update
    void Start()
    {
        playerRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerRigid.velocity = new Vector2(controllerInput.moveX * speedX, 0);
    }
}
