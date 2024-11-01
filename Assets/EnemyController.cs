using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Vector2 startingSquare; //we manually input the starting square for the enemy

    public GridManager gridManager;

    private List<Vector2> possiblePositions = new List<Vector2>(); // this is a dynamic list of the up to 4 possible positions that can be moved too
    private GameObject[] legalCells; // not used for now, maybe will use it for ruling out mountains or other status

    private Vector2 currentPosition;
    private GameObject currentCell;

    //not used now, maybe will be used later?
    bool canUp;
    bool canDown;
    bool canRight;
    bool canLeft;

    int randomDir;

    private void Start()
    {
        //initializes the enemy

        currentPosition = startingSquare;
        currentCell = gridManager.grid[(int)currentPosition.x, (int)currentPosition.y];
        this.transform.SetParent(currentCell.transform, true);
        this.transform.localPosition = Vector2.zero; 

        CheckLegalPositions();
    }


    private void CheckLegalPositions()
    {
        gridManager.ResetCellColor();

        canUp = false;
        canDown = false;
        canRight = false;
        canLeft = false;

        if(currentPosition.y - 1 >= 0) //can we go one down?
        {
            possiblePositions.Add(currentPosition - Vector2.up);
            canDown = true;
            gridManager.grid[(int)possiblePositions[possiblePositions.Count - 1].x, (int)possiblePositions[possiblePositions.Count - 1].y].GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        if(currentPosition.y + 1 <= 4) // can we go one up?
        {
            possiblePositions.Add(currentPosition + Vector2.up);
            canUp = true;
            gridManager.grid[(int)possiblePositions[possiblePositions.Count-1].x, (int)possiblePositions[possiblePositions.Count - 1].y].GetComponent<SpriteRenderer>().color = Color.yellow;
            
        }
        if (currentPosition.x + 1 <= 4) // can we go right?
        {
            possiblePositions.Add(currentPosition + Vector2.right);
            canRight = true;
            gridManager.grid[(int)possiblePositions[possiblePositions.Count - 1].x, (int)possiblePositions[possiblePositions.Count - 1].y].GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        if(currentPosition.x - 1 >= 0) //can we go left?
        {
            possiblePositions.Add(currentPosition - Vector2.right);
            canLeft = true;
            gridManager.grid[(int)possiblePositions[possiblePositions.Count - 1].x, (int)possiblePositions[possiblePositions.Count - 1].y].GetComponent<SpriteRenderer>().color = Color.yellow;
            
        }

        randomDir = Random.Range(0, possiblePositions.Count);
        Debug.Log(randomDir.ToString() + "direction");

        StartCoroutine(WaitForMove());
    }

    private void MovePosition(Vector2 pos)
    {
        currentPosition = pos;
        currentCell = gridManager.grid[(int)currentPosition.x, (int)currentPosition.y];
        this.transform.SetParent(currentCell.transform, true);
        this.transform.localPosition = Vector2.zero;
        possiblePositions.Clear();


        CheckLegalPositions();
    }

    IEnumerator WaitForMove()
    {
        
        yield return new WaitForSeconds(2f);
        MovePosition(possiblePositions[randomDir]);

    }




}
