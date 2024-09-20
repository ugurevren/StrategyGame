using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UnitData : ScriptableObject
{
    public string name;
    public Sprite sprite;
    public int damage;
    public int health;
    public int energyCost;
}
