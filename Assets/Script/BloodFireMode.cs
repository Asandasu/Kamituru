using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodFireMode : MonoBehaviour
{
    [SerializeField]private PlayerInput input;
    [SerializeField] private float duration = 3f;

    private SpriteRenderer sprite;
    public bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // L / C を押した瞬間だけ検出
        if (input.bloodFire && !isActive) 
        { 
            StartCoroutine(BloodFire());
        }
    }

    private IEnumerator BloodFire() 
    {
        isActive = true; 

        // 血炎ON
        sprite.enabled = true; 

        // 3秒待つ
        yield return new WaitForSeconds(duration);

        // 血炎OFF
        sprite.enabled = false; 

        isActive = false;
    }
}
