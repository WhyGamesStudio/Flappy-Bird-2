using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMover : MonoBehaviour
{
    MeshRenderer mer;
    
    // Start is called before the first frame update
    void Start()
    {
        mer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        mer.material.mainTextureOffset += Vector2.right * 0.3f * Time.deltaTime;
    }
}
