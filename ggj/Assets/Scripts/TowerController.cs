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
        Position.z = -2;
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

    private float LastTimeFired = 0;
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
        if (TowerModel.AttackSpeed < LastTimeFired)
        {
            EmitAmmo();
            LastTimeFired = 0;
        }
        LastTimeFired += Time.deltaTime;
    }

    private void EmitAmmo()
    {
        if (AreWeActive() && HasEnemyOnSights())
        {
            Vector3 EmitOffset = new Vector3(0, 0, 0);
            Instantiate(TowerModel.AmmoPrefab, this.transform.position + EmitOffset, Quaternion.identity, null);
        }
    }

    private bool AreWeActive()
    {
        return ActiveStates.Any(x => (x == m_GameManagerRef.Status)) && IsPlaced;
    }

    private bool HasEnemyOnSights()
    {
        int EnemyLayer = 1 << LayerMask.NameToLayer("Enemy");
        Vector2 StartingPosition = this.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector3.right, Mathf.Infinity, EnemyLayer);

        if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            return true;
        }

        return false;

    }
}