using UnityEngine;
using System.Collections;
using Assets.Scripts;

/// <summary>
/// Поля по которым будут жмякать что бы добавить башенку
/// </summary>
public class GameFieldScript : MonoBehaviour {

    private GameObject Tower = null;
	// Use this for initialization
	void Start () {
        //if(Tower != null)
        //{
        //    Tower.transform.gameObject.SetActive(false);
        //}
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnMouseDown()
    {
        if (TowerCreateScript.CurrentAction == ActionType.None)
        {
            return;
        }

        if (Tower != null) 
        {
            Debug.Log("Попытка поставить башеку на место где уже есть башенка");
            return;
        }

        var newTower = TowerCreateScript.CreateTower();
        if(newTower == null)
        {
            Debug.LogError("Не удалось создать башенку типа \"" + TowerCreateScript.CurrentAction + "\"");
            return;
        }

        var size = newTower.GetComponent<SpriteRenderer>().bounds.size;
        newTower.transform.position = this.transform.position;

        newTower.transform.position = new Vector2(newTower.transform.position.x - size.x / 2, newTower.transform.position.y - size.y / 2);
        newTower.transform.rotation = this.transform.rotation;
    }
}
