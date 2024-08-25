using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileElements : MonoBehaviour
{
    [Header("Ÿ�� GameObject")]
    [SerializeField]List<SpriteRenderer> tiles = new List<SpriteRenderer>();

    [Header("Ÿ�� ��������Ʈ")]
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
