using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStat : MonoBehaviour
{
     [SerializeField] private int maxHealth;
     private int _currentHealth;

     [SerializeField] private int attackValue;
     public void Init()
     {
          _currentHealth = maxHealth;
     }

     public void Damage(int damage)
     {
          _currentHealth -= damage;
     }
     public int GetCurrentHealth()
     {
          return _currentHealth;
     }

     public int GetAttackValue()
     {
          return attackValue;
     }
}
