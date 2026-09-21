using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BloodFireMode : MonoBehaviour
{
    [SerializeField]private PlayerInput input;
    [SerializeField] private float duration = 3f;

    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private Transform effectPoint;

    [SerializeField] private TextMeshProUGUI timeText;

    BloodFireGauge gauge;

    private SpriteRenderer sprite;
    public bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        gauge = GetComponent<BloodFireGauge>();

        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // L / C を押した瞬間だけ検出
        if (input.bloodFire && !isActive && gauge.IsFull()) 
        {
            StartCoroutine(BloodFire());
        }
    }

    private IEnumerator BloodFire() 
    {
        isActive = true;

        // エフェクトを生成
        GameObject effect = Instantiate( effectPrefab, effectPoint.position, effectPoint.rotation, effectPoint );

        // 血炎ON
        sprite.enabled = true;

        // 残り時間表示
        timeText.gameObject.SetActive(true);

        float remainingTime = duration;

        while (remainingTime > 0f)
        {
            timeText.text = Mathf.Ceil(remainingTime).ToString();

            remainingTime -= Time.deltaTime;

            yield return null;
        }

        // 0.0を表示
        timeText.text = "0";

        // 血炎OFF
        sprite.enabled = false;

        // 少し待ってから非表示
        timeText.gameObject.SetActive(false);

        gauge.UseGauge(10000);
        gauge.AddGauge(1);
        gauge.UseGauge(1);

        isActive = false;
    }
}
