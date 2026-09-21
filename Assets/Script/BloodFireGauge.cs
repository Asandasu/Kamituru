using UnityEngine;
using UnityEngine.UI;

public class BloodFireGauge : MonoBehaviour
{
    [SerializeField] private float maxGauge = 100f;
    [SerializeField] private Sprite[] sprite;
    [SerializeField] private Image image;

    public float CurrentGauge { get; private set; }

    void Start()
    {
        CurrentGauge = 0f;
    }

    public void AddGauge(float amount)
    {
        CurrentGauge += amount;

        if(CurrentGauge >= maxGauge)
        {
            image.sprite = sprite[0];
        }
        else if(CurrentGauge >= 66)
        {
            image.sprite = sprite[1];
        }
        else if(CurrentGauge >= 33)
        {
            image.sprite = sprite[2];
        }
        else
        {
            image.sprite = sprite[3];
        }

        if (CurrentGauge > maxGauge)
        {
            CurrentGauge = maxGauge;
        }
    }

    public void UseGauge(float amount)
    {
        CurrentGauge -= amount;

        if (CurrentGauge < 0f)
        {
            CurrentGauge = 0f;
        }
    }

    public bool IsFull()
    {
        return CurrentGauge >= maxGauge;
    }
}