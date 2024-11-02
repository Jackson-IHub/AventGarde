using JetBrains.Annotations;
using UnityEngine;

public class playerController : MonoBehaviour
{
        public GameObject player;
        public Vector2 playerPosition;
        public GridManager gridManager;

        // will halt player movement when enemy are in motion
        // may not be needed if there is no animiations
        public bool movementLock = false;
    void Awake(){
        
        //place the player into the grid
        gridManager.grid[(int)playerPosition.x,(int)playerPosition.y] = player;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    void PlayerMove(){
        //Debug.Log($"{playerPosition.x} {playerPosition.y}");
        //TODO needs to check for collision
        if (player.CompareTag("Player1") && !movementLock){
            if (Input.GetKeyUp(KeyCode.W)){
                // Get the position of the grid in targeted direction and set players position to that position
                player.transform.position = gridManager.grid[(int)playerPosition.x,(int)playerPosition.y+1].transform.position;
                playerPosition.y += 1;
            }
            if (Input.GetKeyUp(KeyCode.S)){
                player.transform.position = gridManager.grid[(int)playerPosition.x,(int)playerPosition.y-1].transform.position;
                playerPosition.y -= 1;
            }
            if (Input.GetKeyUp(KeyCode.A)){
                player.transform.position = gridManager.grid[(int)playerPosition.x-1,(int)playerPosition.y].transform.position;
                playerPosition.x -= 1;
            }
            if (Input.GetKeyUp(KeyCode.D)){
                player.transform.position = gridManager.grid[(int)playerPosition.x+1,(int)playerPosition.y].transform.position;
                playerPosition.x += 1;
            }
        }
        if (player.CompareTag("Player2") && !movementLock){
            if (Input.GetKeyUp(KeyCode.UpArrow)){
                // Get the position of the grid in targeted direction and set players position to that position
                player.transform.position = gridManager.grid[(int)playerPosition.x,(int)playerPosition.y+1].transform.position;
                playerPosition.y += 1;
            }
            if (Input.GetKeyUp(KeyCode.DownArrow)){
                player.transform.position = gridManager.grid[(int)playerPosition.x,(int)playerPosition.y-1].transform.position;
                playerPosition.y -= 1;
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow)){
                player.transform.position = gridManager.grid[(int)playerPosition.x-1,(int)playerPosition.y].transform.position;
                playerPosition.x -= 1;
            }
            if (Input.GetKeyUp(KeyCode.RightArrow)){
                player.transform.position = gridManager.grid[(int)playerPosition.x+1,(int)playerPosition.y].transform.position;
                playerPosition.x += 1;
            }
        }
    }
}
