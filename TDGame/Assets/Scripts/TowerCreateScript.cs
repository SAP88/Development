using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class TowerCreateScript : MonoBehaviour
{
    public GameObject TowerBullet;
    public GameObject TowerGameObjectReference;

    public ActionType Action;

	// Use this for initialization
	void Start () 
    {

        Helper.ApplyDeselected(this);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}


    void OnMouseDown()
    {
        if (GameLevelController.Instance.CurrentSelectedTower == Action)
        {
            GameLevelController.Instance.CurrentSelectedTower = ActionType.None;
            GameLevelController.Instance.CurrentSelectedTowerGameObject = null;
            GameLevelController.Instance.CurrentSelectedTowerBullet = null;
            Helper.ApplyDeselected(this);
        }
        else
        {
            GameLevelController.Instance.CurrentSelectedTower = Action;
            GameLevelController.Instance.CurrentSelectedTowerGameObject = TowerGameObjectReference;
            GameLevelController.Instance.CurrentSelectedTowerBullet = TowerBullet;
            Helper.ApplySelected(this);
        }
    }
}
