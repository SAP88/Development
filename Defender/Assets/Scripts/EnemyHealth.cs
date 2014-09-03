using UnityEngine;
using System.Collections;

/// <summary>
/// Жизни врага, отвечает за уничтожение врага после перехода аниматора в состояние tag == "Die"
/// </summary>
public class EnemyHealth : MonoBehaviour {
    public float Health = 100;
    Animator anim = null;

    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (Health < 0)
        {
            anim.SetBool("Headshot", true);
            return;
        }

        if (anim == null) return;
        DealDamage(other);
    }

    void Update()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsTag("Die"))
        {
            Destroy(this.gameObject);
        }
    }

    private void DealDamage(Collider2D coll)
    {
        var bScr = coll.gameObject.GetComponent<BulletScript>();
        if (bScr != null)
        {
            Health = Health - bScr.Damage;
            bScr.UseBullet();
        }
    }


}
