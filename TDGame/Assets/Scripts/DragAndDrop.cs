using UnityEngine;
using System.Collections;

public class DragAndDrop : MonoBehaviour
{
    float speed = 0.1f;
    void Update()
    {
        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) 
        if(Input.GetMouseButtonDown(0))
        {

            // Get movement of the finger since last frame
            //Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
 
            // Move object across XY plane
            //transform.Translate (-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);

            var currPos = Camera.main.camera.WorldToViewportPoint(Input.mousePosition);
            transform.position =  currPos;// Mo(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        }
    }
}
