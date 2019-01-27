using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerModel
{
    public Material BlinkingMaterial;
    public Material DefaultMaterial;    
    public Sprite Sprite;
    
    public int Damage;
    public int Price;
    // Per sec?
    public float AttackSpeed;
    public float Range;
    public GameObject AmmoPrefab;
}