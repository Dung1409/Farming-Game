using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : Singleton<InventoryManager>   
{
    [SerializeField] private ItemDB ItemDB;
    [SerializeField] private List<Image> InventorySlot;
    public Dictionary<int, Item> Index = new Dictionary<int, Item>();

    int currentIndex = 0;
    int previousIndex = 0;

    protected override void Start()
    {
        base.Start();
        for(int i = 0; i < InventorySlot.Count; i++)
        {
            Sprite icon = InventorySlot[i].transform.GetChild(1).GetComponent<Image>().sprite;
            if (ItemDB.items[i].isTool) 
            {
                Index.Add(i, ItemDB.getItem(icon));
            }
            else
            {
                //...
            }
        }
    }

    public void SelectItem(int indx)
    {
        previousIndex = currentIndex;
        currentIndex = indx;
        InventorySlot[previousIndex].transform.GetChild(0).gameObject.SetActive(false);
        InventorySlot[currentIndex].transform.GetChild(0).gameObject.SetActive(true);
    }
}
