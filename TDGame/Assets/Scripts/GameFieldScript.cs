using UnityEngine;
using System.Collections;
using Assets.Scripts;

/// <summary>
/// Поля по которым будут жмякать что бы добавить башенку
/// </summary>
public class GameFieldScript : MonoBehaviour {
    private int i = -1;
    private int j = -1;
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
        if (GameLevelController.Instance.CurrentSelectedTower == ActionType.None)
        {
            return;
        }

        if (Tower != null) 
        {
            Debug.Log("Попытка поставить башеку на место где уже есть башенка");
            return;
        }

        Tower = (GameObject)GameObject.Instantiate(GameLevelController.Instance.CurrentSelectedTowerGameObject, transform.position, transform.rotation);
        var towerScript = (CommonTowerScript)Tower.GetComponent(typeof(CommonTowerScript));
        towerScript.Bullet = GameLevelController.Instance.CurrentSelectedTowerBullet;
    }

    public void SetCoordinates(object[] coords)
    {
        if (coords == null)
        {
            Debug.LogError("coords is null for " + name);
            return;
        }

        i = (int)coords[0];
        j = (int)coords[1];
    }
}
