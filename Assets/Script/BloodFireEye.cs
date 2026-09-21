using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodFireEye : MonoBehaviour
{
    [SerializeField] private float fadeTime = 0.5f;
    private SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeOut()); 
    }

    private IEnumerator FadeOut()
    {
        float timer = 0f;
        Color color = sprite.color;
        color.a = 1f; 

        sprite.color = color;

        while (timer < fadeTime) 
        { 
            timer += Time.deltaTime;
            float alpha = 1f - (timer / fadeTime); 
            color.a = alpha;
            sprite.color = color;
            yield return null; 
        } 

        Destroy(gameObject);
    }
}
