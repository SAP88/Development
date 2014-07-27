using UnityEngine;
using System.Collections;

public class RectangleObjectGenerator : MonoBehaviour {
    public GameObject Object;
	// Use this for initialization
	void Start () {
	
	}

    void CreateObstacle()
    {
        Instantiate(Object);
    }

	// Update is called once per frame
    //void Update () {
	
    //}
}
