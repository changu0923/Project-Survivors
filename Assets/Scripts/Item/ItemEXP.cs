using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEXP : Item
{
    private void OnEnable()
    {
        Init();
    }

    public override void Use()
    {
        Player.GainEXP(ItemAmount);
        ObjectPoolManager.Instance.Destroy(ItemName, gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Use();
        }
    }
}