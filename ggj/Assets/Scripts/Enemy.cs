﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int level;

    public int totalHealth;

    public int CurrentHealth { get; private set; }

    public float attackSpeed;

    public int damage;
    
    public float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = totalHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentHealth <= 0)
        {
            Destroy(this);
        }
    }
}
