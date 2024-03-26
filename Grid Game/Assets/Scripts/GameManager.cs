using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int player1Score = 0;
    public int player2Score = 0;
    public int winScore = 3;

    public void PlayerWon(int playerNumber)
    {
        if (playerNumber == 1)
        {
            player1Score++;
        }
        else if (playerNumber == 2)
        {
            player2Score++;
        }

        if (player1Score >= winScore || player2Score >= winScore)
        {
            SceneManager.LoadScene("WinScreenScene");
        }
    }
}
