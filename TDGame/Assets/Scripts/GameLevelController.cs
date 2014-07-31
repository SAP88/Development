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

    public class FieldInfo
    {
        public GameObject Tower { get; set; }
        public int SourceValue { get; set; }
    }

    public class GameLevelController : INotifyPropertyChanged
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

        public FieldInfo[,] GameField
        {
            get;
            private set;
        }

        public void UpdateGameField(Field[] gField)
        {
            if (gField == null || gField.Length == 0) 
            {
                Debug.LogError("gField is null or empty");
                return;
            }
            int w = gField[0].Row.Length;
            int h = gField.Length;

            try
            {
                GameField = new FieldInfo[h, w];
                for (int i = 0; i < h; i++)
                    for (int j = 0; j < w; j++)
                    {
                        GameField[i, j] = new FieldInfo() { SourceValue = gField[i].Row[j] };
                    }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private ActionType _currentSelectedTower = ActionType.None;
        public ActionType CurrentSelectedTower
        {
            get { return _currentSelectedTower; }
            set 
            { 
                _currentSelectedTower = value; 
                FirePropertyChanged("CurrentSelectedTower"); 
            }
        }

        public GameObject CurrentSelectedTowerGameObject
        {
            get;
            set;
        }

        public GameObject CurrentSelectedTowerBullet
        {
            get;
            set;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void FirePropertyChanged(string propName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
