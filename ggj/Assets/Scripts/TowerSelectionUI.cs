using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TowerSelectionUI : MonoBehaviour
{
    private readonly GameStatus[] ActiveStates = new GameStatus[]
    {
        GameStatus.Edit,
        GameStatus.Preperation
    };

    private int m_MaxTowerCount = 3;
    public int MaxTowerCount
    {
        get
        {
            return m_MaxTowerCount;
        }

        set
        {
            if (m_MaxTowerCount == value)
            {
                return;
            }

            m_MaxTowerCount = value;
            OnTowerCountChanged();
        }
    }

    private void OnTowerCountChanged()
    {
        ClearShopList();
        m_ListedTowers = new GameObject[MaxTowerCount];
    }

    public List<GameObject> TowerPrefabs;
    private GameObject[] m_ListedTowers;
    private GameManager m_GameManagerRef;
    private GameObject m_TowerToBePlacedRef;
    public bool IsBuilding { get; private set; }

    void Awake()
    {
        m_ListedTowers = new GameObject[MaxTowerCount];
        m_GameManagerRef = FindObjectOfType<GameManager>();
        IsBuilding = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
        GenerateShopList();
    }

    // Update is called once per frame 
    void Update()
    {
        if (IsBuilding)
        {
            if (m_TowerToBePlacedRef != null && m_TowerToBePlacedRef.GetComponent<TowerController>().IsPlaced)
            {
                IsBuilding = false;
                Destroy(m_TowerToBePlacedRef);
            }
        }        
    }

    // TODO(batuhan): Maybe return failed to open?
    public bool Open(bool GenerateNewItems = false)
    {        
        if (!CanWeShopNow())
        {
            return false;
        }
        
        if (GenerateNewItems || !DoWeHaveItems())
        {
            GenerateShopList();
        }

        this.gameObject.SetActive(true);
        return true;
    }

    private bool DoWeHaveItems()
    {
        return m_ListedTowers.All(x => x != null);
    }

    public void GenerateShopList()
    {
        for (int i = 0; i < MaxTowerCount; ++i)
        {
            int RandomIndex = UnityEngine.Random.Range(0, TowerPrefabs.Count);            
            GameObject NewTower = TowerPrefabs[RandomIndex];
            m_ListedTowers[i] = CreateItem(NewTower);
        }
    }

    private GameObject CreateItem(GameObject towerPrefab)
    {
        GameObject newItem = new GameObject();
        Image img = newItem.AddComponent<Image>();
        img.sprite = towerPrefab.GetComponent<SpriteRenderer>().sprite;
        Button button = newItem.AddComponent<Button>();
        button.onClick.AddListener(() => TowerClicked(newItem, towerPrefab));
        newItem.transform.SetParent(this.transform);
        return newItem;
    }

    private void ClearShopList()
    {
        for (int i = 0; i < MaxTowerCount; ++i)
        {
            GameObject CurrentObject = m_ListedTowers[i];
            Destroy(CurrentObject);
        }
    }

    protected void TowerClicked(GameObject uiSender, GameObject towerPrefab)
    {
        // TODO(batuhan): Can we buy it?
        if (true)
        {
            Camera camera = Camera.main;
            Vector3 WorldPosition = camera.ScreenToWorldPoint(Input.mousePosition);

            GameObject newTower = Instantiate(towerPrefab, WorldPosition, Quaternion.identity);
            TowerModel towerModel = newTower.GetComponent<TowerController>().TowerModel;
            newTower.AddComponent<FollowMouse>();
            newTower.GetComponent<SpriteRenderer>().material = towerModel.BlinkingMaterial;

            Destroy(uiSender);
        
            IsBuilding = true;
            this.gameObject.SetActive(false);
        }
    }

    private bool CanWeShopNow()
    {        
        return ActiveStates.Any(x => x == m_GameManagerRef.Status) && !m_GameManagerRef.IsShopOpen && !IsBuilding;
    }
}

