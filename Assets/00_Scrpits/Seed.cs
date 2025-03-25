
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Seed : MonoBehaviour , IPointerDownHandler, IPointerUpHandler
{
    RectTransform rectTransform;
    Vector3 startPos;
 
    private void Start()
    {
       rectTransform = GetComponent<RectTransform>();
       startPos = transform.position;
           
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        rectTransform.position = Input.mousePosition;   
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        rectTransform.transform.position = startPos; 
    }
}
