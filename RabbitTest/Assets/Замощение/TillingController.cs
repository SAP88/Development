using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

[System.Serializable]
public class Field
{
    public int[] Row;
}

public class TillingController : MonoBehaviour
{
    private bool IsInitializeSuccessful = false;
    public GameObject PlacableDark;
    public GameObject PlacableLight;

    public GameObject MovableDark;
    public GameObject MovableLight;

    //Должны смотреть вниз
    public GameObject EdgeBottom;
    //Должны смотреть вниз
    public GameObject EdgePerimeter;

    //public int HorizontalCount;
    //public int VerticalCount;

    public Field[] GameField;

    private const int MOVEABLESPACE = 0;
    private const int PLACEABLESPACE = 1;

    //!!!!!!!!!!!!!!!!!!!!!!!!
    //Prefabs
    //http://anwell.me/articles/unity3d-flappy-bird/

	// Use this for initialization
	void Start () 
    {
        if(GameField == null || GameField.Length == 0 || GameField[0].Row == null)
        {
            Debug.LogError("IncorrectSize");
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

        if(!(IsInitializeSuccessful = sError.Length == 0)) return;

        string tmp = "";
        for (int i = 0; i < GameField.Length; i++)
        {
            tmp += GameField[i].Row[0].ToString();
        }
        Debug.Log(tmp);

        //for (int i = GameField.Length - 1; i >= 0; i--)
        //for (int j = 0; j < GameField[i].Row.Length; j++ )
        for (int i = 0; i < GameField.Length ; i++)
        for (int j = 0; j < GameField[i].Row.Length; j++)
        {
            bool divI = i % 2 == 0;
            bool divJ = j % 2 == 0;
            if (divJ)
            {
                //Dark
                if (divI)
                {
                    DarkDrawConditions(i, j);
                }
                else
                {
                    LightDrawConditions(i, j);
                }
            }
            else
            {
                //light
                if (divI)
                {
                    LightDrawConditions(i, j);
                }
                else
                {
                    DarkDrawConditions(i, j);
                    
                }
            }
        }
        
	}

    private void LightDrawConditions(int i, int j)
    {
        if (GameField[i].Row[j] == MOVEABLESPACE)
        {
            DrawMovableDark(i, j);
        }
        else if (GameField[i].Row[j] == PLACEABLESPACE)
        {
            DrawPlaceableDark(i, j);
            DrawBound(i, j);
        }
    }

    private void DarkDrawConditions(int i, int j)
    {
        if (GameField[i].Row[j] == MOVEABLESPACE)
        {
            DrawMovableLight(i, j);
        }
        else if (GameField[i].Row[j] == PLACEABLESPACE)
        {
            DrawPlaceableLight(i, j);
            DrawBound(i, j);
        }
    }

    private void DrawBound(int i, int j)
    {
        if(CheckBound(i + 1, j) && CheckIsMovableSpace(i + 1, j))
        {
            DrawRocks(i + 1, j);
        }
    }

    private bool CheckIsMovableSpace(int i, int j)
    {
        return GameField[i].Row[j] == MOVEABLESPACE;
    }

    private bool CheckBound(int i, int j)
    {
        return 0 <= i && i < GameField.Length && 0 <= j && j < GameField[i].Row.Length;
    }

    private GameObject DrawRocks(int i, int j)
    {
        var @return = (GameObject)Object.Instantiate(EdgeBottom, new Vector2(transform.position.x + j * MovableDark.renderer.bounds.size.x,
                    transform.position.y - i * MovableDark.renderer.bounds.size.y), Quaternion.identity);
        @return.name = string.Format("({0},{1}) _Rock ", i, j);
        return @return;
    }

    private GameObject DrawMovableDark(int i, int j)
    {
        
        var @return = (GameObject)Object.Instantiate(MovableDark, new Vector2(transform.position.x + j * MovableDark.renderer.bounds.size.x,
                    transform.position.y - i * MovableDark.renderer.bounds.size.y), Quaternion.identity);
        @return.name = string.Format("({0},{1}) _moveableDark ", i, j);
        return @return;
    }

    private GameObject DrawMovableLight(int i, int j)
    {
        var @return = (GameObject)Object.Instantiate(MovableLight, new Vector2(transform.position.x + j * MovableLight.renderer.bounds.size.x,
                    transform.position.y - i * MovableLight.renderer.bounds.size.y), Quaternion.identity);
        @return.name = string.Format("({0},{1}) _moveableLight", i, j);
        return @return;
    }

    private GameObject DrawPlaceableDark(int i, int j)
    {
        var @return = (GameObject)Object.Instantiate(PlacableDark, new Vector2(transform.position.x + j * PlacableDark.renderer.bounds.size.x,
                    transform.position.y - i * PlacableDark.renderer.bounds.size.y), Quaternion.identity);
        @return.name = string.Format("({0},{1}) _placeableDark ", i, j);
        return @return;
    }

    private GameObject DrawPlaceableLight(int i, int j)
    {
        var @return = (GameObject)Object.Instantiate(PlacableLight, new Vector2(transform.position.x + j * PlacableLight.renderer.bounds.size.x,
                    transform.position.y - i * PlacableLight.renderer.bounds.size.y), Quaternion.identity);
        @return.name = string.Format("({0},{1}) _placeableLight ", i, j);
        return @return;
    }

    void FixedUpdate()
    {
        if (!IsInitializeSuccessful)
        {
            UnInitialized();
            return;
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if (!IsInitializeSuccessful)
        {
            return;
        }

	}

    private void UnInitialized()
    {
        
    }
}


