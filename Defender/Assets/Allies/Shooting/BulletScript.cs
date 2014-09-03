using UnityEngine;
using System.Collections;

/// <summary>
/// Движение пули
/// </summary>
public class BulletScript : MonoBehaviour {
    public float Speed = 1;
    public float Damage = 25;

    Animator anim;

    void Start()
    {
        anim = this.GetComponent<Animator>();
    }


	void FixedUpdate()
	{
        this.rigidbody2D.velocity = new Vector2(1 * Speed, 0);
	}

    void Update()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsTag("Die"))
        {
            Destroy(this.gameObject);
        }
    }


    public void UseBullet()
    {
        anim.SetBool("isHit", true);
        Destroy(this.collider2D);
        Damage = 0;
    }
		
}
