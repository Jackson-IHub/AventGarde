using UnityEngine;

public class CellManager : MonoBehaviour
{
    public bool isOccupied = false; //is there a character or blocker?

    public Color color;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ResetColor()
    {
        spriteRenderer.color = color;
    }







}
