using UnityEngine;
using System.Collections;


public class MonsterHealth : MonoBehaviour {
    public float CurrentHealth;
    public float MaxHealth;

    public Texture2D TEST;
    public Texture2D TEST2;

    void Start()
    {
        //var wantedPos = Camera.main.WorldToScreenPoint(this.transform.position);
        //transform.position = wantedPos;
        
    }

    private float BarDisplay
    {
        get
        {
            return CurrentHealth / (MaxHealth / 100) / 100;
        }
    }

    private Texture2D _progressBarEmpty = null;
    Texture2D ProgressBarEmpty
    {
        get
        {
            if(_progressBarEmpty == null)
            {
                _progressBarEmpty = Resources.LoadAssetAtPath<Texture2D>("Assets/Картинки/ПустаяПолоскаЖизнецМонстров.psd");
            }
            return _progressBarEmpty;
        }
     }

    private Texture2D _progressBarFull = null;
    Texture2D ProgressBarFull
    {
        get
        {
            if (_progressBarFull == null)
            {
                _progressBarFull = Resources.LoadAssetAtPath<Texture2D>("Assets/Картинки/ПустаяПолоскаЖизнецМонстров.psd");
            }
            return _progressBarFull;
        }
    }

    private Vector2? _size = null;
    Vector2 TextureSize
    {
        get
        {
            if(_size == null)
            {
                _size = new Vector2(ProgressBarFull.width, ProgressBarFull.height);
            }
            return _size.Value;
        }
    }

    void OnGUI()
    {
        //http://answers.unity3d.com/questions/11892/how-would-you-make-an-energy-bar-loading-progress.html
        var pos = Camera.main.WorldToScreenPoint(this.transform.position);
        pos.y = Screen.height - pos.y; //pos в центре монстра

        pos.y = pos.y - this.renderer.bounds.size.y / 2 - TextureSize.y;
        pos.x = pos.x - this.renderer.bounds.size.x / 2;

        //var pos = this.transform.position;

        //GUI.BeginGroup(new Rect(pos.x, pos.y, TextureSize.x, TextureSize.y));
        //    GUI.Box(new Rect(0, 0, TextureSize.x, TextureSize.y), ProgressBarEmpty);

        //    // draw the filled-in part:
        //    GUI.BeginGroup(new Rect(0, 0, TextureSize.x * BarDisplay, TextureSize.y));
        //        GUI.Box(new Rect(0, 0, TextureSize.x, TextureSize.y), ProgressBarFull);

        //    GUI.EndGroup();

        GUI.EndGroup();

        GUI.BeginGroup(new Rect(pos.x, pos.y, TextureSize.x, TextureSize.y));
        GUI.Box(new Rect(0, 0, TextureSize.x, TextureSize.y), TEST2);

        // draw the filled-in part:
        GUI.BeginGroup(new Rect(0, 0, TextureSize.x * BarDisplay, TextureSize.y));
        GUI.Box(new Rect(0, 0, TextureSize.x, TextureSize.y), TEST);

        GUI.EndGroup();

        GUI.EndGroup();

        //GUI.BeginGroup(new Rect(pos.x, pos.y, TextureSize.x + 50, TextureSize.y + 50));
        //GUI.Box(new Rect(0, 0, TextureSize.x, TextureSize.y + 50), TEST2);

        //// draw the filled-in part:
        //GUI.BeginGroup(new Rect(0, 0, TextureSize.x * BarDisplay + 50, TextureSize.y + 50));
        //GUI.Box(new Rect(0, 0, TextureSize.x + 50, TextureSize.y + 50), TEST);

        //GUI.EndGroup();

        //GUI.EndGroup();


    }



    public void DoDamage(float Damage)
    {
        if(CurrentHealth - Damage < 0)
        {
            Destroy(this);
        }
        else
        {
            CurrentHealth -= Damage;
        }
    }

    
}
