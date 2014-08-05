using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{
    public float Damage;
    public Transform Target;
    public float Speed = 10f;
    
    public void Update()
    {
        if(Target != null)
        {
            var offset = this.transform.position - Target.position;
            this.transform.position += (Target.position - this.transform.position) * Speed * Time.deltaTime;
            return;
        }
        
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject == Target.gameObject)
        {
            var health = coll.gameObject.GetComponent<MonsterHealth>();
            if (health != null)
            {
                coll.gameObject.GetComponent<MonsterHealth>().DoDamage(Damage);
            }
            else
            {
                Debug.Log("Монст должен иметь скрипт MonsterHealth");
            }
            Destroy(gameObject);
        }
    }

    //void OnCollisionStay2D(Collision2D coll)
    //{
    //    if (coll.gameObject == Target.gameObject)
    //    {
    //        DoDamage();
    //    }
    //}

}

/*
/// <summary>
/// http://forum.boolean.name/showthread.php?t=13339
/// </summary>
public class BulletScript : MonoBehaviour
{
    // цель для ракеты
    public Transform Target;
    // префаб взрыва
    public GameObject explosionPrefab;
    // скорость ракеты
    public float speed = 5;
    // скорость поворота ракеты
    public float turnSpeed = 100;
    // время до взрыва
    public float explosionTime = 5;


    // трансформ текущего объекта для оптимизации обращений к нему
    private Transform _thisTransform;

    public void Awake()
    {
        _thisTransform = transform;
    }

    public void Update()
    {
        // уменьшаем таймер
        explosionTime -= Time.deltaTime;

        // если время таймера истекло, то взрываем ракету
        if (explosionTime <= 0)
        {
            Explode();
            return;
        }


        // величина движения вперед
        Vector2 movement = _thisTransform.forward * speed * Time.deltaTime;

        // если указана цель
        if (Target != null)
        {
            // направление на цель
            Vector2 direction = Target.position - _thisTransform.position;

            // максимальный угол поворота в текущий кадр
            float maxAngle = turnSpeed * Time.deltaTime;

            // угол между направлением на цель и направлением ракеты
            float angle = Vector2.Angle(_thisTransform.forward, direction);

            if (angle <= maxAngle)
            {
                // угол меньше максимального, значит поворачиваем на цель
                _thisTransform.forward = direction.normalized;
            }
            else
            {
                ////сферическая интерполяция направления с использованием максимального угла поворота
                //_thisTransform.forward = Vector3.Slerp(_thisTransform.forward, direction.normalized, maxAngle / angle);
                _thisTransform.forward = Vector2.Lerp(_thisTransform.forward, direction.normalized, maxAngle / angle);
            }

            // расстояние до цели
            float distanceToTarget = direction.magnitude;

            // если расстояние мало, то создаем взрыв
            if (distanceToTarget < movement.magnitude)
            {
                Explode();
                return;
            }
        }

        // двигамет ракету вперед
        _thisTransform.position += new Vector3(movement.x, movement.y);
    }

    public void Explode()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, _thisTransform.position, _thisTransform.rotation);
        }
        // уничтожаем ракету
        Destroy(gameObject);
    }

    // взрываем при коллизии
    public void OnCollisionEnter()
    {
        Explode();
    }
}
*/