using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Change(int i)
    {
        spriteRenderer.sprite = sprites[i];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
