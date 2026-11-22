using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHP : MonoBehaviour
{
    [SerializeField] private int maxHP = 100;
    [SerializeField] private Slider hpSlider;

    [SerializeField] private PlayerMoveActiver activer;

    [Header("死亡演出")]
    [SerializeField] private GameObject deathPanelPrefab;
    [SerializeField] private Transform canvasTransform;

    private bool isDead = false;
    public int CurrentHP { get; private set; }
    SpriteRenderer sprite;
    float damageTime = 0.1f;
    bool isCoolTime = false;

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
        if (!activer.isPlayerMove && isCoolTime)
            return;

        StartCoroutine(DamageImage());
        CurrentHP -= damage;

        // Sliderを更新
        hpSlider.value = CurrentHP;

        if (CurrentHP <= 0)
        {
            if (isDead)
                return;

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
        isDead = true;

        // Canvasの子として死亡演出を生成
        GameObject panel = Instantiate(
            deathPanelPrefab,
            canvasTransform,
            false
        );

        // 子オブジェクトのAnimatorを取得
        Animator animator = panel.GetComponentInChildren<Animator>();

        if (animator != null)
        {
            // スローや停止中でもアニメーションを再生
            animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        }

        Debug.Log("Player Dead");
    }

    private IEnumerator DamageImage()
    {
        isCoolTime = true;
        sprite.color = Color.red;
        yield return new WaitForSeconds(damageTime);
        isCoolTime = false;
        sprite.color = Color.white;
    }

    private IEnumerator HealImage()
    {
        sprite.color = Color.green;
        yield return new WaitForSeconds(damageTime);
        sprite.color = Color.white;
    }
}