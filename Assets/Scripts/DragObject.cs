using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour
{
    #region Fields
    //  The RectTransform of the GameObject this scipt is attched to
    RectTransform rectTransform;

    //  The RectTransform of the parent GameObject this scipt is attched to
    RectTransform parentRectTransform;

    //  The calculated Position of any Pointer touching this GameObject
    [SerializeField]
    Vector2 touchPos;
    #endregion

    // Use this for initialization
    void Start () {
        //  Get the RectTransform attched to this GameObject
        rectTransform = GetComponent<RectTransform>();

        //  Get the RectTransform attached to this GameObject's parent
        parentRectTransform = transform.parent.GetComponentInParent<RectTransform>();
    }

    /// <summary>
    /// Move this GameObject to Position of a Pointer Clicked on this GameObject
    /// </summary>
    /// <param name="eventData">Data from the Input System about the Pointer (PointerEventData)</param>
    public void OnDrag(BaseEventData eventData)
    {
        //  Get the Position of the pointer in the Canvas Space
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentRectTransform,                                    //  Item in the Canvas
            ((PointerEventData)eventData).position,                 //  Original Pointer Position
            Camera.main,                                            //  The Camera the Canvas is using
            out touchPos);                                          //  A value that returns the calculated Position

        //  Set the center of Canvas item to the calcuated Postion of the Pointer
        rectTransform.anchoredPosition = touchPos;
    }
}
