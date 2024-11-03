using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GridManager : MonoBehaviour
{
    public GameObject[,] grid = new GameObject[5,5];

    public Vector2[] possiblePositions;
    public GameObject[] allCells;

    public playerController playerController;

    public GameObject simpleEnemy;

    public int numberOfEnemies;
    private GameObject enemy;

    private EnemyController[] allEnemies;

    public int click;

    private int clicksPerCycle;
    private int cycle;


    private bool targetsIdentified = false;

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

        int i = 0;

        while (i < numberOfEnemies) 
        {
            
            Vector2 spawnLocation = new Vector2(Random.Range(0, 4), Random.Range(0, 4));

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
        if (playerController.targetPosition != new Vector2(-1, -1) || !playerController.GetComponent<DialogueManager>().dialogueFinished)
        {
            PlayerAction(); 
            StartCoroutine(EnemyActions());
        }        
    }
    private IEnumerator EnemyActions()
    {
        if(!targetsIdentified)
        {
            for (int i = 0; i < allEnemies.Length; i++)
            {
                yield return new WaitForSeconds(0.25f);
                allEnemies[i].EstablishTarget();
            }
            targetsIdentified = true;
            
        }
        else
        {
            for (int i = 0; i < allEnemies.Length; i++)
            {
                yield return new WaitForSeconds(0.25f);
                allEnemies[i].MovePosition();
            }
            for (int i = 0; i < allEnemies.Length; i++)
            {
                yield return new WaitForSeconds(0.25f);
                allEnemies[i].EstablishTarget();
            }
        }


        //ResetCellColor();
    }

    private void PlayerAction()
    {
        playerController.SubmitMove();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            OnCycleStart();
        }

        //ResetCellColor();

    }

    public void ClickUpdate()
    {
        
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

    public void YouLose()
    {
        SceneManager.LoadScene(0);
    }


}
