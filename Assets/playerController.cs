using JetBrains.Annotations;
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

    public Vector2 targetPosition = new Vector2(-1,-1);

        //clear possible postitions do for loop for the resetting

    MoveDirection moveDirection;

    private DialogueManager dialogueManager;


        // will halt player movement when enemy are in motion
        // may not be needed if there is no animiations
        public bool movementLock = false;
    void Awake(){
        //Grab the current cell from manualy inputed player position

        //currentCell = gridManager.grid[(int)playerPosition.x, (int)playerPosition.y];

        dialogueManager = FindFirstObjectByType<DialogueManager>();

        gridManager = FindFirstObjectByType<GridManager>();

        player = this.gameObject;
        


        //spriteRenderer = ManagerGetComponent<SpriteRenderer>();
        //ValidMoves();

        
        //place the player into the grid
        //gridManager.grid[(int)playerPosition.x,(int)playerPosition.y] = player;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentCell = gridManager.grid[(int)playerPosition.x, (int)playerPosition.y];

        player.transform.SetParent(currentCell.transform, true);
        player.transform.localPosition = Vector2.zero; //visual update
        CheckLegalPositions();
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

    void PlayerMove()
    {
            if (Input.GetKeyUp(KeyCode.W))
            {
            moveDirection = MoveDirection.UP;
            PlaceMove();
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
            moveDirection = MoveDirection.DOWN;
            PlaceMove();
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                moveDirection = MoveDirection.LEFT;
                PlaceMove();
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                moveDirection = MoveDirection.RIGHT;
                PlaceMove();
            }
        
    }

    void RecordPositions(Vector2 laspos){
        pastPositions.Add(laspos);
    }

    void RewindPositions(){
        playerPosition = pastPositions[^1];
    }
    public void CheckLegalPositions()
    {
        if(dialogueManager.dialogueFinished == false)
        {
            return;
        }

        if(playerPosition.y - 1 >= 0) //can we go one down?
        {
            possiblePositions.Add(playerPosition - Vector2.up);
            
            gridManager.grid[(int)possiblePositions[possiblePositions.Count - 1].x, (int)possiblePositions[possiblePositions.Count - 1].y].GetComponent<CellManager>().TargetingSquare(true);
            gridManager.grid[(int)possiblePositions[possiblePositions.Count - 1].x, (int)possiblePositions[possiblePositions.Count - 1].y].GetComponent<CellManager>().activeCircle.GetComponent<SpriteRenderer>().color = Color.green;
        }
        if(playerPosition.y + 1 <= gridManager.gridHeight - 1) // can we go one up?
        {
            possiblePositions.Add(playerPosition + Vector2.up);
        
            gridManager.grid[(int)possiblePositions[possiblePositions.Count-1].x, (int)possiblePositions[possiblePositions.Count - 1].y].GetComponent<CellManager>().TargetingSquare(true);
            gridManager.grid[(int)possiblePositions[possiblePositions.Count - 1].x, (int)possiblePositions[possiblePositions.Count - 1].y].GetComponent<CellManager>().activeCircle.GetComponent<SpriteRenderer>().color = Color.green;

        }
        if (playerPosition.x + 1 <= gridManager.gridLength - 1) // can we go right?
        {
            possiblePositions.Add(playerPosition + Vector2.right);

            gridManager.grid[(int)possiblePositions[possiblePositions.Count - 1].x, (int)possiblePositions[possiblePositions.Count - 1].y].GetComponent<CellManager>().TargetingSquare(true);
            gridManager.grid[(int)possiblePositions[possiblePositions.Count - 1].x, (int)possiblePositions[possiblePositions.Count - 1].y].GetComponent<CellManager>().activeCircle.GetComponent<SpriteRenderer>().color = Color.green;
        }
        if(playerPosition.x - 1 >= 0) //can we go left?
        {
            possiblePositions.Add(playerPosition - Vector2.right);

            gridManager.grid[(int)possiblePositions[possiblePositions.Count - 1].x, (int)possiblePositions[possiblePositions.Count - 1].y].GetComponent<CellManager>().TargetingSquare(true);
            gridManager.grid[(int)possiblePositions[possiblePositions.Count - 1].x, (int)possiblePositions[possiblePositions.Count - 1].y].GetComponent<CellManager>().activeCircle.GetComponent<SpriteRenderer>().color = Color.green;

        }
    }

    private void PlaceMove()
    {
        if (dialogueManager.dialogueFinished == false)
        {
            return;
        }

        for (int i = 0; i < possiblePositions.Count; i++)
        {
            gridManager.grid[(int)possiblePositions[i].x, (int)possiblePositions[i].y].GetComponent<CellManager>().ResetColor(true);
        }

        if (moveDirection == MoveDirection.RIGHT)
        {
            gridManager.grid[(int)playerPosition.x + 1, (int)playerPosition.y].GetComponent<CellManager>().TargetingSquare(true);

            gridManager.grid[(int)playerPosition.x + 1, (int)playerPosition.y].GetComponent<CellManager>().activeCircle.GetComponent<SpriteRenderer>().color = Color.green;
            targetPosition = playerPosition;
            targetPosition += Vector2.right;
        }
        if((moveDirection == MoveDirection.LEFT))
        {
            gridManager.grid[(int)playerPosition.x - 1, (int)playerPosition.y].GetComponent<CellManager>().TargetingSquare(true);

            gridManager.grid[(int)playerPosition.x - 1, (int)playerPosition.y].GetComponent<CellManager>().activeCircle.GetComponent<SpriteRenderer>().color = Color.green;
            targetPosition = playerPosition;
            targetPosition -= Vector2.right;
        }
        if (moveDirection == MoveDirection.UP)
        {
            gridManager.grid[(int)playerPosition.x, (int)playerPosition.y + 1].GetComponent<CellManager>().TargetingSquare(true);
            gridManager.grid[(int)playerPosition.x, (int)playerPosition.y + 1].GetComponent<CellManager>().activeCircle.GetComponent<SpriteRenderer>().color = Color.green;
            targetPosition = playerPosition;
            targetPosition += Vector2.up;
        }
        if (moveDirection == MoveDirection.DOWN)
        {
            gridManager.grid[(int)playerPosition.x, (int)playerPosition.y - 1].GetComponent<CellManager>().TargetingSquare(true);
            gridManager.grid[(int)playerPosition.x, (int)playerPosition.y - 1].GetComponent<CellManager>().activeCircle.GetComponent<SpriteRenderer>().color = Color.green;
            targetPosition = playerPosition;
            targetPosition += Vector2.down;
        }
    }

    public void SubmitMove()
    {
        if (targetPosition == new Vector2(-1, -1))
        {

        }
        else 
        {
            ClearPositions();
            currentCell = gridManager.grid[(int)targetPosition.x, (int)targetPosition.y];
            player.transform.SetParent(currentCell.transform, true);
            player.transform.localPosition = Vector2.zero;
            playerPosition = targetPosition;
            targetPosition = new Vector2(-1, -1);
            CheckLegalPositions();
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
            gridManager.grid[(int)possiblePositions[i].x, (int)possiblePositions[i].y].GetComponent<CellManager>().ResetColor(true);
        }
        possiblePositions.Clear();
        
    }
}
