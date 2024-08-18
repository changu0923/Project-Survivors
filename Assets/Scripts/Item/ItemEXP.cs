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
        ObjectPoolManager.Instance.Destory(ItemName, gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            Use();
        }
    }
}