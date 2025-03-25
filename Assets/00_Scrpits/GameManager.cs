using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject grid;

    protected override void Start()
    {
        base.Start();
        getGrid();
    }

    private void getGrid()
    {
        grid = GameObject.FindWithTag("Grid");
    }
}
