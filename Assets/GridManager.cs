using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GridManager : MonoBehaviour
{
    public int sceneNumber;

    public GameObject[,] grid;

    public int gridLength;
    public int gridHeight;

    DialogueManager dialogueManager;

    public Vector2[] possiblePositions;
    public GameObject[] allCells;

    public playerController playerController;

    public GameObject simpleEnemy;

    private int numberOfEnemies = 0;
    private GameObject enemy;

    private List<EnemyController> allEnemies = new List<EnemyController>();

    public int click;

    private int clicksPerCycle;
    private int cycle;


    private bool targetsIdentified = false;

    private void Awake()
    {
        dialogueManager = this.GetComponent<DialogueManager>();
        grid =  new GameObject[gridLength, gridHeight];
        int i = 0;
        foreach (var cell in allCells)
        {
            grid[(int)possiblePositions[i].x, (int)possiblePositions[i].y] = allCells[i];
            i++;
        }

        clicksPerCycle = numberOfEnemies;
        playerController = FindFirstObjectByType<playerController>();
    }

    private void Start()
    {
        InitializeEnemies();
    }

    public void AddEnemy(EnemyController enemy)
    {
        allEnemies.Add(enemy);
        numberOfEnemies++;
        clicksPerCycle = numberOfEnemies;
    }

    public void NextScene()
    {
        SceneManager.LoadScene(sceneNumber +1);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(sceneNumber);
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

        //int i = 0;

        //while (i < numberOfEnemies) 
        //{
            
        //    Vector2 spawnLocation = new Vector2(Random.Range(0, 4), Random.Range(0, 4));

        //    if((grid[(int)spawnLocation.x, (int)spawnLocation.y].GetComponent<CellManager>().isOccupied) || (grid[(int)spawnLocation.x, (int)spawnLocation.y].GetComponent<CellManager>().isTargeted))
        //    {
        //        spawnLocation = new Vector2(Random.Range(0, 4), Random.Range(0, 4));
                
        //    }
        //    else
        //    {
        //        GameObject enemy = Instantiate(simpleEnemy);
        //        EnemyController controller = enemy.GetComponent<EnemyController>();
        //        controller.gridManager = this.gameObject.GetComponent<GridManager>();
        //        controller.startingSquare = spawnLocation;
        //        controller.Spawn();
        //        allEnemies[i] = controller;
        //        i++;
        //    }
        //}
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
        if (playerController.targetPosition != new Vector2(-1, -1) && dialogueManager.dialogueFinished == true)
        {
            PlayerAction(); 
            StartCoroutine(EnemyActions());
        }

        
    }
    private IEnumerator EnemyActions()
    {
        if(!targetsIdentified)
        {
            for (int i = 0; i < allEnemies.Count; i++)
            {
                yield return new WaitForSeconds(0.25f);
                allEnemies[i].EstablishTarget();
            }
            targetsIdentified = true;
            
        }
        else
        {
            for (int i = 0; i < allEnemies.Count; i++)
            {
                yield return new WaitForSeconds(0.25f);
                allEnemies[i].MovePosition();
            }
            for (int i = 0; i < allEnemies.Count; i++)
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
        SceneManager.LoadScene(sceneNumber);
    }


}
