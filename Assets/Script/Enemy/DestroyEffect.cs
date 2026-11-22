using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    // アニメーションの最後から呼び出す
    public void AnimationEnd()
    {
        Destroy(gameObject);
    }
}
