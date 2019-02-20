using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Image))]
public class ChangeColor : MonoBehaviour
{
    [SerializeField]
    Image image;

	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
	}
	
	public void NewColor()
    {
        image.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
}
