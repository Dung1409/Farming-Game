using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ShopManager : Singleton<ShopManager> 
{
    [SerializeField] GameObject Shop;
    public Dictionary<string , Seed> seeds = new Dictionary<string , Seed>();
    [SerializeField] private TextMeshProUGUI textCoin;
    public int Coin;
    protected override void Start()
    {
        base.Start();
        Shop.SetActive(false);
        Seed[] s = GameObject.FindObjectsOfType<Seed>();
        seeds = s.ToDictionary(t => t.name.ToLower() , t => t);
    }

    public void ShowShop()
    {
        Shop.SetActive(!Shop.activeSelf);
        if (!Shop.activeSelf)
        {
            foreach(var key in  seeds.Keys)
            {
                if (seeds[key].curSeed != seeds[key].seed) 
                {
                    int s = seeds[key].seed;
                    int c = seeds[key].curSeed;
                    UIManager.intant.ShowMessage("+ " + (s - c) + " " + key);
                    seeds[key].curSeed = s;
                }
            }
        }
    }

    public void UpdateCoin(int Value)
    {
        Coin += Value;
        textCoin.text = "Coin: " + Coin.ToString();
    }
}
