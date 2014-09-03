using UnityEngine;
using System.Collections;
using UnityEditor;

[ExecuteInEditMode]
public class PlantsPlaces : MonoBehaviour {

    [MenuItem("TEST")]
    public void Test()
    {

    }
    
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

        //Vector2 pSize = prefabBoxCollider.size;
        Vector2 pSize = prefabBoxCollider.transform.localScale;
        
        
        var leftTopX = transform.position.x - gameFieldRender.bounds.size.x / 2 + LeftTopOffset + pSize.x / 2;
        var leftTopY = transform.position.y + gameFieldRender.bounds.size.y / 2 - pSize.y / 2;

        //(GameObject.Instantiate(PlantsPlace) as GameObject).transform.position = new Vector2(
        //    transform.position.x - gameFieldRender.bounds.size.x / 2,
        //    transform.position.y + gameFieldRender.bounds.size.y / 2);
        
        //if (PlantsPlace.renderer != null)
        //{
        //    pSize = PlantsPlace.renderer.bounds.size;
        //}
        //else
        //{
            
        //}
        for(int i = 0; i < CountHeight; i++)
        for(int j = 0; j < CountWidth; j++)
        {
            GameObject prefab = GameObject.Instantiate(PlantsPlace) as GameObject;
            prefab.name = string.Format("[{0},{1}] FieldObject", j, i);
            prefab.transform.parent = this.transform; // Selection.activeTransform ?? this.transform;
            prefab.transform.position = new Vector3(leftTopX + pSize.x * j, leftTopY - pSize.y * i, -5);
        }

	}

    //void FixedUpdate()
    //{
    //}
    //
	//// Update is called once per frame
	//void Update () {
	//    
	//}
}
