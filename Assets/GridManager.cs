using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject[,] grid = new GameObject[5,5];

    public Vector2[] possiblePositions;
    public GameObject[] allCells;

    private void Awake()
    {
        int i = 0;
        foreach (var cell in allCells)
        {

            grid[(int)possiblePositions[i].x, (int)possiblePositions[i].y] = allCells[i];
            i++;
        }
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







    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
