using UnityEngine;
using System.Collections;

public class CreateTower : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    ScriptableObject s;
    Vector2 screenSpace;
    Vector2 offset;

	// Update is called once per frame
	void Update () {
	    
	}

    void OnMouseDown()
    {
        //translate the cubes position from the world to Screen Point
        screenSpace = Camera.main.WorldToScreenPoint(transform.position);

        //calculate any difference between the cubes world position and the mouses Screen position converted to a world point  
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
    }

    /*
    OnMouseDrag is called when the user has clicked on a GUIElement or Collider and is still holding down the mouse.
    OnMouseDrag is called every frame while the mouse is down.
    */

    void OnMouseDrag()
    {
        ////keep track of the mouse position
        //var curScreenSpace = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        ////convert the screen mouse position to world point and adjust with offset
        //var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;

        ////update the position of the object in the world
        //transform.position = curPosition;
    }
}
