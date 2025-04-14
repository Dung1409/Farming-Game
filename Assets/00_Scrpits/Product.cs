using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product
{
    public string name;
    public SpriteRenderer Icon;
    public int quantity;

    public Product(string name ,  SpriteRenderer Icon){
        this.name = name;
        this.Icon = Icon;
        quantity = 0;
    }
}
