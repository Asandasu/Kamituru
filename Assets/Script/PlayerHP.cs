using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private int maxHP = 100;

    public int CurrentHP { get; private set; }

    void Start()
    {
        CurrentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        CurrentHP -= damage;

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
    }

    private void Die()
    {
        Debug.Log("Player Dead");
    }
}