using UnityEngine;

public class BloodFireItem : MonoBehaviour
{
    [SerializeField] private float gaugeAmount = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BloodFireGauge gauge = collision.GetComponentInChildren<BloodFireGauge>();

            if (gauge != null)
            {
                gauge.AddGauge(gaugeAmount);
            }

            // アイテムを消す
            Destroy(gameObject);
        }
    }
}
