using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class playerController : MonoBehaviour
{
        public GameObject player;
        public Vector2 playerPosition;
        public GridManager gridManager;

        //tracks the current cell the player is occupying
        public GameObject currentCell;
    

        // will halt player movement when enemy are in motion
        // may not be needed if there is no animiations
        public bool movementLock = false;
    void Awake(){
        //Grab the current cell from manualy inputed player position

        currentCell = gridManager.grid[(int)playerPosition.x, (int)playerPosition.y];
        player.transform.SetParent(currentCell.transform, true);
        player.transform.localPosition = Vector2.zero; //visual update
        //ValidMoves();

        
        //place the player into the grid
        //gridManager.grid[(int)playerPosition.x,(int)playerPosition.y] = player;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        //ValidMoves();
    }

    void PlayerMove(){
        //Debug.Log($"{playerPosition.x} {playerPosition.y}");
        //TODO needs to check for collision
        if (player.CompareTag("Player1") && !movementLock){
            if (Input.GetKeyUp(KeyCode.W)){
                currentCell = gridManager.grid[(int)playerPosition.x,(int)playerPosition.y+1];
                player.transform.SetParent(currentCell.transform, true);
                player.transform.localPosition = Vector2.zero;
                playerPosition.y += 1;
                //ValidMoves();
            }
            if (Input.GetKeyUp(KeyCode.S)){
                currentCell = gridManager.grid[(int)playerPosition.x,(int)playerPosition.y-1];
                player.transform.SetParent(currentCell.transform, true);
                player.transform.localPosition = Vector2.zero;
                playerPosition.y -= 1;
                //ValidMoves();
            }
            if (Input.GetKeyUp(KeyCode.A)){
                currentCell = gridManager.grid[(int)playerPosition.x-1,(int)playerPosition.y];
                player.transform.SetParent(currentCell.transform, true);
                player.transform.localPosition = Vector2.zero;
                playerPosition.x -= 1;
                //ValidMoves();
            }
            if (Input.GetKeyUp(KeyCode.D)){
                currentCell = gridManager.grid[(int)playerPosition.x+1,(int)playerPosition.y];
                player.transform.SetParent(currentCell.transform, true);
                player.transform.localPosition = Vector2.zero;
                playerPosition.x += 1;
                //ValidMoves();
            }
        }
        if (player.CompareTag("Player2") && !movementLock){
            if (Input.GetKeyUp(KeyCode.UpArrow)){
                currentCell = gridManager.grid[(int)playerPosition.x,(int)playerPosition.y+1];
                player.transform.SetParent(currentCell.transform, true);
                player.transform.localPosition = Vector2.zero;
                playerPosition.y += 1;
            }
            if (Input.GetKeyUp(KeyCode.DownArrow)){
                currentCell = gridManager.grid[(int)playerPosition.x,(int)playerPosition.y-1];
                player.transform.SetParent(currentCell.transform, true);
                player.transform.localPosition = Vector2.zero;
                playerPosition.y -= 1;
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow)){
                currentCell = gridManager.grid[(int)playerPosition.x-1,(int)playerPosition.y];
                player.transform.SetParent(currentCell.transform, true);
                player.transform.localPosition = Vector2.zero;
                playerPosition.x -= 1;
            }
            if (Input.GetKeyUp(KeyCode.RightArrow)){
                currentCell = gridManager.grid[(int)playerPosition.x+1,(int)playerPosition.y];
                player.transform.SetParent(currentCell.transform, true);
                player.transform.localPosition = Vector2.zero;
                playerPosition.x += 1;
            }
        }
        
    }

    public enum MoveDirection
    {
        UP, DOWN, LEFT, RIGHT
    }

    void ValidMoves(){
        
        gridManager.grid[(int)playerPosition.x,(int)playerPosition.y+1].GetComponent<SpriteRenderer>().color = Color.yellow;    
        gridManager.grid[(int)playerPosition.x,(int)playerPosition.y-1].GetComponent<SpriteRenderer>().color = Color.yellow;
        gridManager.grid[(int)playerPosition.x-1,(int)playerPosition.y].GetComponent<SpriteRenderer>().color = Color.yellow;
        gridManager.grid[(int)playerPosition.x+1,(int)playerPosition.y].GetComponent<SpriteRenderer>().color = Color.yellow;

    }

}
