using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BossWall : MonoBehaviour
{
    TilemapCollider2D collider2D;
    TilemapRenderer tilemapRenderer;
    private void Start()
    {
        collider2D = GetComponent<TilemapCollider2D>();
        tilemapRenderer = GetComponent<TilemapRenderer>();

        tilemapRenderer.enabled = false;
        collider2D.enabled = false;
    }
    public void BossWallSet()
    {
        tilemapRenderer.enabled = true;
        collider2D.enabled = true;
    }
}
