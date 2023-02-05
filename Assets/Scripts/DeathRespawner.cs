using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathRespawner : MonoBehaviour
{
    public float respawnTime;

    private PlayerMovement pm;
    private GameObject lastRespawnPoint;
    private bool isRespawning = false;
    private float lastRespawnTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isRespawning && Time.time > this.lastRespawnTime + respawnTime) {
            this.isRespawning = false;
            pm.enabled = true; // enable player movement
        }
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("RespawnPoint")) {
            this.lastRespawnPoint = other.gameObject;
        }
    }

    public void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("DamageZone")) {
            transform.position = this.lastRespawnPoint.transform.position;
            this.isRespawning = true;
            this.lastRespawnTime = Time.time;
            pm.Reset();
            pm.enabled = false; // disable player movement
        }
    }
}
