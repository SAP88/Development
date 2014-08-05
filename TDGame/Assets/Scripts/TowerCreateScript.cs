using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class TowerInfo
{
    public float ShootingRadius { get; set; }
    public float CountDown { get; set; }
}

public class TowerCreateScript : MonoBehaviour
{
    public GameObject TowerBullet;
    public Texture2D TowerGameObjectReference;

    public ActionType Action;

    public float ShootingRadius = 5.8f;
    public float CountDown = 2.5f;

	// Use this for initialization
	void Start () 
    {
        Helper.ApplyDeselected(this);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
    void OnLevelWasLoaded(int level)
    {
        //CurrentTower = null;
        CurrentAction = ActionType.None;
        CurrentTowerBullets = null;
    }


    //public static GameObject CurrentTower { get; set; }
    public static GameObject CreateTower() //ActionType action, TowerInfo tInfo)
    {
        ActionType action = CurrentAction;
        TowerInfo tInfo = CurrentTowerInfo;
        GameObject newTower = null;
        switch (action)
        {
            case ActionType.CreateCommonTower:
                newTower = new GameObject("Башенка");

                var comTowerScript = newTower.AddComponent<CommonTowerScript>();
                comTowerScript.ShootingRadius = tInfo.ShootingRadius;
                comTowerScript.CountDown = tInfo.CountDown;
                var newTowerSprite = newTower.AddComponent<SpriteRenderer>();
                newTowerSprite.sortingLayerName = "Башенки";
                var tower1 = Resources.LoadAssetAtPath<Texture2D>("Assets/Картинки/Башня1.psd");

                newTowerSprite.sprite =
                    Sprite.Create(tower1, new Rect(0, 0, tower1.width, tower1.height), new Vector2(0, 0));
                
                //var towerScript = (CommonTowerScript)Tower.GetComponent(typeof(CommonTowerScript));
                comTowerScript.Bullet = TowerCreateScript.CurrentTowerBullets;
                //var thisTexture = this.GetComponent<SpriteRenderer>().sprite.texture;
                //newTowerSprite.sprite = Sprite.Create(thisText, new Rect(0, 0, thisText.width, thisText.height), new Vector2(0, 0));

                break;
        }

        return newTower;
    }

    private static GameObject CurrentTowerBullets { get; set; }
    public static ActionType CurrentAction { get; set; }
    public static TowerInfo CurrentTowerInfo { get; set; }

    void OnMouseDown()
    {
        if (CurrentAction == Action)
        {
            CurrentAction = ActionType.None;
            CurrentTowerBullets = null;
            Helper.ApplyDeselected(this);
        }
        else
        {
            CurrentAction = Action;
            CurrentTowerBullets = TowerBullet;
            CurrentTowerInfo = new TowerInfo { CountDown = CountDown, ShootingRadius = ShootingRadius };
            Helper.ApplySelected(this);

            
            
        }
    }

    
}
