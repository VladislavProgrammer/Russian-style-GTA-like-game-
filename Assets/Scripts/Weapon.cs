using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon 
{
    public string Name { get; }
    public int Damage { get; }

    public Weapon(string name, int damage)
    {
        Name = name;
        Damage = damage;
        Debug.Log($"Создано оружие: {Name}, урон: + {Damage}");
    }
}
