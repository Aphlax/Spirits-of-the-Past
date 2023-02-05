using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{

    public float speed;
    public int startPoint;
    public Transform[] points;
    public int mode;

    private int i;
    private bool reverse = false;

    private AnimationCurve heightAnimation;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[startPoint].position;
        heightAnimation = new  AnimationCurve(
            new Keyframe(0, 0),
            new Keyframe(1, 1));
        heightAnimation.preWrapMode  = WrapMode.PingPong;
        heightAnimation.postWrapMode  = WrapMode.PingPong;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.05f) {
            if (reverse)
                i--;
            else
                i++;

            if (i == points.Length) {
                i--;
                reverse = true;
            }

            if (i == 0) {
                reverse = false;
            }
        }

        if (mode == 1)
            NormalMoving();

        if (mode == 2)
            SmoothMoving();
    }

    void NormalMoving() {
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }

    void SmoothMoving() {
        transform.position = Vector2.Lerp(transform.position, points[i].position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player"))
            collision.transform.SetParent(transform);
    }

    
    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player"))
            collision.transform.SetParent(null);
    }
}
