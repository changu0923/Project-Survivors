using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerMoveable
{
    public void GetInput();
    public void Move();    
}
