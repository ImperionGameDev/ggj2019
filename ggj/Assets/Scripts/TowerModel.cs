using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType
{
    NONE = -1,
    T1,
    T2,
    NB,
}

[System.Serializable]
public class TowerModel
{
    public Material BlinkingMaterial;
    public Material DefaultMaterial;    
    public Sprite Sprite;

    public TowerType Type;
    public int Damage;
    public int Price;
    // Per sec?
    public float AttackSpeed;
    public float Range;      
}