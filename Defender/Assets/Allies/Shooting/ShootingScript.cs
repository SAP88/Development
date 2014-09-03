using UnityEngine;
using System.Collections;

/// <summary>
/// Проверяет есть ли на пути враг. Если есть то создаёт пулю из "GameObject Bullet"
/// </summary>
public class ShootingScript : MonoBehaviour {

    public LayerMask MonsterLayer;
    public float ShootingPeriod = 1;

    bool doShooting = false;
    float lastShootingTime;

    public GameObject Bullet;

    void Start()
    {
        lastShootingTime = Time.time;
    }

	void FixedUpdate()
	{
        var rayRes = Physics2D.Raycast(this.transform.position, new Vector2(1, 0), float.MaxValue, MonsterLayer);
        if (rayRes.collider != null && rayRes.collider.gameObject.tag == "Enemy")
        {
            doShooting = true;
        }
        else
        {
            doShooting = false;
        }
	}
	
    void Update()
    {
        if (Bullet == null) return;

        if (doShooting && Time.time - lastShootingTime > ShootingPeriod)
        {
            Debug.Log("Shooting");
            GameObject.Instantiate(Bullet, this.transform.position, Quaternion.identity);
            lastShootingTime = Time.time;
        }
    }
}
