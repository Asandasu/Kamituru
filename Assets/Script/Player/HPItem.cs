using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPItem : MonoBehaviour
{
    [SerializeField] private int hpAmount = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHP hp = collision.GetComponent<PlayerHP>();
            
            hp.Heal(hpAmount);

            // アイテムを消す
            Destroy(gameObject);
        }
    }
}
