
using UnityEngine;

public class credits : MonoBehaviour
{
    public SpriteRenderer credit;

    public float speed;

    Vector3 move;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }




    // Update is called once per frame
    void Update()
    {
        //credit.transform.position = Vector2.down*Time.deltaTime

        if (credit.transform.position.y >= 25.8)
        {
            Application.Quit();
        }

        move = new Vector3(0, -speed * Time.deltaTime);
    
        credit.transform.position += move;


    }
}
