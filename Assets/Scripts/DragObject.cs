using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour
{
    RectTransform rectTransform;
    RectTransform parentRectTransform;

    [SerializeField]
    Vector2 touchPos;

    // Use this for initialization
    void Start () {
        rectTransform = GetComponent<RectTransform>();
        parentRectTransform = transform.parent.GetComponentInParent<RectTransform>();
    }

    public void OnDrag(BaseEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, ((PointerEventData)eventData).position, Camera.main, out touchPos);
        rectTransform.anchoredPosition = touchPos;
    }
}
