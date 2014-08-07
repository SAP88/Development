using UnityEngine;
using System.Collections;


public class MonsterHealth : MonoBehaviour 
{
    public float CurrentHealth;
    public float MaxHealth;

    public Texture2D FullHp;
    public Texture2D EmptyHp;

    void Start()
    {
        //var wantedPos = Camera.main.WorldToScreenPoint(this.transform.position);
        //transform.position = wantedPos;
        
    }

    private float BarDisplay
    {
        get
        {
            return CurrentHealth / MaxHealth;
        }
    }

    private Texture2D _progressBarEmpty = null;
    private Texture2D ProgressBarEmpty
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
    private Texture2D ProgressBarFull
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

    /// <summary>
    /// Ожидается что ширина полоски с жизнями равна высоте монстра
    /// </summary>
    private Vector2? _size = null;
    private Vector2 TextureSize
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
        var pos = Camera.main.WorldToScreenPoint(this.transform.position);
        pos.y = Screen.height - pos.y; //pos в центре монстра

        //pos.y = pos.y - this.renderer.bounds.size.y / 2 - TextureSize.y;
        //pos.x = pos.x - this.renderer.bounds.size.x / 2;

        pos.y = pos.y - TextureSize.x / 2 - TextureSize.y;
        pos.x = pos.x - TextureSize.x / 2;

        GUI.BeginGroup(new Rect(pos.x, pos.y, TextureSize.x, TextureSize.y));
            GUI.DrawTexture(new Rect(0, 0, TextureSize.x * BarDisplay, TextureSize.y), FullHp);
            GUI.DrawTexture(new Rect(0, 0, TextureSize.x, TextureSize.y), EmptyHp);
        GUI.EndGroup();

    }



    public void DoDamage(float Damage)
    {
        if(CurrentHealth - Damage < 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            CurrentHealth -= Damage;
        }
    }

    
}
