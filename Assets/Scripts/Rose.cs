using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rose : MonoBehaviour
{

    public GameObject gameOver;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.name == "Rose Stone") {
            GetComponent<Explodable>().explode();
            gameOver.SetActive(true);
        }
    }
}
