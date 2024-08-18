using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{    
    [SerializeField] string itemName;
    [SerializeField] int itemAmount;
    [SerializeField] float itemDropChance;
    [SerializeField] GameObject itemPrefab;

    private Player player;
    private Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    public string ItemName { get => itemName; set => itemName = value; }
    public int ItemAmount { get => itemAmount; set => itemAmount = value; }
    public Player Player { get => player; set => player = value; }
    public SpriteRenderer SpriteRenderer { get => spriteRenderer; set => spriteRenderer = value; }
    public float ItemDropChance { get => itemDropChance; set => itemDropChance = value; }

    protected virtual void Init()
    {
        player = GameManager.Instance.Player;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public virtual void Use()
    {

    }   
}
