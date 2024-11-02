using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System.Collections.Generic;
using Unity.Collections;

public class playerController : MonoBehaviour
{
        public GameObject player;
        public Vector2 playerPosition;
        public GridManager gridManager;

        //tracks the current cell the player is occupying
        public GameObject currentCell;

        public CellManager cellManager;

        public List<Vector2> possiblePositions;
        private SpriteRenderer spriteRenderer;

        public List<Vector2> pastPositions;

        //clear possible postitions do for loop for the resetting


        // will halt player movement when enemy are in motion
        // may not be needed if there is no animiations
        public bool movementLock = false;
    void Awake(){
        //Grab the current cell from manualy inputed player position

        //currentCell = gridManager.grid[(int)playerPosition.x, (int)playerPosition.y];
    
        player.transform.SetParent(currentCell.transform, true);
        player.transform.localPosition = Vector2.zero; //visual update
        CheckLegalPositions();


        //spriteRenderer = ManagerGetComponent<SpriteRenderer>();
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

        if (Input.GetKeyUp(KeyCode.LeftShift)){
            //Rewind();
        }

    }

    void PlayerMove(){
        //Debug.Log($"{playerPosition.x} {playerPosition.y}");
        //TODO needs to check for collision
        //if (player.CompareTag("Player1")){
            if (Input.GetKeyUp(KeyCode.W)){
                ClearPositions();
                currentCell = gridManager.grid[(int)playerPosition.x,(int)playerPosition.y+1];
                player.transform.SetParent(currentCell.transform, true);
                player.transform.localPosition = Vector2.zero;
                playerPosition.y += 1;
                CheckLegalPositions();
            }
            if (Input.GetKeyUp(KeyCode.S)){
                ClearPositions();
                currentCell = gridManager.grid[(int)playerPosition.x,(int)playerPosition.y-1];
                player.transform.SetParent(currentCell.transform, true);
                player.transform.localPosition = Vector2.zero;
                playerPosition.y -= 1;
                CheckLegalPositions();
            }
            if (Input.GetKeyUp(KeyCode.A)){
                ClearPositions();
                currentCell = gridManager.grid[(int)playerPosition.x-1,(int)playerPosition.y];
                player.transform.SetParent(currentCell.transform, true);
                player.transform.localPosition = Vector2.zero;
                playerPosition.x -= 1;
                CheckLegalPositions();
            }
            if (Input.GetKeyUp(KeyCode.D)){
                ClearPositions();
                currentCell = gridManager.grid[(int)playerPosition.x+1,(int)playerPosition.y];
                player.transform.SetParent(currentCell.transform, true);
                player.transform.localPosition = Vector2.zero;
                playerPosition.x += 1;
                CheckLegalPositions();
            }
        //}
        if (player.CompareTag("Player2") && !movementLock){
            //currentCell = gridManager.grid[(int)playerPosition.x,(int)playerPosition.y];
            if (Input.GetKeyUp(KeyCode.UpArrow)){
                ClearPositions();
                currentCell = gridManager.grid[(int)playerPosition.x,(int)playerPosition.y+1];
                player.transform.SetParent(currentCell.transform, true);
                player.transform.localPosition = Vector2.zero;
                playerPosition.y += 1;
                CheckLegalPositions();
            }
            if (Input.GetKeyUp(KeyCode.DownArrow)){
                currentCell = gridManager.grid[(int)playerPosition.x,(int)playerPosition.y-1];
                player.transform.SetParent(currentCell.transform, true);
                player.transform.localPosition = Vector2.zero;
                playerPosition.y -= 1;
                CheckLegalPositions();
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow)){
                ClearPositions();
                currentCell = gridManager.grid[(int)playerPosition.x-1,(int)playerPosition.y];
                player.transform.SetParent(currentCell.transform, true);
                player.transform.localPosition = Vector2.zero;
                playerPosition.x -= 1;
                CheckLegalPositions();
            }
            if (Input.GetKeyUp(KeyCode.RightArrow)){
                ClearPositions();
                currentCell = gridManager.grid[(int)playerPosition.x+1,(int)playerPosition.y];
                player.transform.SetParent(currentCell.transform, true);
                player.transform.localPosition = Vector2.zero;
                playerPosition.x += 1;
                CheckLegalPositions();
            }
        }
        
    }

    void RecordPositions(Vector2 laspos){
        pastPositions.Add(laspos);
    }

    void RewindPositions(){
        playerPosition = pastPositions[^1];
    }
    private void CheckLegalPositions()
    {

        if(playerPosition.y - 1 >= 0) //can we go one down?
        {
            possiblePositions.Add(playerPosition - Vector2.up);
            
            gridManager.grid[(int)possiblePositions[possiblePositions.Count - 1].x, (int)possiblePositions[possiblePositions.Count - 1].y].GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        if(playerPosition.y + 1 <= 4) // can we go one up?
        {
            possiblePositions.Add(playerPosition + Vector2.up);
        
            gridManager.grid[(int)possiblePositions[possiblePositions.Count-1].x, (int)possiblePositions[possiblePositions.Count - 1].y].GetComponent<SpriteRenderer>().color = Color.yellow;
            
        }
        if (playerPosition.x + 1 <= 4) // can we go right?
        {
            possiblePositions.Add(playerPosition + Vector2.right);

            gridManager.grid[(int)possiblePositions[possiblePositions.Count - 1].x, (int)possiblePositions[possiblePositions.Count - 1].y].GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        if(playerPosition.x - 1 >= 0) //can we go left?
        {
            possiblePositions.Add(playerPosition - Vector2.right);

            gridManager.grid[(int)possiblePositions[possiblePositions.Count - 1].x, (int)possiblePositions[possiblePositions.Count - 1].y].GetComponent<SpriteRenderer>().color = Color.yellow;
            
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


    void  ClearPositions(){
        for (int i = 0; i<possiblePositions.Count; i++)
        {
            currentCell = gridManager.grid[(int)possiblePositions[i].x,(int)possiblePositions[i].y];
            //currentCell.GetComponent<CellManager>().ResetColor();
            gridManager.grid[(int)possiblePositions[i].x, (int)possiblePositions[i].y].GetComponent<CellManager>().ResetColor();
        }
        possiblePositions.Clear();
        
    }
}
