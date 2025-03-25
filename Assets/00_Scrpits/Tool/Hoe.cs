using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Hoe : Tool
{
    public Tilemap Soil;
    public Tilemap Interface;
    public Tilemap Obstacal;
    public TileBase tile;
    public Vector3Int mouseStart, mouseEnd;
    
    [SerializeField] bool CanDraw = true; 

    protected override void Start()
    {
        base.Start();
    }

    public override void Handler()
    {
        if (tile == null)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            CanDraw = true;
            hand.anim.SetInteger(Contant.State, 1);
            mouseStart = Soil.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)); 
            isHandle = true;
        }
        else if (Input.GetMouseButton(0) && isHandle)
        {
            Interface.ClearAllTiles();
            mouseEnd = Interface.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));  
            DrawTile(Interface);
        }
        else if (Input.GetMouseButtonUp(0)) 
        {
            hand.anim.SetInteger(Contant.State, 0);
            isHandle = false;
            Interface.ClearAllTiles();
            if (CanDraw)
            {
                mouseEnd = Soil.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));                
                DrawTile(Soil);
            }
        }
    }

    void DrawTile(Tilemap map)
    {
        int minX = Mathf.Min(mouseStart.x, mouseEnd.x);
        int maxX = Mathf.Max(mouseStart.x, mouseEnd.x);
        int minY = Mathf.Min(mouseStart.y, mouseEnd.y);
        int maxY = Mathf.Max(mouseStart.y, mouseEnd.y);

        for (int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, mouseStart.z);
                if(Obstacal.GetTile(tilePos) != null || Soil.GetTile(tilePos) != null)
                {
                    map.SetColor(tilePos, Color.green);
                    CanDraw = false;
                    continue;
                }
                map.SetTile(tilePos, tile);
            }
        }
    }

    public override void getProp()
    {
        Soil = Grid.transform.GetChild(4).GetComponent<Tilemap>();
        Interface = Grid.transform.GetChild(3).GetComponent<Tilemap>();
        Obstacal = Grid.transform.GetChild(1).GetComponent<Tilemap>();
    }

}
