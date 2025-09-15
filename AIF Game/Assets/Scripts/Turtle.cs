using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : MonoBehaviour
{
    LayerMask layerMask;
    public bool playerFront;
    // Start is called before the first frame update
    private void Awake()
    {
        layerMask = LayerMask.GetMask("Player");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerFront = Physics.Raycast(transform.position, Vector3.forward, 5, layerMask);
        if (playerFront = true && Input.GetKeyDown(KeyCode.Space))
        {
            Quest();
        }
    }
    void Quest()
    {

    }
}
