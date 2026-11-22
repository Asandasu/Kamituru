using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    private float originalFixedDeltaTime;
    private Coroutine slowMotionCoroutine;

    private void Awake()
    {
        Instance = this;
        originalFixedDeltaTime = Time.fixedDeltaTime;
    }

    public void SlowMotion(float duration, float scale)
    {
        if (slowMotionCoroutine != null)
        {
            StopCoroutine(slowMotionCoroutine);
        }

        slowMotionCoroutine = StartCoroutine(
            SlowMotionCoroutine(duration, scale)
        );
    }

    private IEnumerator SlowMotionCoroutine(float duration, float scale)
    {
        Time.timeScale = scale;
        Time.fixedDeltaTime = originalFixedDeltaTime * scale;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = originalFixedDeltaTime;

        slowMotionCoroutine = null;
    }
}