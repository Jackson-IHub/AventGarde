using UnityEngine;

public class CollectibleObject : MonoBehaviour
{
    public Vector2 startingPos;
    public GridManager gridManager;
    public GameObject currentCell;

    private void Awake()
    {
        gridManager = FindFirstObjectByType<GridManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentCell = gridManager.grid[(int)startingPos.x, (int)startingPos.y];
        this.transform.SetParent(currentCell.transform, true);
        this.transform.localPosition = Vector2.zero; //visual update
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
