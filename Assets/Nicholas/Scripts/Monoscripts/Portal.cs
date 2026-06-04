using System;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform connectedPortal;
    private HashSet<GameObject> teleportedObjects = new HashSet<GameObject>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (teleportedObjects.Contains(collision.gameObject)) return;
        try
        {
            if(connectedPortal.TryGetComponent(out Portal destinationPortal))
                destinationPortal.teleportedObjects.Add(collision.gameObject);
            collision.transform.position = connectedPortal.position;
        }
        catch (NullReferenceException ex)
        {
            Debug.LogWarning($"No attached portal. Details:{ex.Message}");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerMovement pm))
        {
            teleportedObjects.Remove(collision.gameObject);
        }
    }
}
