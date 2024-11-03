using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene1 : MonoBehaviour
{
    public SpriteRenderer[] allChildren;

    public int startImage;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startImage == 5){

            SceneManager.LoadScene(2);
        }
        if(Input.GetKeyUp(KeyCode.Space)){
            allChildren[startImage].sortingOrder = 1;
            startImage++;
        }

    }
}
