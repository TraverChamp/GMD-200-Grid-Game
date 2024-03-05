using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gridassist : MonoBehaviour
{
    public Texture2D levelTexture;
    public SpriteRenderer tile;
    private GameObject[] _tiles;
    // Start is called before the first frame update
    void Start()
    {

        int width = levelTexture.width;
        int height = levelTexture.height;
        Color[] colors = levelTexture.GetPixels();
        for (int i = 0; i < colors.Length; i++)
        {
            Vector3 tilePosition = new Vector3(
                i % width, i / height,0);
            //SpriteRenderer tile = Instantiate();
        }

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
