using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : Singleton<GameManager>
{
    public GameObject grid;
    public Dictionary<Vector3Int, bool> tilePos = new Dictionary<Vector3Int, bool>();
    
    #region
    public Tilemap Soil;
    public Tilemap Interface;
    public Tilemap Obstacal;
    public Tilemap WaterMap;
    #endregion

    public TextMeshProUGUI OrderMessage;
    public Dictionary<string, Product> AssetsItem = new Dictionary<string, Product>();
    public List<string> orders =  new List<string>();
    
    [SerializeField] private int quantity;
    [SerializeField] private string item;
    [SerializeField] private int Coin = 0;
    
    protected override void Start()
    {
        base.Start();
        getGrid();
        ResultOrder(OrderMessage.text);
    }


    private void getGrid()
    {
        grid = GameObject.FindWithTag("Grid");
        Obstacal = grid.transform.GetChild(1).GetComponent<Tilemap>();
        Interface = grid.transform.GetChild(3).GetComponent<Tilemap>();
        Soil = grid.transform.GetChild(4).GetComponent<Tilemap>();
        WaterMap = grid.transform.GetChild(5).GetComponent<Tilemap>();
    }

    public void ResultOrder(string orderMess)
    {
        string[] order = orderMess.ToLower().Trim().Split(' ');
        try
        {
            quantity = int.Parse(order[1]);
            item = order[2];
        }catch(Exception e)
        {
            Debug.LogError(e.Message);  
        }
    }

    public void ReceiveMoney()
    {
        if(item == "")
        {
            return;
        }

        if (!AssetsItem.ContainsKey(item))
        {
            Debug.Log("KHONG CO");
            return;
        }

        if (AssetsItem[item].quantity >= quantity)
        {
            AssetsItem[item].quantity -= quantity;
            Coin += quantity * 10;
            UIManager.intant.ShowMessage(quantity * 10);
            item = "";
            LoadOrder();
        }
    }

    public void LoadOrder()
    {
        if(orders == null)
        {
            item = "";
            return;
        }
        OrderMessage.text = orders[0];
        ResultOrder(orders[0]);
        orders.RemoveAt(0);
    }

    public void AddItem(string name , Product p)
    {
        if (!AssetsItem.ContainsKey(name))
        {
            AssetsItem.Add(name, p);
            UIManager.intant.ShowProduct(p.Icon.sprite);
        }
        AssetsItem[name].quantity += 1;
    }
}
