using UnityEngine;
using System.Collections;

public class FloatingTowards : MonoBehaviour
{
    // The target marker.
    public Transform target;

    // Speed in units per sec.
    public float speed;

    void Update()
    {
        // The step size is equal to speed times frame time.
        float step = speed * Time.deltaTime;

        // Move our position a step closer to the target.
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
