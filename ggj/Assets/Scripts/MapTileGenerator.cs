using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapTileGenerator : MonoBehaviour
{
    private int x = 11, y = 5;
    
    public Sprite tileFull;

    private GameObject[,] TileFulls;

    private bool _editMode = false;

    private Vector3 tileSize;

    private GameObject VisibileTile;

    // Start is called before the first frame update
    void Start()
    {
        TileFulls = new GameObject[x, y];

        tileSize = tileFull.bounds.size;

        for (int i = 0; i < x; i++)
        { 
            for (int j = 0; j < y; j++)
            {
                float posX = ((float)i - x / 2f + 0.5f) * tileSize.x;
                float posY = ((float)j - y / 2f + 0.5f) * tileSize.y;

                ////Tile Border
                //var borderObj = new GameObject();
                //borderObj.transform.SetParent(this.transform);
                //borderObj.transform.position = new Vector3(posX, posY, -1);

                //var borderRenderer = borderObj.AddComponent<SpriteRenderer>();
                //borderRenderer.sprite = tileBorder;
                //borderObj.name = $"tile-border({i}, {j})";

                //TileBorders[i, j] = borderObj;

                //Tile
                var tileObj = new GameObject();
                tileObj.transform.SetParent(this.transform);
                tileObj.transform.position = new Vector3(posX, posY, -1);

                var tileRenderer = tileObj.AddComponent<SpriteRenderer>();
                tileRenderer.sprite = tileFull;
                tileRenderer.name = $"tile({i}, {j})";

                TileFulls[i, j] = tileObj;
            }
        }

        //ActivateTileBorders(false);

        foreach (var x in TileFulls)
        {
            x.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        VisibileTile?.SetActive(false);
        if (_editMode)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                int i = (int)Math.Round(hit.point.x / tileSize.x - 0.5f + x / 2f);
                int j = (int)Math.Round(hit.point.y / tileSize.y - 0.5f + y / 2f);

                if (i < 0 || i >= x || j < 0 || j >= y)
                {
                    return;
                }
                VisibileTile = TileFulls[i, j];
                VisibileTile.SetActive(true);
            }
        }
    }

    public void RespawnEnemy(Enemy enemy)
    {
        var position = GameObject.FindObjectOfType<Enemy>().transform.localPosition;
    }

    public void EnableEditMode()
    {
        _editMode = true;
    }
}
