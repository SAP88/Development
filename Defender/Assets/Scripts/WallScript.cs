using UnityEngine;
using System.Collections;

/// <summary>
/// Уничтожаем пули что бы они не летели бесконечно
/// </summary>
public class WallScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
        if(other.gameObject.tag == "Bullets")
        {
            Destroy(other.gameObject);
        }
	}
}
