using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item DataBase")]
public class ItemDB : ScriptableObject
{
   public List<Item> items = new List<Item>();

    public Item getItem(Sprite icon)
    {
        foreach(Item i in items)
        {
            if(i.icon == icon)
            {
                return i;
            }
        }
        return null;
    }
}

