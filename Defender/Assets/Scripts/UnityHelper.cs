using UnityEngine;
using System.Collections;

public static class UnityHelper
{
    public static bool IsAnyTouch(this MonoBehaviour mb)
    {
        Vector3 touchPos;
        if(Input.GetMouseButtonDown(0))
        {
            touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        }
        else
        {
            return false;
        }

        var test = Physics2D.OverlapPoint(touchPos);
        if (mb.collider2D == test)
        {
            return true;
        }
        return false;
    }
}