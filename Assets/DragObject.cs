using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour
{
    [SerializeField]
    RectTransform rectTransform;

    [SerializeField]
    Vector2 boardSize;

    // Use this for initialization
    void Start () {
        boardSize = transform.parent.GetComponentInParent<RectTransform>().rect.size / 2f;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerDown(BaseEventData eventData)
    {
        
    }

    public void OnDrag(BaseEventData eventData)
    {
        rectTransform.anchoredPosition = ((PointerEventData)eventData).position - boardSize;
    }
}
