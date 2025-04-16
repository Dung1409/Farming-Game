using UnityEngine;
using UnityEngine.EventSystems;
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
        if (tile == null || Handle.isSeed)
        {
            return;
        }
        
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            CanDraw = true;
            hand.anim.SetInteger(Contant.State, 1);
            mouseStart = Soil.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)); 
            isHandle = true;
        }

        else if (Input.GetMouseButton(0) && isHandle)
        {
            Interface.ClearAllTiles();
            CanDraw = true;
            mouseEnd = Interface.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));  
            DrawTile(Interface);
        }
            
        else if (Input.GetMouseButtonUp(0) && isHandle) 
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
                if(Obstacal.HasTile(tilePos) || Soil.GetTile(tilePos) != null)
                {
                    CanDraw = false;
                    continue;
                }
                map.SetTile(tilePos, tile);
            }
        }
        if(CanDraw)
        {
            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    Vector3Int tilePos = new Vector3Int(x, y, mouseStart.z);
                    GameManager.intant.tilePos[tilePos] = false;
                }
            }
            
        }
    }

    public override void getProp()
    {
        Soil = GameManager.intant.Soil;
        Interface = GameManager.intant.Interface;   
        Obstacal = GameManager.intant.Obstacal; 
    }

}
