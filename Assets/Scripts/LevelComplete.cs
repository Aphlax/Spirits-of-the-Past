using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public string nextSceneId;
    public float nextSceneDelay;

    private bool isLevelCompleted = false;
    private float levelCompletedTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isLevelCompleted) {
            float scale = Time.time - this.levelCompletedTime + 1;
            transform.localScale = new Vector3(scale, scale, 1);
        }

        if (this.isLevelCompleted && Time.time > this.levelCompletedTime + nextSceneDelay) {
            SceneManager.LoadScene(nextSceneId, LoadSceneMode.Single);
        }
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            PlayerMovement pm = other.gameObject.GetComponent<PlayerMovement>();
            pm.Reset();
            pm.enabled = false;
            this.isLevelCompleted = true;
            this.levelCompletedTime = Time.time;
        }
    }
}
