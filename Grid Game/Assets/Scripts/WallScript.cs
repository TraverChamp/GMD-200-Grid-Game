using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WallScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1"))
        {
            // Load the next scene
            SceneManager.LoadScene("EndP1");
        }
        if (collision.CompareTag("Player2"))
        {
            SceneManager.LoadScene("EndP2");
        }
    }
}
