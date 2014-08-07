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
	void Update()
    {
        bool isTargetInCircle = false;
        foreach (var collider in Physics2D.OverlapCircleAll(this.transform.position, ShootingRadius))
        {
            if(collider.gameObject == TargetEnemy)
            {
                isTargetInCircle = true;
                break;
            }
        }

        if(!isTargetInCircle)
        {
            TargetEnemy = GetNextEnemy();
        }

        if (TargetEnemy != null && Time.time - LastShootTime > CountDown)
        {
            LastShootTime = Time.time;

            GameObject newBullet = (GameObject)GameObject.Instantiate(Bullet, transform.position, Quaternion.identity);
            newBullet.GetComponent<SpriteRenderer>().sortingLayerName = "Пули";
            newBullet.AddComponent(typeof(Rigidbody2D));
            var bulletScript = newBullet.AddComponent<BulletScript>();
            bulletScript.Target = TargetEnemy.transform;
            bulletScript.Damage = 1.0f;
            newBullet.rigidbody2D.gravityScale = 0;
        }
    }

    private GameObject GetNextEnemy()
    {
        foreach (var collider in Physics2D.OverlapCircleAll(this.transform.position, ShootingRadius))
        {
            if (collider.tag == "GameEnemy")
            {
                return collider.gameObject;
            }
        }
        return null;
    }

    public float targetHeading;

    //void OnTriggerStay2D(Collider other)
    //{
    //    if(other.tag == "Enemy")
    //    {

    //    }
    //}
}
