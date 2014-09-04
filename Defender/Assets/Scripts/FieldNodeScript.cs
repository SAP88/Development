using UnityEngine;
using System.Collections;

public class FieldNodeScript : MonoBehaviour 
{
    

    void Update()
    {
        if (LevelController.SelectedDefender != null && this.IsAnyTouch())
        {
            var gObject = (GameObject)GameObject.Instantiate(LevelController.SelectedDefender);
            gObject.transform.position = this.transform.position;
            LevelController.SelectedDefender = null;
        }
    }


    
	// Update is called once per frame
	//void Update () 
    //{
    //    if(this.IsAnyTouch())
    //    {
    //        Debug.Log("Pressed");
    //    }
	//}
}
