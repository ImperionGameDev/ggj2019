using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public TowerModel TowerModel;
    private readonly GameStatus[] ActiveStates = new GameStatus[]
    {
        GameStatus.Wave,
        GameStatus.Shopping        
    };
    public bool IsPlaced { get; private set; }
    private GameManager m_GameManagerRef;    

    public void Place(Vector3 Position)
    {
        this.gameObject.GetComponent<SpriteRenderer>().material = TowerModel.DefaultMaterial;
        this.transform.position = Position;
        Destroy(this.gameObject.GetComponent<FollowMouse>());
        this.gameObject.GetComponent<SpriteRenderer>().sprite = TowerModel.Sprite;
    }

    void Awake()
    {
        m_GameManagerRef = FindObjectOfType<GameManager>();        
        IsPlaced = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        // HAcky
        GameObject SelectedTile = FindObjectOfType<MapTileGenerator>().GetHighlightedTile();
        if (!IsPlaced && Input.GetMouseButtonDown(0) && SelectedTile != null)
        {
            Place(SelectedTile.transform.position);
            IsPlaced = true;
            // ?
            return;
        }
        
            // Fix me
        if (TowerModel.AttackSpeed < Time.deltaTime)
        {
            EmitAmmo();
        }     
    }

    private void EmitAmmo()
    {
        if (AreWeActive() && HasEnemyOnSight())
        {
            Vector3 EmitOffset = new Vector3(5, 0, 0);
            Instantiate(TowerModel.AmmoPrefab, this.transform.position + EmitOffset, Quaternion.identity, null);
        }
    }

    private bool AreWeActive()
    {
        return ActiveStates.Any(x => (x == m_GameManagerRef.Status)) && IsPlaced;
    }

    private bool HasEnemyOnSight()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.right);
        if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            return true;
        }

        return false;
    }
}
