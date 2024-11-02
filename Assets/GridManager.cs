using System.Collections;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject[,] grid = new GameObject[5,5];

    public Vector2[] possiblePositions;
    public GameObject[] allCells;

    public GameObject simpleEnemy;

    public int numberOfEnemies;
    private GameObject enemy;

    private EnemyController[] allEnemies;

    private int click;
    private int cycle;

    public playerController2 player2;

    private void Awake()
    {
        int i = 0;
        foreach (var cell in allCells)
        {
            grid[(int)possiblePositions[i].x, (int)possiblePositions[i].y] = allCells[i];
            i++;
        }

        allEnemies = new EnemyController[numberOfEnemies];
        player2.enabled = true;
    }

    private void Start()
    {
        InitializeEnemies();
    }

    public void ResetCellColor()
    {
        for (int i = 0; i < allCells.Length; i++)
        {
            if(i % 2 == 0)
            {
                allCells[i].GetComponent<SpriteRenderer>().color = Color.black;
            }
            else
            {
                allCells[i].GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }

    private void InitializeEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++) 
        {
            Vector2 spawnLocation = new Vector2(Random.Range(0, 4), Random.Range(0, 4));

            if(grid[(int)spawnLocation.x, (int)spawnLocation.y].GetComponent<CellManager>().isOccupied == true)
            {
                i--;
                return;
            }

            
            GameObject enemy = Instantiate(simpleEnemy);
            EnemyController controller = enemy.GetComponent<EnemyController>();
            controller.gridManager = this.gameObject.GetComponent<GridManager>();
            controller.startingSquare = spawnLocation;
            controller.Spawn();
            allEnemies[i] = controller;
        }

        OnCycleStart();
    }

    private void OnCycleStart()
    {
        StartCoroutine(EnemyActions());
    }
    private IEnumerator EnemyActions()
    {
        for(int i = 0; i < allEnemies.Length; i++)
        {
            yield return new WaitForSeconds(0.25f);
            allEnemies[i].MovePosition();
        }

        yield return new WaitForSeconds(1f);
        //ResetCellColor();
        OnCycleStart();
    }






}
