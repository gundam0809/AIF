using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnCollision(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Gundam 00 is better the unicorn ong");
            Destroy(gameObject);
        }
    }
}
