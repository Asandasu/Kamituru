using UnityEngine;
using UnityEngine.UI;

public class BloodFireGauge : MonoBehaviour
{
    [SerializeField] private float maxGauge = 100f;
    [SerializeField] private Sprite[] sprite;
    [SerializeField] private Image image;
    [SerializeField] private Image fireEye;

    public float CurrentGauge { get; private set; }

    void Start()
    {
        CurrentGauge = 0f;
        fireEye.enabled = false;
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
            fireEye.enabled = false;
        }

        if (CurrentGauge > maxGauge)
        {
            CurrentGauge = maxGauge;
        }
    }

    public void FireEyeOpen()
    {
        fireEye.enabled = true;
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