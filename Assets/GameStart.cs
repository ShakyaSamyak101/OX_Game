using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    private bool AI_Turn = false;
    public GameObject GameOverScreen_AI;
    public GameObject GameOverScreen_Pvp;
    public GameObject RetryPlayerSelection;

    private float Delay = 0;// To add delay for AI move

    public int AI = Buttons.vsAI;// To check if AI is enabled

    public GameObject Top_Left;
    public GameObject Top;
    public GameObject Top_Right;
    public GameObject Right;
    public GameObject Bottom_Right;
    public GameObject Bottom;
    public GameObject Bottom_Left;
    public GameObject Left;
    public GameObject Middle;
    public GameObject Pvp1;
    public GameObject Pvp2;
    public GameObject PvA1;
    public GameObject PvA2;

    private GameObject[] ClickAreas;

    public SpriteRenderer X_SpriteRenderer;
    public SpriteRenderer O_SpriteRenderer;
    public SpriteRenderer X_opaque_SpriteRenderer;
    public SpriteRenderer O_opaque_SpriteRenderer;

    public int id = Player_Selection.PlayerID;
    public int AISymbol = 0;

    public GameObject XTurn;
    public GameObject OTurn;
    public GameObject XWon;
    public GameObject OWon;

    private float timer = 0;// Timer to hide the announcement


    private GameObject X_1;
    private GameObject X_2;
    private GameObject X_3;
    private GameObject O_1;
    private GameObject O_2;
    private GameObject O_3;

    private int TurnCountX = 0;// To delete the X after 3 turns
    private int TurnCountO = 0;// To delete the O after 3 turns
    private int DeleteX = 0;// To delete the X after 3 turns
    private int DeleteO = 0;// To delete the O after 3 turns



    // Winning Combinations
    private string[] Row1 = new string[] { "Top Left", "Top", "Top Right" };
    private string[] Row2 = new string[] { "Left", "Middle", "Right" };
    private string[] Row3 = new string[] { "Bottom Left", "Bottom", "Bottom Right" };
    private string[] Column1 = new string[] { "Top Left", "Left", "Bottom Left" };
    private string[] Column2 = new string[] { "Top", "Middle", "Bottom" };
    private string[] Column3 = new string[] { "Top Right", "Right", "Bottom Right" };
    private string[] Diagonal1 = new string[] { "Top Left", "Middle", "Bottom Right" };
    private string[] Diagonal2 = new string[] { "Top Right", "Middle", "Bottom Left" };

    private string[] Xwincheck;
    private string[] Owincheck;
    public Text TopScore;
    public Text BottomScore;
    private string[][] Wincondition;

    //private int MaxAIDepth = 3; // How many turns ahead the AI checks.
    private int Turn = 1;
    private string Player = "";


    private void DeactivateTurn()
    {
        timer += Time.deltaTime;
        if (timer > 2)
        {
            if (XTurn.activeSelf == true || OTurn.activeSelf == true)
            {
                timer = 0;
                XTurn.SetActive(false);
                OTurn.SetActive(false);
                timer = 0;
                Debug.Log("Turn Announcement Deactivated");
            }
        }
    }

    private void ActivateTurn()
    {
        if (!XWon.activeSelf && !OWon.activeSelf)
        {
            if (id == 1)
            {
                XTurn.SetActive(true);
            }
            else if (id == 2)
            {
                OTurn.SetActive(true);
            }
        }
    }

    public void ReturntoMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }

    public void PlayerSelectedX()
    {
        Player = "X";
        id = 1;
        RetryPlayerSelection.SetActive(false);
        AISymbol = 2;
        XTurn.SetActive(true);
        Turn = 1;
    }

    public void PlayerSelectedO()
    {
        DisableClick();
        Player = "O";
        id = 2;
        RetryPlayerSelection.SetActive(false);
        AISymbol = 1;
        XTurn.SetActive(true);
        Turn = 1;
    }

    public void RestartGame()
    {
        GameOverScreen_AI.SetActive(false);
        XWon.SetActive(false);
        OWon.SetActive(false);
        if (AI == 1)
        {
            RetryPlayerSelection.SetActive(true);
        }

        // Reset all variables and UI elements
        foreach (var area in ClickAreas)
        {
            area.GetComponent<SpriteRenderer>().sprite = null;
            if (area.TryGetComponent<BoxCollider2D>(out var collider))
            {
                collider.enabled = true;
            }
        }
        XWon.SetActive(false);
        OWon.SetActive(false);
        X_1 = null;
        X_2 = null;
        X_3 = null;
        O_1 = null;
        O_2 = null;
        O_3 = null;
        TurnCountX = 0;
        TurnCountO = 0;
        DeleteX = 0;
        DeleteO = 0;
        if (AI == 0)
        {
            id = 1;
            XTurn.SetActive(true);
            Turn = 1;
            GameOverScreen_Pvp.SetActive(false);
        }
        else
        {
            id = 0;
        }
        //MaxAIDepth = 3;
        AI_Turn = false;
        timer = 0;
    }
    private void DisableClick()
    {
        AI_Turn = true;
    }

    private void EnableClick()
    {
        AI_Turn = false;
    }

    void Start()
    {
        Debug.Log("Game Started" + AI);
        AI = Buttons.vsAI;// To check if AI is enabled
        Debug.Log("Game Started" + AI);
        if (AI == 0)
        {
            Debug.Log("Pvp Enabled");
            Pvp1.SetActive(true);
            Pvp2.SetActive(true);
            EnableClick();
            id = 1;
        }
        else if (AI == 1)
        {
            id = Player_Selection.PlayerID;//get PlayerID from Player_Selection script
            Debug.Log("AI Enabled");
            PvA1.SetActive(true);
            PvA2.SetActive(true);
        }

        if (id == 1)
        {
            Player = "X";
            if (AI == 1)
            {
                AISymbol = 2;
            }

        }
        else
        {
            Player = "O";
            if (AI == 1)
            {
                AISymbol = 1;
            }
        }
        if (AI == 1 && Player == "O")
        {
            DisableClick();
        }

        Wincondition = new string[][] { Row1, Row2, Row3, Column1, Column2, Column3, Diagonal1, Diagonal2 };

        ClickAreas = new GameObject[] { Top_Left, Top, Top_Right, Right, Bottom_Right, Bottom, Bottom_Left, Left, Middle };

        XTurn.SetActive(true);
        if (XTurn.activeSelf == true || OTurn.activeSelf == true)
        {
            DeactivateTurn();
        }
    }

    void Update()
    {
        Debug.LogWarning(X_1);
        Debug.LogWarning(X_2);
        Debug.LogWarning(X_3);

        Debug.LogWarning(O_1);
        Debug.LogWarning(O_2);
        Debug.LogWarning(O_3);
        //Remove Announcement
        if (XTurn.activeSelf == true || OTurn.activeSelf == true)
        {
            DeactivateTurn();
        }

        // Place only the first X in a random place if AI is enabled and X starts
        if (AISymbol == 1 && AI == 1 && Turn == 1 && !XTurn.activeSelf)
        {
            Debug.Log("AI's First Move");
            Turn = 10;
            var board = GetBoardState();
            var available = board
                .Select((cell, idx) => new { cell, idx })
                .Where(x => x.cell == "")
                .Select(x => x.idx)
                .ToList();
            if (available.Count > 0)
            {
                Debug.Log("Placing first X");
                Delay = 0;
                int RandomArea = available[UnityEngine.Random.Range(0, available.Count)];
                var selectedArea = ClickAreas[RandomArea];
                selectedArea.GetComponent<SpriteRenderer>().sprite = X_SpriteRenderer.sprite;

                // Store X turns to delete after 3 turns
                X_1 = selectedArea;
                TurnCountX = 1;
                // Switch turn to player X
                EnableClick();
                id = 2;
                XTurn.SetActive(false);
                OTurn.SetActive(true);

            }
        }

        //Game Over Detection
        if (XWon.activeSelf == true || OWon.activeSelf == true)
        {
            Debug.LogWarning("Game Over");
        }

        //Click Detection
        if (Input.GetMouseButtonDown(0) && AI_Turn == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            // To prevent errors if clicked outside any collider
            if (hit.collider == null)
            {
                return;
            }

            // Get the SpriteRenderer component of the clicked object
            SpriteRenderer sr = hit.collider.gameObject.GetComponent<SpriteRenderer>();

            if (hit.collider != null)
            {
                if (sr.sprite == null)
                {
                    // X Turn
                    if (id == 1)
                    {
                        hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = X_SpriteRenderer.sprite;

                        // Store X turns and delete after 3 turns
                        if (DeleteX == 1)
                        {
                            X_3.GetComponent<SpriteRenderer>().sprite = null;
                            DeleteX = 0;
                        }
                        if (TurnCountX < 3)
                        {
                            TurnCountX++;
                        }
                        if (TurnCountX == 1)
                        {
                            X_1 = hit.collider.gameObject;
                        }
                        if (TurnCountX == 2)
                        {
                            X_2 = X_1;
                            X_1 = hit.collider.gameObject;
                        }
                        if (TurnCountX == 3)
                        {
                            X_3 = X_2;
                            X_2 = X_1;
                            X_1 = hit.collider.gameObject;
                        }

                        // Check and make X opaque
                        if (X_3 != null)
                        {
                            X_3.GetComponent<SpriteRenderer>().sprite = X_opaque_SpriteRenderer.sprite;
                            DeleteX = 1;

                            // Check if X Won
                            Xwincheck = new string[] { X_1.name, X_2.name, X_3.name };
                            if (Wincondition.Any(condition => condition.All(x => Xwincheck.Contains(x))))
                            {
                                XWon.SetActive(true);

                                //Change score
                                TopScore.text = (int.Parse(TopScore.text) + 1).ToString();
                                if (AI == 1)
                                {
                                    GameOverScreen_AI.SetActive(true);
                                }
                                else
                                {
                                    GameOverScreen_Pvp.SetActive(true);
                                }
                                // Disable all colliders to prevent further clicks
                                DisableClick();
                            }
                        }

                        // Switch Turn
                        Turn = 10;
                        XTurn.SetActive(false);
                        timer = 0;
                        if (AI == 1)
                        {
                            DisableClick();
                        }
                        id = 2;
                    }

                    // O Turn
                    else if (id == 2)
                    {
                        hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = O_SpriteRenderer.sprite;

                        // Store O turns and delete after 3 turns
                        if (DeleteO == 1)
                        {
                            O_3.GetComponent<SpriteRenderer>().sprite = null;
                            DeleteO = 0;
                        }
                        if (TurnCountO < 3)
                        {
                            TurnCountO++;
                        }
                        if (TurnCountO == 1)
                        {
                            O_1 = hit.collider.gameObject;
                        }
                        if (TurnCountO == 2)
                        {
                            O_2 = O_1;
                            O_1 = hit.collider.gameObject;
                        }
                        if (TurnCountO == 3)
                        {
                            O_3 = O_2;
                            O_2 = O_1;
                            O_1 = hit.collider.gameObject;
                        }

                        // Check and make O opaque
                        if (O_3 != null)
                        {
                            O_3.GetComponent<SpriteRenderer>().sprite = O_opaque_SpriteRenderer.sprite;
                            DeleteO = 1;

                            // Check if O Won
                            Owincheck = new string[] { O_1.name, O_2.name, O_3.name };
                            if (Wincondition.Any(condition => condition.All(x => Owincheck.Contains(x))))
                            {
                                OWon.SetActive(true);

                                //Change score
                                if (AI == 1 && Player == "O")
                                {
                                    TopScore.text = (int.Parse(TopScore.text) + 1).ToString();
                                }
                                else
                                {
                                    BottomScore.text = (int.Parse(BottomScore.text) + 1).ToString();
                                }

                                if (AI == 1)
                                {
                                    GameOverScreen_AI.SetActive(true);
                                }
                                else
                                {
                                    GameOverScreen_Pvp.SetActive(true);
                                }

                                // Disable all colliders to prevent further clicks
                                DisableClick();
                            }
                        }

                        // Switch Turn
                        Turn = 10;
                        OTurn.SetActive(false);
                        timer = 0;
                        if (AI == 1)
                        {
                            DisableClick();
                        }
                        id = 1;
                    }

                    //Turn Announcement
                    ActivateTurn();
                }
            }
        }

        //AI
        if (AI == 1 && !OWon.activeSelf && !XWon.activeSelf)
        {

            var board = GetBoardState();
            int bestMove = GetBestMove(board);
            if (AISymbol == 2 && id == 2 && Delay < 0.5)
            {
                Delay += Time.deltaTime;
                return;
            }
            if (AISymbol == 1 && id == 1 && Delay < 0.5)
            {
                Delay += Time.deltaTime;
                return;
            }
            if (bestMove != -1)
            {
                var selectedArea = ClickAreas[bestMove];
                if (AISymbol == 2 && id == 2 && Delay > 0.5) // AI is O
                {
                    Delay = 0;
                    selectedArea.GetComponent<SpriteRenderer>().sprite = O_SpriteRenderer.sprite;
                    // Store O turns and delete after 3 turns
                    if (DeleteO == 1)
                    {
                        O_3.GetComponent<SpriteRenderer>().sprite = null;
                        DeleteO = 0;
                    }
                    if (TurnCountO < 3)
                    {
                        TurnCountO++;
                    }
                    if (TurnCountO == 1)
                    {
                        O_1 = selectedArea;
                    }
                    if (TurnCountO == 2)
                    {
                        O_2 = O_1;
                        O_1 = selectedArea;
                        //MaxAIDepth = 10;// Allow deeper lookahead
                    }
                    if (TurnCountO == 3)
                    {
                        O_3 = O_2;
                        O_2 = O_1;
                        O_1 = selectedArea;
                    }
                    // Check and make O opaque
                    if (O_3 != null)
                    {
                        O_3.GetComponent<SpriteRenderer>().sprite = O_opaque_SpriteRenderer.sprite;
                        DeleteO = 1;
                        // Check if O Won
                        Owincheck = new string[] { O_1.name, O_2.name, O_3.name };
                        if (Wincondition.Any(condition => condition.All(x => Owincheck.Contains(x))))
                        {
                            OWon.SetActive(true);
                            BottomScore.text = (int.Parse(BottomScore.text) + 1).ToString();
                            GameOverScreen_AI.SetActive(true);
                            DisableClick();
                        }
                    }
                    // Switch turn to player X
                    Turn = 10;
                    if (!OWon.activeSelf)
                    {
                        id = 1;
                        XTurn.SetActive(true);
                        EnableClick();
                        timer = 0;
                    }
                    OTurn.SetActive(false);
                }
                else if (AISymbol == 1 && id == 1 && Delay > 0.5) // AI is X
                {
                    Delay = 0;
                    selectedArea.GetComponent<SpriteRenderer>().sprite = X_SpriteRenderer.sprite;
                    // Store X turns and delete after 3 turns
                    if (DeleteX == 1)
                    {
                        X_3.GetComponent<SpriteRenderer>().sprite = null;
                        DeleteX = 0;
                    }
                    if (TurnCountX < 3)
                    {
                        TurnCountX++;
                    }
                    if (TurnCountX == 1)
                    {
                        X_1 = selectedArea;
                    }
                    if (TurnCountX == 2)
                    {
                        X_2 = X_1;
                        X_1 = selectedArea;
                        //MaxAIDepth = 10;// Allow deeper lookahead
                    }
                    if (TurnCountX == 3)
                    {
                        X_3 = X_2;
                        X_2 = X_1;
                        X_1 = selectedArea;
                    }
                    // Check and make X opaque
                    if (X_3 != null)
                    {
                        X_3.GetComponent<SpriteRenderer>().sprite = X_opaque_SpriteRenderer.sprite;
                        DeleteX = 1;
                        // Check if X Won
                        Xwincheck = new string[] { X_1.name, X_2.name, X_3.name };
                        if (Wincondition.Any(condition => condition.All(x => Xwincheck.Contains(x))))
                        {
                            XWon.SetActive(true);
                            Debug.LogWarning("AI X Won" + Player);
                            if (Player == "O")
                            {
                                BottomScore.text = (int.Parse(BottomScore.text) + 1).ToString();
                            }
                            else if (Player == "X")
                            {
                                TopScore.text = (int.Parse(TopScore.text) + 1).ToString();
                            }
                            GameOverScreen_AI.SetActive(true);
                            DisableClick();
                        }
                    }
                    // Switch turn to player O
                    Turn = 10;
                    if (!XWon.activeSelf)
                    {
                        id = 2;
                        OTurn.SetActive(true);
                        EnableClick();
                        timer = 0;
                    }
                    XTurn.SetActive(false);
                }
                if (XTurn.activeSelf == true || OTurn.activeSelf == true)
                {
                    DeactivateTurn();
                }
            }
        }
    }

    // Rule-based AI

    private string[] GetBoardState()
    {
        return ClickAreas.Select(area =>
        {
            var sr = area.GetComponent<SpriteRenderer>();
            if (sr.sprite == X_SpriteRenderer.sprite)
            {
                return "X";
            }
            if (sr.sprite == X_opaque_SpriteRenderer.sprite)
            {
                return "Xo";
            }
            if (sr.sprite == O_SpriteRenderer.sprite)
            {
                return "O";
            }
            if (sr.sprite == O_opaque_SpriteRenderer.sprite)
            {
                return "Oo";
            }
            return "";
        }).ToArray();
    }

    private int GetBestMove(string[] CurrentBoard)
    {
        int bestScore = -1;
        int score = -1;
        int move = -1;
        string AI_PlayerSymbol = (AISymbol == 1) ? "O" : "X";
        string AI_AiSymbol = (AISymbol == 1) ? "X" : "O";

        // If AI can win immediately, take that move.
        for (int i = 0; i < CurrentBoard.Length; i++)
        {
            if (CurrentBoard[i] != "")
            {
                continue;
            }
            CurrentBoard[i] = AI_AiSymbol;            
            if (Function_CheckWinner(CurrentBoard) == AI_AiSymbol)
            {
                CurrentBoard[i] = "";
                score = 100;
                if (score > bestScore)
                {
                    bestScore = score;
                    move = i;
                }
            }
            CurrentBoard[i] = "";
        }

        // If opponent can win next, block them.
        for (int i = 0; i < CurrentBoard.Length; i++)
        {
            if (CurrentBoard[i] != "")
            {
                continue;
            }
            CurrentBoard[i] = AI_PlayerSymbol;            
            if (Function_CheckWinner(CurrentBoard) == AI_PlayerSymbol)
            {
                CurrentBoard[i] = "";
                score = 99;
                if (score > bestScore)
                {
                    bestScore = score;
                    move = i;
                }
            }
            CurrentBoard[i] = "";
        }

        if (bestScore < 0)
        {
            var available = CurrentBoard
            .Select((cell, idx) => new { cell, idx })
            .Where(x => x.cell == "")
            .Select(x => x.idx)
            .ToList();

            if (available.Count == 0)
            {
                return -1;
            }
            bestScore = 0;
            move = available[UnityEngine.Random.Range(0, available.Count)];
        }
        return move;
    }

    private string Function_CheckWinner(string[] board)
    {
        foreach (var condition in Wincondition)
        {
            var indices = condition.Select(name => System.Array.FindIndex(ClickAreas, a => a.name == name)).ToArray();
            if (indices.All(i => board[i] == "X"))
            {
                return "X";
            }

            if (indices.All(i => board[i] == "O"))
            {
                return "O";
            }
        }
        return "";
    }
}