using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using Assets.Scripts;

[System.Serializable]
public class Field
{
    public int[] Row;
}

[System.Serializable]
public enum EnemyType
{
    Монстр1
}


[System.Serializable]
public class EnemyGroup
{
    //public Enemies[] Enemies;
    public int Count;
    public EnemyType EnemyType;
    public float CreationTimeout;
}

[System.Serializable]
public class EnemyWave
{
    public EnemyGroup[] GroupWave;
    public float NextWaveTimeout;
}

[ExecuteInEditMode]
public class TillingController : MonoBehaviour
{
    //public float SpawnCountDown; // С каким интервалом
    public float BeginAfter; // Через скока монстры должны пойти

    private bool IsInitializeSuccessful = false;
    public GameObject PlacableDark;
    public GameObject PlacableLight;

    public GameObject MovableDark;
    public GameObject MovableLight;

    //Должны смотреть вниз
    public GameObject EdgeBottom;
    //Должны смотреть вниз
    public GameObject EdgePerimeter;

    public GameObject Respawn;
    public GameObject Escape;


    //public int HorizontalCount;
    //public int VerticalCount;

    public Field[] GameField;
    public EnemyWave[] Waves;

    //!!!!!!!!!!!!!!!!!!!!!!!!
    //Prefabs
    //http://anwell.me/articles/unity3d-flappy-bird/

    #region Common Events (Start, Reset, OnMouseUp etc..)

    /// <summary>
    /// ТОлько для EditMode-а ([ExecuteInEditMode])
    /// </summary>
    public void Reset()
    {
        var fieldObjects = GameObject.FindGameObjectsWithTag("FieldObject");
        if (fieldObjects != null)
        {
            foreach (var fo in fieldObjects)
            {
				GameObject.DestroyImmediate(fo);
            }
        }

        fieldObjects = GameObject.FindGameObjectsWithTag("FieldObjectPosition");
        if (fieldObjects != null)
        {
            foreach (var fo in fieldObjects)
            {
				GameObject.DestroyImmediate(fo);
            }
        }

        fieldObjects = GameObject.FindGameObjectsWithTag("Exit");
        if (fieldObjects != null)
        {
            foreach (var fo in fieldObjects)
            {
                GameObject.DestroyImmediate(fo);
            }
        }

        fieldObjects = GameObject.FindGameObjectsWithTag("Spawn");
        if (fieldObjects != null)
        {
            foreach (var fo in fieldObjects)
            {
                GameObject.DestroyImmediate(fo);
            }
        }
    }

    void OnLevelWasLoaded(int level)
    {
        GameLevelController.Instance.GameFieldPosition = null;
    }

	// Use this for initialization
	void Start () 
    {
        Reset();
        if(GameField == null || GameField.Length == 0 || GameField[0].Row == null)
        {
            Debug.LogError("IncorrectSize");
            return;
        }

        StringBuilder sError = new StringBuilder();
        int len = GameField[0].Row.Length;
        for (int i = 0; i< GameField.Length; i++) 
        {
            var item = GameField[i];
            if(item.Row == null || item.Row.Length != len)
            {
                sError.AppendFormat("IncorrectRow {0}; ", i);
            }
        }

        if (!(IsInitializeSuccessful = sError.Length == 0))
        {
            Debug.LogError(sError);
            return;
        }

        GameLevelController.Instance.UpdateGameField(GameField);

        for (int i = 0; i < GameField.Length ; i++)
        for (int j = 0; j < GameField[i].Row.Length; j++)
        {
            MainGameObjectsCheck(i, j);
            bool divI = i % 2 == 0;
            bool divJ = j % 2 == 0;
            if (divJ) {
                //Логика для шахматных клеток тёмных
                if (divI) { 
                    DarkDrawConditions(i, j); 
                } else { 
                    LightDrawConditions(i, j); 
                }
            } else {
                //Логика для шахматных клеток светлых
                if (divI) {
                    LightDrawConditions(i, j);
                } else {
                    DarkDrawConditions(i, j);
                }
            }
        }

        GameLevelController.Instance.GameFieldPosition = this.transform;
	}

    private void MainGameObjectsCheck(int i, int j)
    {
        
        if (GameField[i].Row[j] == Helper.RESPAWN)
        {
            DrawRespawn(i, j);
        }
        else if (GameField[i].Row[j] == Helper.ESCAPE)
        {
            DrawEscape(i, j);
        }
    }

    #endregion

    #region Логика создания игровых шашечек
    /// <summary>
    /// Для светлых шахматных клеток рисует квадраты
    /// </summary>
    private void LightDrawConditions(int i, int j)
    {
        if (GameField[i].Row[j] == Helper.PLACEABLESPACE)
        {
            DrawPlaceableDark(i, j);
            DrawBound(i, j);
            
        }
        else if (false)
        {
            //Другие декоры
        }
        else 
        {
            //(GameField[i].Row[j] == MOVEABLESPACE)
            DrawMovableDark(i, j);
        }
    }

    /// <summary>
    /// Для темных шахматных клеток рисует квадраты
    /// </summary>
    private void DarkDrawConditions(int i, int j)
    {
        if (GameField[i].Row[j] == Helper.PLACEABLESPACE)
        { 
            DrawPlaceableLight(i, j);
            DrawBound(i, j);   
        } 
        else if(false) 
        {
            //Другие декоры
        }
        else
        {
            //(GameField[i].Row[j] == MOVEABLESPACE)
            DrawMovableLight(i, j);
        }
        
    }

    /// <summary>
    /// Рисует декорацию в i , j
    /// </summary>
    /// <param name="i"></param>
    /// <param name="j"></param>
    private void DrawBound(int i, int j)
    {
        if(CheckBound(i + 1, j) && CheckIsMovableSpace(i + 1, j))
        {
            DrawRocks(i + 1, j);
        }

		if(CheckBound(i, j + 1) && CheckIsMovableSpace(i, j + 1))
		{
			DrawPerimeter(i, j + 1, 90);
		}

        if (CheckBound(i - 1, j) && CheckIsMovableSpace(i - 1, j))
        {
            DrawPerimeter(i - 1, j, 180);
        }

        if (CheckBound(i, j - 1) && CheckIsMovableSpace(i, j - 1))
        {
            DrawPerimeter(i, j - 1, -90);
        }
    }

    private bool CheckIsMovableSpace(int i, int j)
    {
        return GameField[i].Row[j] == Helper.MOVEABLESPACE || GameField[i].Row[j] == Helper.ESCAPE || GameField[i].Row[j] == Helper.RESPAWN;
    }

    private bool CheckBound(int i, int j)
    {
        return 0 <= i && i < GameField.Length && 0 <= j && j < GameField[i].Row.Length;
    }
    #endregion

    #region ОТРИСОВКА респауна, выхода

    private GameObject DrawEscape(int i, int j)
    {
        var @return = (GameObject)Object.Instantiate(Escape, new Vector2(transform.position.x + j * Escape.renderer.bounds.size.x,
                    transform.position.y - i * Escape.renderer.bounds.size.y), Quaternion.identity);

        @return.name = string.Format("({0},{1}) MonstersEscape", i, j);

        @return.tag = "Exit";
        //var respScript = @return.AddComponent<RespawnScript>();
        //respScript.GameField = GameField;
        //respScript.BeginAfter = BeginAfter;
        //respScript.SpawnCountDown = SpawnCountDown;

        return @return;
    }

    private GameObject DrawRespawn(int i, int j)
    {
        var @return = (GameObject)Object.Instantiate(Respawn, new Vector2(transform.position.x + j * Respawn.renderer.bounds.size.x,
                    transform.position.y - i * Respawn.renderer.bounds.size.y), Quaternion.identity);

        @return.name = string.Format("({0},{1}) MonstersRespawn", i, j);

        var respScript = @return.AddComponent<RespawnScript>();
        respScript.GameField = GameField;
        respScript.BeginAfter = BeginAfter;
        //respScript.SpawnCountDown = SpawnCountDown;
        respScript.Waves = Waves;
        @return.tag = "Spawn";

        return @return;
    }
    #endregion

    #region ОТРИСОВКА декоративных объектов
    private GameObject DrawPerimeter(int i, int j, int angle)
	{
		var @return = (GameObject)Object.Instantiate(EdgePerimeter, new Vector2(transform.position.x + j * EdgePerimeter.renderer.bounds.size.x,
                    transform.position.y - i * EdgePerimeter.renderer.bounds.size.y), Quaternion.identity);

		@return.name = string.Format("({0},{1}) _Perimeter", i, j);
        
        @return.transform.Rotate(0, 0, angle);
        return MarkTagParent(@return, "FieldObject");
	}
    private GameObject DrawRocks(int i, int j)
    {
        var @return = (GameObject)Object.Instantiate(EdgeBottom, new Vector2(transform.position.x + j * MovableDark.renderer.bounds.size.x,
                    transform.position.y - i * MovableDark.renderer.bounds.size.y), Quaternion.identity);

        @return.name = string.Format("({0},{1}) _Rock ", i, j);
        return MarkTagParent(@return, "FieldObject");
    }
    #endregion

    #region ОТРИСОВКА объектов поля (Шашечки)
    private GameObject DrawMovableDark(int i, int j)
    {
        
        var @return = (GameObject)Object.Instantiate(MovableDark, new Vector2(transform.position.x + j * MovableDark.renderer.bounds.size.x,
                    transform.position.y - i * MovableDark.renderer.bounds.size.y), Quaternion.identity);

        @return.name = string.Format("({0},{1}) _moveableDark ", i, j);
        return MarkTagParent(@return, "FieldObjectPosition");
    }

    private GameObject DrawMovableLight(int i, int j)
    {
        var @return = (GameObject)Object.Instantiate(MovableLight, new Vector2(transform.position.x + j * MovableLight.renderer.bounds.size.x,
                    transform.position.y - i * MovableLight.renderer.bounds.size.y), Quaternion.identity);

        @return.name = string.Format("({0},{1}) _moveableLight", i, j);
        return MarkTagParent(@return, "FieldObjectPosition");
    }

    private GameObject DrawPlaceableDark(int i, int j)
    {
        var @return = (GameObject)Object.Instantiate(PlacableDark, new Vector2(transform.position.x + j * PlacableDark.renderer.bounds.size.x,
                    transform.position.y - i * PlacableDark.renderer.bounds.size.y), Quaternion.identity);

        @return.name = string.Format("({0},{1}) _placeableDark ", i, j);
        return MarkGameFieldObject(MarkTagParent(@return, "FieldObjectPosition"), i, j);
    }

    private GameObject DrawPlaceableLight(int i, int j)
    {
        var @return = (GameObject)Object.Instantiate(PlacableLight, new Vector2(transform.position.x + j * PlacableLight.renderer.bounds.size.x,
                    transform.position.y - i * PlacableLight.renderer.bounds.size.y), Quaternion.identity);

        
        @return.name = string.Format("({0},{1}) _placeableLight ", i, j);
        return MarkGameFieldObject(MarkTagParent(@return, "FieldObjectPosition"), i, j);
    }
    #endregion

    private GameObject MarkTagParent(GameObject @return, string tag)
    {
        @return.transform.parent = transform.parent;
        @return.tag = tag;
        return @return;
    }

    private GameObject MarkGameFieldObject(GameObject gameObj, int i, int j)
    {
        var so = gameObj.AddComponent("GameFieldScript");
        //so.SendMessage("SetCoordinates", new object[] { i, j });
        var boxCollider = gameObj.AddComponent("BoxCollider2D") as BoxCollider2D;
        return gameObj;
    }
}


