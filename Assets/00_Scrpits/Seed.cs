
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Seed : MonoBehaviour , IPointerDownHandler, IPointerUpHandler , IDragHandler
{
    RectTransform rectTransform;
    Vector3 startPos;
    [SerializeField] GameObject seed;
    [SerializeField] private Tilemap soil;

    private void Start()
    {
       rectTransform = GetComponent<RectTransform>();
       startPos = transform.position;
          
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        rectTransform.position = Input.mousePosition;
        soil = GameManager.intant.grid.transform.GetChild(4).GetComponent<Tilemap>();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        rectTransform.transform.position = startPos; 
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
        Vector3Int mousePos = soil.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        Debug.Log(mousePos);
        if(soil.GetTile(mousePos) != null ) 
        {
            GameObject g = Instantiate(seed);
            g.transform.position = soil.CellToWorld(mousePos);
        }
    }
}
