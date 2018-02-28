using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    float speed;

    const string k_HORIZONTAL = "Horizontal";
    const string k_VERTICAL = "Vertical";

    RectTransform rectTransform;

    [SerializeField]
    Vector2 boardSize;

    // Use this for initialization
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        boardSize = transform.parent.GetComponentInParent<RectTransform>().rect.size / 2f;
	}
	
	// Update is called once per frame
	void Update()
    {
#if UNITY_EDITOR
        Move(Input.GetAxis(k_HORIZONTAL), Input.GetAxis(k_VERTICAL));
#else
        Move(Input.acceleration.x, Input.acceleration.y);
#endif

    }

    void Move(float x, float y)
    {
        Vector2 newPos = rectTransform.anchoredPosition;
        newPos.x += x * speed;
        newPos.y += y * speed;

        if (newPos.x < -boardSize.x)
            newPos.x = -boardSize.x;
        else if (newPos.x > boardSize.x)
            newPos.x = boardSize.x;

        if (newPos.y < -boardSize.y)
            newPos.y = -boardSize.y;
        else if (newPos.y > boardSize.y)
            newPos.y = boardSize.y;

        rectTransform.anchoredPosition = newPos;
    }
}
