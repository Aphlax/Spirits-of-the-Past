using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShowHint : MonoBehaviour
{

    public GameObject panel;
    public GameObject player;
    public float distance;

    
    // Start is called before the first frame update
    void Start() {
        panel.active = false;
    }

    // Update is called once per frame
    void Update() {
        panel.active = Vector2.Distance(transform.position, player.transform.position) < distance;
    }
}
