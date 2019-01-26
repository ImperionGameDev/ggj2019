using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTileGenerator : MonoBehaviour
{
    public int mapSizeX;

    public int mapSizeY;

    public Sprite tile;

    // Start is called before the first frame update
    void Start()
    {
        var tileSize = tile.bounds.size;

        for (int i = 0; i < mapSizeX; i++)
        {
            for (int j = 0; j < mapSizeY; j++)
            {
                float x = ((float)i - mapSizeX / 2f + 0.5f) * tileSize.x;
                float y = ((float)j - mapSizeY / 2f + 0.5f) * tileSize.y;

                var obj = new GameObject();
                obj.transform.SetParent(this.transform);
                obj.transform.position = new Vector3(x, y, -1);

                var renderer = obj.AddComponent<SpriteRenderer>();
                renderer.sprite = tile;
                renderer.color = Color.red;
                obj.name = $"tile({i}, {j})";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
