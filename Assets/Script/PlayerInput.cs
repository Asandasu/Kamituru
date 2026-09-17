using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float moveX;
    public float moveY;

    public bool jump;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        jump = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);
    }
}
