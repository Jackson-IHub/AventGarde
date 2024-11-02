using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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

    public int click;

    private int clicksPerCycle;
    private int cycle;

    bool isSetUp = false;

    private void Awake()
    {
        int i = 0;
        foreach (var cell in allCells)
        {
            grid[(int)possiblePositions[i].x, (int)possiblePositions[i].y] = allCells[i];
            i++;
        }

        allEnemies = new EnemyController[numberOfEnemies];

        clicksPerCycle = numberOfEnemies;
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

            allCells[i].GetComponent<CellManager>().isTargeted = false;
        }
        
    }

    private void InitializeEnemies()
    {

        

        //List<Vector2> possibleSpawns = new List<Vector2>();

        //while (i < grid.Length)
        //{
        //    for (int k = 0; k < 5; k++)
        //    {
        //        if (grid[i, k].GetComponent<CellManager>().isOccupied || grid[i, k].GetComponent<CellManager>().isTargeted)
        //        {
        //            // no nothing is supposed to happen here
        //        }
        //        else
        //        {
        //            possibleSpawns.Add(new Vector2(i, k));
        //        }
                
        //    }
        //    i = 0;

        //}


        int i = 0;

        while (i < numberOfEnemies) 
        {
            
            Vector2 spawnLocation = new Vector2(Random.Range(0, 4), Random.Range(0, 4));

            //int randomNumber = Random.Range(0, (int)possibleSpawns.Count());
            //Vector2 startingSpawn = possibleSpawns[randomNumber];

            if((grid[(int)spawnLocation.x, (int)spawnLocation.y].GetComponent<CellManager>().isOccupied) || (grid[(int)spawnLocation.x, (int)spawnLocation.y].GetComponent<CellManager>().isTargeted))
            {
                spawnLocation = new Vector2(Random.Range(0, 4), Random.Range(0, 4));
                
            }
            else
            {
                GameObject enemy = Instantiate(simpleEnemy);
                EnemyController controller = enemy.GetComponent<EnemyController>();
                controller.gridManager = this.gameObject.GetComponent<GridManager>();
                controller.startingSquare = spawnLocation;
                controller.Spawn();
                allEnemies[i] = controller;
                i++;
            }
        }

        
    }

    public void RemoveEnemy(GameObject gameObject)
    {
        for (int i = 0; i < allEnemies.Count(); i++){
            if (allEnemies[i] == gameObject){
                allEnemies[i] = null;
            }
        }
    }

    private void OnCycleStart()
    {
        StartCoroutine(EnemyActions());
    }
    private IEnumerator EnemyActions()
    {
        if (isSetUp){//move
            for(int i = 0; i < allEnemies.Length; i++)
            {
                yield return new WaitForSeconds(0.25f);
                if (!allEnemies[i] == null){
                    allEnemies[i].MovePosition();
                } 
                //ClickUpdate();
                // reset
                // calculate move
                
            }
        }else{//calculate
            for(int i = 0; i < allEnemies.Length; i++){
                allEnemies[i].CheckLegalPositions();
            }
        }
        isSetUp = !isSetUp;
        //ResetCellColor();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            OnCycleStart();
        }

        //ResetCellColor();

    }

    public void ClickUpdate(){
        
        click = click+1;

        if (click == clicksPerCycle)
        {
            CycleUpdate();
            click = 0;
        }

    }

    //public void ResetTargeting()

    public void CycleUpdate()
    {
        cycle++;
        ResetCellColor();
    }




}
