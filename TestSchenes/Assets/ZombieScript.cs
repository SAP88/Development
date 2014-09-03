using UnityEngine;
using System.Collections;

public class ZombieScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
    void FixedUpdate()
    {
        //this.rigidbody2D.AddForce(new Vector2(-1, 0));
        this.rigidbody2D.velocity = new Vector2(-0.5f, 0);
    }
    
    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Finish")
        {
            
        }
    }


    
	// Update is called once per frame
	void Update () {
	
	}
}
