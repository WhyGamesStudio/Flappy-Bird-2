using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public Transform pipe;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 0, 2);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Spawn()
    {
        Instantiate(pipe, new Vector3(transform.position.x , Random.Range(-3f, 3f), 0), Quaternion.identity);
    }
}
