using UnityEngine;
using System.Collections;
using System;

public class EnemyMovement : MonoBehaviour {
    public bool IsLeft = true;
    public float Speed = 0.5f;

    Animator anim = null;

    bool isInited = false;
    void Start()
    {
        anim = this.GetComponent<Animator>();
        if(anim == null)
        {
            isInited = false;
            throw new Exception("Enemy don't have an animator");
        }

        if (this.rigidbody2D == null)
        {
            isInited = false;
            throw new Exception("rigidbody2D is null");
        }

        isInited = true;

        
    }

    void Update()
    {
        if (!isInited) return;

        
    }
    
    void FixedUpdate()
    {
        if(!isInited) return;


        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Die"))
        {
            this.rigidbody2D.velocity = new Vector2(0, 0);
        }
        else
        {
            this.rigidbody2D.velocity = new Vector2(IsLeft ? -1 * Speed : Speed, 0);
        }
    }	
}
