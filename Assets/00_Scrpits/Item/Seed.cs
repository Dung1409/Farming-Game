
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
    public GameObject vegetable;
    [SerializeField] private Tilemap soil;
    public int seed = 30;
    public int curSeed;
    public string name;
    private void Start()
    {
       curSeed = seed;
       rectTransform = GetComponent<RectTransform>();
       startPos = transform.position;
       name = vegetable.name;
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
            if(seed <= 0)
            {
                return;
            }
            seed -= 1;
            GameObject g = ObjectPooling.intant.CreateGameObject(vegetable);
            g.transform.position = soil.CellToWorld(mousePos) + new Vector3(0.5f, 0.5f, 0);
            GameManager.intant.tilePos[mousePos] = true;
        }
        
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        rectTransform.transform.position = startPos;
        Handle.isSeed = false;
        curSeed = seed;
    }


}
