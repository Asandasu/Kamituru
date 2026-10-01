using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveActiver : MonoBehaviour
{
    public bool isBossMove = false;

    [SerializeField] private BossWall wall;
    public void BossMoveActive()
    {
        isBossMove = true;
        wall.BossWallSet();
    }
}
