using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionDetector : MonoBehaviour
{
    [SerializeField] List<GameObject> m_objectsInCollider = new();
    public List<GameObject> objectsInCollider { get { return m_objectsInCollider; } }

    [SerializeField] List<string> m_tagFilter;

    public UnityEvent<Collider> onTriggerEnter;
    public UnityEvent<Collider> onTriggerStay;
    public UnityEvent<Collider> onTriggerExit;

    public UnityEvent<Collision> onCollisionEnter;
    public UnityEvent<Collision> onCollisionStay;
    public UnityEvent<Collision> onCollisionExit;

    private void OnTriggerEnter(Collider other)
    {
        if (m_tagFilter.Contains(other.tag))
        {
            AddToList(other);
            onTriggerEnter?.Invoke(other);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (m_tagFilter.Contains(other.tag)) onTriggerStay?.Invoke(other);
    }
    private void OnTriggerExit(Collider other)
    {
        RemoveFromList(other);
        onTriggerExit?.Invoke(other);
    }
    void OnCollisionEnter(Collision other)
    {
        if (m_tagFilter.Contains(other.gameObject.tag))
        {
            AddToList(other.collider);
            onCollisionEnter?.Invoke(other);
        }
    }
    public void OnCollisionStay(Collision other)
    {
        if (m_tagFilter.Contains(other.gameObject.tag)) onCollisionStay?.Invoke(other);
    }
    private void OnCollisionExit(Collision other)
    {
        RemoveFromList(other.collider);
        onCollisionExit?.Invoke(other);
    }

    void AddToList(Collider collider) { if (!m_objectsInCollider.Contains(collider.gameObject)) m_objectsInCollider.Add(collider.gameObject); }
    void RemoveFromList(Collider collider) { if (m_objectsInCollider.Contains(collider.gameObject)) m_objectsInCollider.Remove(collider.gameObject); }
}
