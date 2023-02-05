using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms2 : MonoBehaviour
{
    public GameObject target;
    public float speed;

    private Vector2 startPosition;
    private AnimationCurve heightAnimation;

    void Start()
    {
        startPosition = transform.position;
        
        heightAnimation = new  AnimationCurve(
            new Keyframe(0, 0),
            new Keyframe(1, 1));
        heightAnimation.preWrapMode  = WrapMode.PingPong;
        heightAnimation.postWrapMode  = WrapMode.PingPong;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.Lerp(startPosition, target.transform.position, heightAnimation.Evaluate(Time.time * speed));
    }

    public void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            other.transform.SetParent(transform);
        }
    }

    public void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            other.transform.SetParent(null);
        }
    }
}
