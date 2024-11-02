using UnityEngine;

public class CellManager : MonoBehaviour
{
    public bool isOccupied = false; //is there a character or blocker?

    public bool isTargeted = false;

    public GridManager gridManager;
    public Color startingColor;

    private SpriteRenderer spriteRenderer;

    public  EnemyController enemyInCell;

    // called when an entity enters the grid square
    public void CheckForChildren(){
        Transform test;
        test = this.transform;
        if (test.childCount != 0)
        {
            gridManager.RemoveEnemy(test.GetComponentInChildren<EnemyController>().gameObject);
            Destroy(test.GetComponentInChildren<EnemyController>().gameObject);
        }

    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ResetColor()
    {
        spriteRenderer.color = startingColor;
    }

    public void ResetStatus()
    {
        isTargeted = false;
    }







}
