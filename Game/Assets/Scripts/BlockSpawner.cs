using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour {

    public GameObject[] blocks;

    public float fallTime = .8f;

    public static int height = 20;
    public static int width = 10;
    public static Transform[,] grid = new Transform[width, height];

    private GameObject activeBlock;
    private float previousTime;

    private List<Transform> inactiveBlocks = new List<Transform>();

    void Start()
    {
        SpawnNewBlock();
    }

    public void SpawnNewBlock()
    {
        if (activeBlock != null)
        {
            inactiveBlocks.Add(activeBlock.transform);
        }

        activeBlock = Instantiate(blocks[Random.Range(0, blocks.Length)], transform.position, Quaternion.identity);
        activeBlock.transform.parent = this.transform;
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
            MoveDown();
        }
    }

    #region Movement and rotation functions
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

    public void MoveDown()
    {
        activeBlock.transform.position -= new Vector3(0, 1, 0);
        if (!ValidMove())
        {
            activeBlock.transform.position += new Vector3(0, 1, 0);
            AddToGrid();
            SpawnNewBlock();
        }
        previousTime = Time.time;
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
    #endregion
    void AddToGrid()
    {
        foreach (Transform c in activeBlock.transform)
        {
            int roundX = Mathf.RoundToInt(c.transform.position.x);
            int roundY = Mathf.RoundToInt(c.transform.position.y);

            grid[roundX, roundY] = c;
        }

        RowChecker();
    }

    void RowChecker()
    {
        for (int h = 0; h < height; h++)
        {
            for (int w = 0; w < width; w++)
            {
                if (grid[w, h] == null)
                {
                    break;//empty space no need to continue with this row of blocks
                }
                else if (w == width - 1)
                {
                    //delete row of cubes and move everything down one unit on the y axis
                    RemoveRowFromGrid(h);
                }
            }
        }
    }

    void RemoveRowFromGrid(int row)
    {
        //Debug.Log("Removing " + row);
        for (int i = 0; i < width; i++)
        {
            Destroy(grid[i, row].gameObject);
            grid[i, row] = null;
        }

        MoveBlocksDown(row + 1);
    }

    private void MoveBlocksDown(int row)
    {
        //Debug.Log("Moving bocks down from " + row);
        for (int h = row; h < 19; h++)
        {
            for (int w = 0; w < width; w++)
            {
                //Debug.Log(grid[w, h]);
                if (grid[w, h] != null)
                {
                    //Debug.LogFormat("{0}, {1}", h, w);
                    grid[w, h].transform.position += new Vector3(0, -1, 0);
                }
            }
        }

        ResetGrid(row);
    }

    void ResetGrid(int startingRow)
    {
        //Debug.Log("restting grid");
        for (int h = startingRow ; h < 19; h++)
        {
            for (int w = 0; w < width; w++)
            {
                grid[w, h - 1] = grid[w, h];
            }
        }
    }

}
