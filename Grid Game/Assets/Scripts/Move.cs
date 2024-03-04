using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    //Movement keys
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode leftKey;
    public KeyCode rightKey;

    public float speed = 16;

    public GameObject wallPrefab;

    //Current Wall
    Collider2D wall;

    Vector2 lastWallEnd;

    bool atEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
        spawnWall();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(upKey))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
            spawnWall();
        }
        else if (Input.GetKeyDown(downKey))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.down * speed;
            spawnWall();
        }
        else if (Input.GetKeyDown(leftKey))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.left* speed;
            spawnWall();
        }
        else if (Input.GetKeyDown(rightKey))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
            spawnWall();
        }

        fitColliderBetween(wall, lastWallEnd, transform.position);
    }

    void spawnWall()
    {
        //Save last wall's position
        lastWallEnd = transform.position;
        //Spawn new wall
        GameObject g = (GameObject)Instantiate(wallPrefab, transform.position, Quaternion.identity);
        wall = g.GetComponent<Collider2D>();
    }

    void fitColliderBetween(Collider2D co, Vector2 a, Vector2 b)
    {
        //Get center
        co.transform.position = a + (b - a) * 0.5f;

        //Scale horizontally or vertically
        float dist = Vector2.Distance(a, b);
        if (a.x != b.x)
        {
            co.transform.localScale = new Vector2(dist + 1, 1);
        }
        else
        {
            co.transform.localScale = new Vector2(1, dist + 1);
        }
    }

    void OnTriggerEnter2D(Collider2D co)
    {
       if (co != wall)
        {
            GameObject.Find("player1").GetComponent<Rigidbody2D>().velocity = new Vector2();
            GameObject.Find("player2").GetComponent<Rigidbody2D>().velocity = new Vector2();
            atEnd = true;
        }
    }

    void OnGUI() //Shows win UI when atEnd is true
    {
        if (atEnd)
        {
            GUI.BeginGroup(new Rect((Screen.width / 2) - 50, (Screen.height / 2) - 60, 100, 120));

            GUI.Label(new Rect(0, 0, 100, 20), "You Win!!");

                if (GUI.Button(new Rect(0, 20, 100, 50), "Play Again"))
            {
                SceneManager.LoadScene("LightCyclesTest", LoadSceneMode.Single);
            }
                if (GUI.Button(new Rect(0, 70, 100, 50), "Quit"))
            {
                Application.Quit();
            }
            GUI.EndGroup();
        }
    }
}
