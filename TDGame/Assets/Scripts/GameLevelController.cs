using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public enum ActionType
	{
        None,
	    CreateCommonTower,
	}

    //public class FieldInfo
    //{
    //    public GameObject Tower { get; set; }
    //    public int SourceValue { get; set; }
    //}

    public class GameLevelController // : INotifyPropertyChanged
    {
        private GameLevelController()
        {

        }

        private static GameLevelController _instance = null;
        public static GameLevelController Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new GameLevelController();
                }

                return _instance;
            }
        }

        public int[,] GameField
        {
            get;
            private set;
        }

        public Transform GameFieldPosition { get; set; }

        public IList<Vector2> ShortestPath { get; private set; }
        public Vector2 Respawn { get; private set; }
        public Vector2 Escape { get; private set; }

        public void UpdateGameField(Field[] gField)
        {
            if (gField == null || gField.Length == 0) 
            {
                Debug.LogError("gField is null or empty");
                return;
            }
            int w = gField[0].Row.Length;
            int h = gField.Length;

            GameField = new int[h, w];

            int[] walls = new int[] { Helper.PLACEABLESPACE };
            //List<int> walls = new List<int>();
            for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
            {
                GameField[i, j] = gField[i].Row[j];

                if(gField[i].Row[j] == Helper.RESPAWN)
                {
                    Respawn = new Vector2(j, i);
                }
                else if (gField[i].Row[j] == Helper.ESCAPE)
                {
                    Escape = new Vector2(j, i);
                }
            }

            ShortestPath = WaveAlgorithm.FindShortestPath(GameField, walls, (int)Respawn.x, (int)Respawn.y, (int)Escape.x, (int)Escape.y);
        }

    }
}
