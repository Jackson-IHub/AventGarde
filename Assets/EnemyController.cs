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
    public bool isZigZag = false;
    private bool pointedRight = true;
    private bool pointedUp = true;


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
        if (!isZigZag){
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

            }else{
                if (pointedUp)
                {
                    if (currentPosition.y + Vector2.up.y <= 4)
                    {
                        Debug.Log("less than 4");
                        targetPosition = currentPosition + Vector2.up;
                        Debug.Log(targetPosition);
                    }
                    else
                    {
                        Debug.Log("equal to 4");

                        targetPosition = currentPosition - Vector2.up;
                        pointedUp = false;
                    }
                }
                else
                {
                    if (currentPosition.y - Vector2.up.y < 0)
                    {
                        Debug.Log("too far down");
                        targetPosition = currentPosition + Vector2.up;
                        pointedUp = true;
                    }
                    else
                    {

                        Debug.Log("not far down enough");
                        targetPosition = currentPosition - Vector2.up;
                        
                    }
                }
            }
        }
        else{
            //do ZIG ZAG!
            if (isHorizontal)
            {
                //Checks to see if we can go up after we hit left/right wall
                //if (goingUp){
                //issue
                    if (currentPosition.y + Vector2.up.y <= 4){
                        //zagUp = true;
                        pointedUp = true;
                    }else{
                        //zagUp = false;
                        pointedUp = false;
                    }
                //}

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
                        pointedRight = false;
                        if (pointedUp){
                            Debug.Log("Next Move Up");
                            targetPosition = currentPosition + Vector2.up;
                            Debug.Log(targetPosition);
                        }
                        else
                        {
                            Debug.Log("Next Move Down");
                            targetPosition = currentPosition + Vector2.up;
                            Debug.Log(targetPosition);
 
                        }

                        //targetPosition = currentPosition - Vector2.right;
                        //now we need to force next move to be up or down 1
                    }
                }
                else
                {
                    if (currentPosition.x - Vector2.right.x < 0)
                    {
                        //Need to move up or down since we are too far left
                        pointedRight = true;
                        if (pointedUp){
                            Debug.Log("Next Move Up");
                            targetPosition = currentPosition + Vector2.up;
                            Debug.Log(targetPosition);
                        }
                        else
                        {
                            Debug.Log("Next Move Down");
                            targetPosition = currentPosition + Vector2.up;
                            Debug.Log(targetPosition);
 
                        }

                        // Debug.Log("too far left");
                        // targetPosition = currentPosition + Vector2.right;


                    }
                    else
                    {

                        Debug.Log("not far left enough");
                        targetPosition = currentPosition - Vector2.right;
                        
                    }
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
        currentCell.GetComponent<CellManager>().ResetStatus();
        currentCell.GetComponent<CellManager>().isOccupied = true;
    }




}
