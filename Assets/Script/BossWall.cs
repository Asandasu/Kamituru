using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BossWall : MonoBehaviour
{
    TilemapCollider2D collider2D;
    private void Start()
    {
        collider2D = GetComponent<TilemapCollider2D>();

        collider2D.enabled = false;
    }
    public void BossWallSet()
    {
        collider2D.enabled = true;
    }
}
