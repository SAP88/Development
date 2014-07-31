using UnityEngine;
using System.Collections;

public class CommonTowerScript : MonoBehaviour {
    private GameObject TargetEnemy = null;
    public float ShootingRadius = 5.8f;
    public float CountDown = 2.5f;
    private float LastShootTime = 0;
    public GameObject Bullet;
	// Use this for initialization
	void Start () {
        LastShootTime = Time.time;
	}

    //Does Unity null all references to a GameObject after Object.Destroy is called on it? If so, how?
    //http://answers.unity3d.com/questions/10032/does-unity-null-all-references-to-a-gameobject-aft.html
	void FixedUpdate()
    {
        if (TargetEnemy == null)
        {
            foreach (var collider in Physics2D.OverlapCircleAll(this.transform.position, ShootingRadius))
            {
                if (collider.tag == "GameEnemy")
                {
                    TargetEnemy = collider.gameObject;
                    break;
                }
            }
            if (TargetEnemy == null) return;
        }

        if(Time.time - LastShootTime > CountDown)
        {
            LastShootTime = Time.time;
            GameObject bullet = (GameObject)GameObject.Instantiate(Bullet, transform.position, Quaternion.identity);

            Bullet.rigidbody2D.AddForce(TargetEnemy.transform.position);
        }
    }

	// Update is called once per frame
	void Update () {
	    
	}   

    //void OnTriggerStay2D(Collider other)
    //{
    //    if(other.tag == "Enemy")
    //    {

    //    }
    //}
}
