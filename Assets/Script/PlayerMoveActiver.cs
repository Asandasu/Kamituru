using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveActiver : MonoBehaviour
{
    public bool isPlayerMove = false;

    public void PlayerMoveActive()
    {
        isPlayerMove = true;
    }

    public void PlayerMoveStopper()
    {
        isPlayerMove = false;
    }
}
