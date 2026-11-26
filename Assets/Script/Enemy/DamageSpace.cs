using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSpace : MonoBehaviour
{
    [Header("ダメージ")]
    [SerializeField] private int hpAmount = 10;
    [SerializeField] private float coolTimeSecond = 1.0f;

    private bool isActive = true;

    void Start()
    {

    }

    void Update()
    {
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isActive)
        {
            PlayerHP hp =
                collision.GetComponent<PlayerHP>();

            if (hp != null)
            {
                hp.TakeDamage(hpAmount);
            }

            StartCoroutine(CoolTime());
        }
    }

    private IEnumerator CoolTime()
    {
        isActive = false;

        yield return new WaitForSeconds(coolTimeSecond);

        isActive = true;
    }
}
