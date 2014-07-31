using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(BoxCollider2D), typeof(CircleCollider2D))]
public class DragAndDrop : MonoBehaviour
{
    

    Vector2 offset;
    void Update()
    {
       
    }

	void OnMouseDrag()
	{
		//var curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y);\

        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var curPosition = new Vector2(mousePos.x, mousePos.y) + offset;
		transform.position = curPosition;
	}

	void OnMouseDown()
	{
		var screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
	}

	void OnMouseUp()
	{

        //Camera.main.orthographicSize *= 1.1f;
        
	}
}
