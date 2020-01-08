using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour {

    public GameObject[] blocks;
    public GameObject activeBlock;
    // Use this for initialization

    private float previousTime;

    public float fallTime = .8f;

    public static int height = 20;
    public static int width = 10;
    public static Transform[,] grid = new Transform[width, height];
    void Start ()
    {
        SpawnNewBlock();
	}

    public void SpawnNewBlock()
    {
        activeBlock = Instantiate(blocks[Random.Range(0, blocks.Length)], transform.position, Quaternion.identity);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            RotatePiece();

        }

        if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
        {
            activeBlock.transform.position -= new Vector3(0, 1, 0);
            if (!ValidMove())
            {
                activeBlock.transform.position += new Vector3(0, 1, 0);
                AddToGrid();
                this.enabled = false;
                SpawnNewBlock();
            }
            previousTime = Time.time;
        }
    }

    public void RotatePiece()
    {
        activeBlock.transform.eulerAngles += new Vector3(0, 0, 90);

        if (!ValidMove())
        {
            activeBlock.transform.eulerAngles -= new Vector3(0, 0, 90);
        }
    }

    public void MoveRight()
    {
        activeBlock.transform.position += new Vector3(1, 0, 0);
        if (!ValidMove())
        {
            activeBlock.transform.position -= new Vector3(1, 0, 0);
        }
    }

    public void MoveLeft()
    {
        activeBlock.transform.position -= new Vector3(1, 0, 0);
        if (!ValidMove())
        {
            activeBlock.transform.position += new Vector3(1, 0, 0);
        }
    }

    bool ValidMove()
    {
        foreach (Transform c in activeBlock.transform)
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

    void AddToGrid()
    {
        foreach (Transform c in activeBlock.transform)
        {
            int roundX = Mathf.RoundToInt(c.transform.position.x);
            int roundY = Mathf.RoundToInt(c.transform.position.y);

            grid[roundX, roundY] = c;
        }
    }

}
