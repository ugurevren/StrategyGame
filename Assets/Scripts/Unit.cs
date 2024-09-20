using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class Unit : Poolable, IAttackable
{
    public UnitData unitData;
    public int width;
    public int height;

    public bool TakeDamage(int damage, out bool isDead)
    {
        Debug.Log("Unit Take Damage");
        return isDead = true;
    }
}
