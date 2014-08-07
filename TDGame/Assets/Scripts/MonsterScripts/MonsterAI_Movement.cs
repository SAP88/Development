using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class MonsterAI_Movement : MonoBehaviour 
{
    Vector3 NextPosition;
    bool IsLastMove = false;
    int CurrentPosition = 1;

	// Use this for initialization
	void Start () 
    {
        NextPosition = GetNextPosition(GameLevelController.Instance.ShortestPath[CurrentPosition]);
        rigidbody2D.gravityScale = 0;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (IsLastMove) return;
        var difX = Mathf.Abs(NextPosition.x - this.transform.position.x);
        var difY = Mathf.Abs(NextPosition.y - this.transform.position.y);
        if (difX < 0.01f && difY < 0.01f)
        {
            CurrentPosition++;
            if (GameLevelController.Instance.ShortestPath.Count == CurrentPosition)
            {
                IsLastMove = true;
            }
            else
            {
                NextPosition = GetNextPosition(GameLevelController.Instance.ShortestPath[CurrentPosition]);
            }
        }
        else
        {
            this.transform.position += (NextPosition - this.transform.position) * 3 * Time.deltaTime;
        }
	}

    private Vector2 GetNextPosition(Vector2 nextCoord)
    {
        return new Vector2(GameLevelController.Instance.GameFieldPosition.position.x + this.renderer.bounds.size.x * nextCoord.x, 
            GameLevelController.Instance.GameFieldPosition.position.y - this.renderer.bounds.size.y * nextCoord.y);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Exit")
        {
            Debug.Log("HAHA I AM OUT!");
            Destroy(this.gameObject);
        }
    }
}
