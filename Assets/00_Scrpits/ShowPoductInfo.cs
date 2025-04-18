using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowPoductInfo : MonoBehaviour, IPointerEnterHandler , IPointerExitHandler
{
    public GameObject info;
    public TextMeshProUGUI quantity;

    private void Awake()
    {
        info.SetActive(false);  
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        info.SetActive(true);
        quantity.text = "Quantity: " + GameManager.intant.AssetsItem[this.name.ToLower()].quantity;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       info.SetActive(false);
    }


   
}
