using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSelectionUI : MonoBehaviour
{
    public int MaxTowerCount = 3;    
    public List<GameObject> TowerPrefabs;
    private GameObject[] m_ListedTowers;

    void Awake()
    {
        m_ListedTowers = new GameObject[MaxTowerCount];
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateShopList();
    }

    // Update is called once per frame 
    void Update()
    {
        // Handle reroll/close 
    }

    public void GenerateShopList()
    {
        for (int i = 0; i < MaxTowerCount; ++i)
        {
            GameObject NewTower = TowerPrefabs[UnityEngine.Random.Range(0, TowerPrefabs.Count - 1)];            
            CreateItem(NewTower);
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

    protected void TowerClicked(GameObject uiSender, GameObject towerPrefab)
    {
        Debug.Log("Clicked");
        if (true)
        {
            Camera camera = Camera.main;
            Vector3 WorldPosition = camera.ScreenToWorldPoint(Input.mousePosition);
            WorldPosition.z = -1;            

            GameObject newTower = Instantiate(towerPrefab, WorldPosition, Quaternion.identity);
            TowerModel towerModel = newTower.GetComponent<TowerController>().TowerModel;
            newTower.AddComponent<FollowMouse>();
            newTower.GetComponent<SpriteRenderer>().material = towerModel.BlinkingMaterial;

            Destroy(uiSender);
            this.gameObject.SetActive(false);
        }
    }
}
