using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject[,] grid;

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







    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
