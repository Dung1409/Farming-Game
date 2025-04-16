using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class Hammer : Tool
{
    [SerializeField] private Tilemap Obstacal;
    List<Vector3Int> dir = new List<Vector3Int>() {Vector3Int.left , Vector3Int.right , Vector3Int.up , Vector3Int.down };
    protected override void Start()
    {
        base.Start();
    }

    public override void getProp()
    {
        Obstacal = GameManager.intant.Obstacal; 
    }

    public override void Handler()
    {
        
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) 
        {
            hand.anim.SetInteger(Contant.State, 1);
            Vector3Int mousePos = Obstacal.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            DestroyObstacal(mousePos);
        }
        else if(Input.GetMouseButtonUp(0))
        {
            hand.anim.SetInteger(Contant.State, 0);
        }
    }

    private void DestroyObstacal(Vector3Int pos)
    {
        if(Obstacal.GetTile(pos) == null)
        {
            return;
        }
        Obstacal.SetTile(pos , null);
        foreach(Vector3Int d in dir)
        {
            DestroyObstacal(pos + d);
        }
    }
}
