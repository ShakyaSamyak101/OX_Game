using UnityEngine;

public class Player_Selection : MonoBehaviour
{

    public static int PlayerID = 0;
    public static int vsAI = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SelectX()
    {
        Debug.Log("Player X Selected");
        PlayerID = 1;
    }
    public void SelectO()
    {
        Debug.Log("Player O Selected");
        PlayerID = 2;
    }
}

