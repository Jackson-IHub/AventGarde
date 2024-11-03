using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CellManager : MonoBehaviour
{
    public bool isOccupied = false; //is there a character or blocker?

    public bool isTargeted = false;

    public GridManager gridManager;
    public Color startingColor;

    private SpriteRenderer spriteRenderer;

    public  EnemyController enemyInCell;

    public Sprite collectible;

    GameObject collectibleObject;

    DialogueManager dialogueManager;

    private bool collectiblePickedUp = false;

    public GameObject targetCircle;

    public GameObject activeCircle;
    public GameObject enemyCircle;

    private void Awake()
    {
        gridManager = FindFirstObjectByType<GridManager>();
        GameObject collectibleObject = new GameObject();
        spriteRenderer = GetComponent<SpriteRenderer>();
        dialogueManager = FindFirstObjectByType<DialogueManager>();
    }

    private void Start()
    {
        activeCircle = Instantiate(targetCircle, this.transform, true);

        activeCircle.transform.localPosition = Vector3.zero;
        activeCircle.SetActive(false);

        enemyCircle = Instantiate(targetCircle, this.transform, true);

        enemyCircle.transform.localPosition = Vector3.zero;
        enemyCircle.SetActive(false);
    }

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

    private void Update()
    {
        if(this.gameObject.GetComponentInChildren<playerController>() && this.GetComponentInChildren<EnemyController>())
        {
            Debug.Log("lost");
            gridManager.YouLose();
        }
        if(this.gameObject.GetComponentInChildren<playerController>() && this.gameObject.GetComponentInChildren<CollectibleObject>())
        {
            collectibleObject = this.gameObject.GetComponentInChildren<CollectibleObject>().gameObject;
            Destroy(collectibleObject);
            dialogueManager.finishedRound = true;
            dialogueManager.Start();
        }
    }

    

    public void ResetColor(bool isPlayer)
    {
        if (isPlayer)
        {
            activeCircle.SetActive(false);
        }
        else
        {
            enemyCircle.SetActive(false);
        }
    }

    public void TargetingSquare(bool isPlayer)
    {
        if (isPlayer)
        {
            activeCircle.SetActive(true);
        }
        else
        {
            enemyCircle.SetActive(true);
        }

        
    }

    public void ResetStatus()
    {
        isTargeted = false;
    }
}
