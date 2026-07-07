using UnityEngine;

public class Buttons : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject StartButton;
    public GameObject CreditButton;
    public GameObject PlayerSelectionButton;
    public static int vsAI = 0;
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        Debug.Log("Game Started");
    }

    public void StartButtonClicked()
    {
        Debug.Log("Start Button Clicked");
        StartButton.SetActive(true);
    }
    public void StartButtonBack()
    {
        Debug.Log("Back Button Clicked");
        StartButton.SetActive(false);

    }
    public void vsPlayerClicked()
    {
        vsAI = 0;
        Debug.Log("vs PLayer Clicked");     
    }
    public void vsAIClicked()
    {
        vsAI = 1;
        Debug.Log("vs AI Clicked");
        PlayerSelectionButton.SetActive(true);
        StartButton.SetActive(false);
    }

    public void vsAIBack()
    {
        Debug.Log("vs AI Clicked");
        PlayerSelectionButton.SetActive(false);
    }

    public void CreditButtonClicked()
    {
        Debug.Log("Credit Button Clicked");
        CreditButton.SetActive(true);
    }
    public void CreditButtonBack()
    {
        Debug.Log("Credit Back Button Clicked");
        CreditButton.SetActive(false);

    }
    public void QuitGame()
    {
        Debug.Log("Quit Game Button Clicked");
        Application.Quit();
    }
}


