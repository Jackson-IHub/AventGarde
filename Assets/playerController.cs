using JetBrains.Annotations;
using UnityEngine;

public class playerController : MonoBehaviour
{
        public GameObject player;
        public Vector2 playerPosition;
        public GridManager gridManager;
    void Awake(){
        
        //place the player into the grid
        gridManager.grid[(int)playerPosition.x,(int)playerPosition.y] = player;
        //player.transform.position = new Vector3(5,5);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.W)){
            
        }
        if (Input.GetKey(KeyCode.S)){
            player.transform.position = gridManager.grid[(int)playerPosition.x,(int)playerPosition.y+1].transform.position;
        }
        if (Input.GetKey(KeyCode.A)){

        }
        if (Input.GetKey(KeyCode.D)){

        }
    }
}
