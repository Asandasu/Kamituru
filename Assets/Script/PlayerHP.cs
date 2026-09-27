using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHP : MonoBehaviour
{
    [SerializeField] private int maxHP = 100;
    [SerializeField] private Slider hpSlider;

    public int CurrentHP { get; private set; }
    SpriteRenderer sprite;
    float damageTime = 0.1f;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        CurrentHP = maxHP;

        // Sliderの初期設定
        hpSlider.maxValue = maxHP;
        hpSlider.value = CurrentHP;
    }

    public void TakeDamage(int damage)
    {
        StartCoroutine(DamageImage());
        CurrentHP -= damage;

        // Sliderを更新
        hpSlider.value = CurrentHP;

        if (CurrentHP <= 0)
        {
            CurrentHP = 0;

            Die();
        }
    }

    public void Heal(int amount)
    {
        StartCoroutine(HealImage());
        CurrentHP += amount;

        if (CurrentHP > maxHP)
        {
            CurrentHP = maxHP;
        }

        // Sliderを更新
        hpSlider.value = CurrentHP;
    }

    private void Die()
    {
        Debug.Log("Player Dead");
    }

    private IEnumerator DamageImage()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(damageTime);
        sprite.color = Color.white;
    }

    private IEnumerator HealImage()
    {
        sprite.color = Color.green;
        yield return new WaitForSeconds(damageTime);
        sprite.color = Color.white;
    }
}