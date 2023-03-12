using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : BaseEnemy
{
    private void Update() 
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);    
    }
}
