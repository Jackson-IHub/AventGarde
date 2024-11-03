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
    private bool pointedUp = true;

    public bool randomSwapping = false;

    private void Awake()
    {
        gridManager = GetComponent<GridManager>();
    }

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        currentPosition = startingSquare;
        gridManager = FindAnyObjectByType<GridManager>();
        Debug.Log("starting square " + startingSquare);
        currentCell = gridManager.grid[(int)currentPosition.x, (int)currentPosition.y];
        this.transform.SetParent(currentCell.transform, true);
        this.transform.localPosition = Vector2.zero;
        gridManager.AddEnemy(this.gameObject.GetComponent<EnemyController>());
        currentCell.GetComponent<CellManager>().isOccupied = true;
    }

    public void EstablishTarget()
    {
        if(randomSwapping)
        {
            FlipEnemy();
        }

        if (isHorizontal)
        {
            if (pointedRight)
            {
                if (currentPosition.x + Vector2.right.x <= gridManager.gridLength - 1)
                {
                    targetPosition = currentPosition + Vector2.right;
                    Debug.Log(targetPosition);
                }
                else
                {
                    targetPosition = currentPosition - Vector2.right;
                    pointedRight = false;
                }
            }
            else
            {
                if (currentPosition.x - Vector2.right.x < 0)
                {
                    targetPosition = currentPosition + Vector2.right;
                    pointedRight = true;
                }
                else
                {
                    targetPosition = currentPosition - Vector2.right;
                    
                }
            }

        }
        else //must be going vertically
        {
            if (pointedUp)
            {
                if (currentPosition.y + Vector2.up.y <= gridManager.gridHeight - 1)
                {
                    targetPosition = currentPosition + Vector2.up;
                    Debug.Log(targetPosition);
                }
                else
                {
                    targetPosition = currentPosition - Vector2.up;
                    pointedUp = false;
                }
            }
            else
            {
                if (currentPosition.y - Vector2.up.y < 0)
                {
                    targetPosition = currentPosition + Vector2.up;
                    pointedUp = true;
                }
                else
                {
                    targetPosition = currentPosition - Vector2.up;

                }
            }
        }

        gridManager.grid[(int)targetPosition.x, (int)targetPosition.y].GetComponent<CellManager>().TargetingSquare(false);
        gridManager.grid[(int)targetPosition.x, (int)targetPosition.y].GetComponent<CellManager>().enemyCircle.GetComponent<SpriteRenderer>().color = Color.red;
    }

    private void FlipEnemy()
    {
        if(Random.Range(0,1) == 0)
        {
            isHorizontal = !isHorizontal;
        }
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
        currentCell.GetComponent<CellManager>().ResetColor(false);
        currentCell.GetComponent<CellManager>().ResetStatus();
        currentCell.GetComponent<CellManager>().isOccupied = true;
    }




}
