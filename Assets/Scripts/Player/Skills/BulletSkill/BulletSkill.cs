using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSkill : Skill
{
    [SerializeField] GameObject bulletPrefab;
    public override void Use()
    {
        Debug.Log($"[{DateTime.Now}] BulletSkill »ç¿ë!");   
    }    
}
