using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public GameObject target;

    private float center;
    private AnimationCurve heightAnimation;

    // Start is called before the first frame update
    void Start()
    {
        center = transform.position.y;

        heightAnimation = new  AnimationCurve(
            new Keyframe(-8, -4),
            new Keyframe(-4, 0),
            new Keyframe(4, 0),
            new Keyframe(8, 4));
        heightAnimation.SmoothTangents(0, 0);
        heightAnimation.SmoothTangents(3, 0);
        heightAnimation.preWrapMode  = WrapMode.ClampForever;
        heightAnimation.postWrapMode  = WrapMode.ClampForever;
    }

    // Update is called once per frame
    void Update()
    {
        float y = center + heightAnimation.Evaluate(target.transform.position.y - center);
        transform.position = new Vector3(target.transform.position.x, y,  transform.position.z);
    }
}
