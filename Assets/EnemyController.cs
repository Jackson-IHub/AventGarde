using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Vector2 startingSquare; //we manually input the starting square for the enemy

    public GridManager gridManager;

    private Vector2 possibleMovement;

    private List<Vector2> possiblePositions = new List<Vector2>(); // this is a dynamic list of the up to 4 possible positions that can be moved too
    private GameObject[] legalCells; // not used for now, maybe will use it for ruling out mountains or other status

    private Vector2 currentPosition;
    private GameObject currentCell;
    public Vector2 targetPosition;

    public bool isHorizontal = true;
    private bool pointedRight = true;

    public void Spawn()
    {
        currentPosition = startingSquare;
        Debug.Log("starting square " + startingSquare);
        currentCell = gridManager.grid[(int)currentPosition.x, (int)currentPosition.y];
        this.transform.SetParent(currentCell.transform, true);
        this.transform.localPosition = Vector2.zero;
        currentCell.GetComponent<CellManager>().isOccupied = true;
    }

    public void EstablishTarget()
    {
        if (isHorizontal)
        {
            if (pointedRight)
            {
                if (currentPosition.x + Vector2.right.x <= 4)
                {
                    Debug.Log("less than 4");
                    targetPosition = currentPosition + Vector2.right;
                    Debug.Log(targetPosition);
                }
                else
                {
                    Debug.Log("equal to 4");

                    targetPosition = currentPosition - Vector2.right;
                    pointedRight = false;
                }
            }
            else
            {
                if (currentPosition.x - Vector2.right.x < 0)
                {
                    Debug.Log("too far left");
                    targetPosition = currentPosition + Vector2.right;
                    pointedRight = true;
                }
                else
                {

                    Debug.Log("not far left enough");
                    targetPosition = currentPosition - Vector2.right;
                    
                }
            }

        }

        gridManager.grid[(int)targetPosition.x, (int)targetPosition.y].GetComponent<SpriteRenderer>().color = Color.red;


    }



    public void MovePosition()
    {
        currentCell.GetComponent<CellManager>().isOccupied = false;
        currentCell.GetComponent<CellManager>().isTargeted = false;

        currentPosition = targetPosition;
        currentCell = gridManager.grid[(int)currentPosition.x, (int)currentPosition.y];
        this.transform.SetParent(currentCell.transform, true);
        this.transform.localPosition = Vector2.zero;
        possiblePositions.Clear();
        currentCell.GetComponent<CellManager>().ResetColor();
        currentCell.GetComponent<CellManager>().isOccupied = true;
    }




}
