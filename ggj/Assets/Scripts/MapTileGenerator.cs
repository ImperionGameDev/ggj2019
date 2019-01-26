using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapTileGenerator : MonoBehaviour
{
    private int x = 11, y = 5;
    
    public Sprite tileFull;

    public GameObject[] Tiles;

    private bool _editMode = false;

    private Vector3 tileSize;

    private GameObject VisibileTile;

    // Start is called before the first frame update
    void Start()
    {

        foreach (var x in Tiles)
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

                foreach (var tile in Tiles)
                {
                    Debug.Log(tile.GetComponent<SpriteRenderer>().bounds);

                    if (tile.GetComponent<SpriteRenderer>().bounds.Contains(new Vector3(hit.point.x, hit.point.y, -1)))
                    {
                        VisibileTile = tile;
                        VisibileTile?.SetActive(true);
                        return;
                    }
                }

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
