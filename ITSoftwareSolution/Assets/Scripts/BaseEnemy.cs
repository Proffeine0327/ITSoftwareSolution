using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    [Header("Base")]
    [SerializeField] protected int maxHp;
    [SerializeField] protected int currentHp;
    [SerializeField] protected float moveSpeed;

    public virtual void Damage(int amount)
    {
        currentHp -= amount;
    }
}
