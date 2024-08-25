using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileElements : MonoBehaviour
{
    [Header("타일 GameObject")]
    [SerializeField]List<SpriteRenderer> tiles = new List<SpriteRenderer>();

    [Header("타일 스프라이트")]
    [SerializeField]List<Sprite> tileSprites= new List<Sprite>();   
    
    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        foreach (SpriteRenderer tile in tiles) 
        {
            int randomIndex = Random.Range(0, tileSprites.Count);
            Sprite randomSprite = tileSprites[randomIndex];
            tile.sprite = randomSprite;
        }
    }
}
