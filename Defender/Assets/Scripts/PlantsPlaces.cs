using UnityEngine;
using System.Collections;
using UnityEditor;
using System;


[ExecuteInEditMode]
public class PlantsPlaces : MonoBehaviour {

    public GameObject PlantsPlace;
    public int CountWidth;
    public int CountHeight;
    public float LeftTopOffset;

	// Use this for initialization
	void Start () 
    {
        if(PlantsPlace == null)
        {
            Debug.LogWarning("PlantsPlace is null");
            return;
        }

        if(PlantsPlace.tag != "FieldObject")
        {
            Debug.LogWarning("PlantsPlace tag is not FieldObject");
            return;
        }

        var fieldObjects = GameObject.FindGameObjectsWithTag("FieldObject");
        if (fieldObjects != null)
        {
            foreach (var fo in fieldObjects)
            {
                GameObject.DestroyImmediate(fo);
            }
        }

        
        SpriteRenderer gameFieldRender = GetComponent<SpriteRenderer>();
        BoxCollider2D prefabBoxCollider = PlantsPlace.GetComponent<BoxCollider2D>();

        Vector2 pSize = prefabBoxCollider.transform.localScale;
        
        var leftTopX = transform.position.x - gameFieldRender.bounds.size.x / 2 + LeftTopOffset + pSize.x / 2;
        var leftTopY = transform.position.y + gameFieldRender.bounds.size.y / 2 - pSize.y / 2;

        for (int i = 0; i < CountHeight; i++)
        {
            for (int j = 0; j < CountWidth; j++)
            {
                GameObject prefab = GameObject.Instantiate(PlantsPlace) as GameObject;
                prefab.name = string.Format("[{0},{1}] FieldObject", j, i);
                prefab.transform.parent = this.transform; // Selection.activeTransform ?? this.transform;
                prefab.transform.position = new Vector2(leftTopX + pSize.x * j, leftTopY - pSize.y * i);
            }

            //Рисуем линии по которым будут монстры ходить
            //var gLine = new GameObject(string.Format("EnemyRoad_{0}", i + 1)) { tag = "FieldObject" };
            //var boxColl = gLine.AddComponent<BoxCollider2D>();
            //boxColl.transform.position = new Vector3(transform.position.x, leftTopY - pSize.y  * i - (pSize.y / 2), -4);
            //boxColl.size = new Vector2(gameFieldRender.bounds.size.x, 0.2f);
        }

	}

}
