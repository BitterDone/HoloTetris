using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockControlScript : MonoBehaviour
{
    private BlockSpawner spawner;
    private float previousTime;

    public float fallTime = .8f;

    public static int height = 20;
    public static int width = 10;
    public static Transform[,] grid = new Transform[width, height];

	// Use this for initialization
	void Start ()
    {
        spawner = GameObject.Find("Spawner").GetComponent<BlockSpawner>();	
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position -= new Vector3(1, 0, 0);
            if (!ValidMove())
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.eulerAngles += new Vector3(0, 0, 90);

            if (!ValidMove())
            {
                transform.eulerAngles -= new Vector3(0, 0, 90);
            }

        }

        if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
        {
            transform.position -= new Vector3(0, 1, 0);
            if (!ValidMove())
            {
                transform.position += new Vector3(0, 1, 0);
                AddToGrid();
                this.enabled = false;
                spawner.SpawnNewBlock();
            }
            previousTime = Time.time;
        }
    }

    void AddToGrid()
    {
        foreach (Transform c in transform)
        {
            int roundX = Mathf.RoundToInt(c.transform.position.x);
            int roundY = Mathf.RoundToInt(c.transform.position.y);

            grid[roundX, roundY] = c;
        }
    }

    bool ValidMove()
    {
        foreach (Transform c in transform)
        {
            int roundX = Mathf.RoundToInt(c.transform.position.x);
            int roundY = Mathf.RoundToInt(c.transform.position.y);

            if (roundX < 0 || roundX >= width || roundY < 0)
            {
                return false;
            }
            if (grid[roundX, roundY] != null)
            {
                return false;
            }
        }

        return true;
    }
}
