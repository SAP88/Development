using UnityEngine;
using System.Collections;

public class CoordsInfo : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void OnGUI()
    {
        var thisObjectPosition = Camera.main.WorldToScreenPoint(this.transform.position);
        GUI.Label(new Rect(thisObjectPosition.x, Screen.height - thisObjectPosition.y, 30, 140), 
            string.Format("{0,6}\n\r{1,6}", transform.position.x, transform.position.y), new GUIStyle() { fontSize = 10 });
    }

}
