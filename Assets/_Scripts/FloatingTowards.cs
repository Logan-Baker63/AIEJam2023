using UnityEngine;
using System.Collections;

public class FloatingTowards : MonoBehaviour
{
    // The target marker.
    public Transform target;

    public float minSpeed = 10f;
    public float maxSpeed = 50f;


    // Speed in units per sec.
    private float speed;

    private void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        // The step size is equal to speed times frame time.
        float step = speed * Time.deltaTime;

        // Move our position a step closer to the target.
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
