using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour {

    public GameObject[] blocks;

	// Use this for initialization
	void Start ()
    {
        SpawnNewBlock();
	}

    public void SpawnNewBlock()
    {
        Instantiate(blocks[Random.Range(0, blocks.Length)], transform.position, Quaternion.identity);
    }
	
}
