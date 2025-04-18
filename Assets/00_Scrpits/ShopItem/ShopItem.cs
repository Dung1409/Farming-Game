
using TMPro;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI Quantity;
    [SerializeField] private TextMeshProUGUI Price;
    [SerializeField] private int maxQuantity = 20;
    private int quantity;
    private int price;  

    void UpdateInfo()
    {
        Quantity.text = "Quantity: " + quantity.ToString();
        price = quantity * 10;
        Price.text = "Price: " + price.ToString(); 
    }

    public void AddItem()
    {
        if( ShopManager.intant.Coin == 0 || ShopManager.intant.Coin - 10 < 0 || quantity >= maxQuantity)
        {
            return;
        }
        quantity = Mathf.Min(maxQuantity, quantity + 1);
        ShopManager.intant.seeds[name.text.ToLower()].seed += 1;
        ShopManager.intant.UpdateCoin(-10);
        UpdateInfo();
    }

    public void SubtractItem()
    {
        if(quantity == 0)
        {
            return;
        }
        quantity = Mathf.Max(quantity - 1, 0);
        ShopManager.intant.seeds[name.text.ToLower()].seed -= 1;
        ShopManager.intant.UpdateCoin(10);
        UpdateInfo();   
    }

    private void OnDisable()
    {
        quantity = 0;
        UpdateInfo();
    }
}
