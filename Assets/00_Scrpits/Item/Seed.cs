
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Seed : MonoBehaviour , IPointerDownHandler, IPointerUpHandler , IDragHandler
{
    RectTransform rectTransform;
    Vector3 startPos;
    [SerializeField] GameObject vegetable;
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
        Handle.isSeed = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
        Vector3Int mousePos = soil.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if(soil.GetTile(mousePos) != null && (!GameManager.intant.tilePos.ContainsKey(mousePos) || GameManager.intant.tilePos[mousePos] == false)) 
        {
            GameObject g = ObjectPooling.intant.CreateGameObject(vegetable);
            g.transform.position = soil.CellToWorld(mousePos) + new Vector3(0.5f, 0.5f, 0);
            GameManager.intant.tilePos[mousePos] = true;
        }
        
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        rectTransform.transform.position = startPos;
        Handle.isSeed = false;
    }


}
