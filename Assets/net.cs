using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class net : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Random")
        {
            Destroy(other.gameObject);
            //Debug.Log("touched");
        }
    }
}
