using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(SpriteRenderer))]
public class ChangeColor : MonoBehaviour
{
    SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
	}

    public void NewColor()
    {
        sprite.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
}
