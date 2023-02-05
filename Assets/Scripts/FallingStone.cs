using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingStone : MonoBehaviour
{
    private GameObject player;

    private Rigidbody2D rb;
    private Vector2 startPosition;
    private Quaternion startRotation;
    private bool isFalling;
    private bool initial;
    private float fallingStartTime;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
                rb.isKinematic = true;
        this.isFalling = false;
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isFalling) {
            float time = Time.time - fallingStartTime;

            if (time < 0.1f) {
                this.transform.position = new Vector2(transform.position.x, transform.position.y - Time.deltaTime * 0.6f);
            } else if (time < 0.25f) {

            } else if (initial) {
                // enable
                rb.isKinematic = false;
                rb.velocity = new Vector2(0, 0.3f);
                initial = false;
                
                if (player != null) {
                    player.transform.SetParent(null);
                    player = null;
                }
            }

            if (time > 3) {
                this.transform.position = startPosition;
                transform.rotation = startRotation;
                // disable
                rb.isKinematic = true;
                rb.velocity = new Vector2(0, 0);
                isFalling = false;
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player") && !isFalling) {
            other.transform.SetParent(transform);
            isFalling = true;
            initial = true;
            fallingStartTime = Time.time;
            player = other.gameObject;
        }
    }

    public void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
           other.transform.SetParent(null);
           player = null;
        }
    }
}
