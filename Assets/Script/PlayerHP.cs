using UnityEngine;
using UnityEngine.UI;


public class PlayerHP : MonoBehaviour
{
    [SerializeField] private int maxHP = 100;
    [SerializeField] private Slider hpSlider;

    public int CurrentHP { get; private set; }

    void Start()
    {
        CurrentHP = maxHP;

        // Sliderの初期設定
        hpSlider.maxValue = maxHP;
        hpSlider.value = CurrentHP;
    }

    public void TakeDamage(int damage)
    {
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
}