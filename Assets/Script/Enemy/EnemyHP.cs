using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{
    public int maxHP;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private GameObject[] items;

    [SerializeField] private TalkEventBox eventer;
    [SerializeField] private BossMoveActiver moveActiver;

    [SerializeField] private GameObject killedEffect;

    SpriteRenderer sprite;

    float damageTime = 0.1f;

    public int CurrentHP { get; private set; }

    GameObject bossSlider;

    private bool isDead = false;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        CurrentHP = maxHP;

        if(hpSlider)
        {
            // Sliderの初期設定
            hpSlider.maxValue = maxHP;
            hpSlider.value = CurrentHP;
            bossSlider = hpSlider.gameObject;
            if(moveActiver && !moveActiver.isBossMove)
            {
                bossSlider.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (moveActiver && moveActiver.isBossMove)
        {
            bossSlider.SetActive(true);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        CurrentHP -= damage;

        int itemIndex = Random.Range(0, items.Length);

        float angle = Random.Range(0f, 360f);

        GameObject item = Instantiate(items[itemIndex], transform.position, Quaternion.identity);
        float speed = 0.1f;
        Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad),Mathf.Sin(angle * Mathf.Deg2Rad));
        Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed;

        StartCoroutine(DamageImage());
        if (CurrentHP <= 0)
        {
            if(eventer)
            {
                // ボス撃破時だけスロー演出
                TimeManager.Instance.SlowMotion(2f, 0.2f);

                eventer.AfterBattleEvent();
            }

            Instantiate(killedEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);

            return;
        }

        if(hpSlider)
        {
            hpSlider.value = CurrentHP;
        }
    }

    IEnumerator DamageImage()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(damageTime);
        sprite.color = Color.white;
    }
}
