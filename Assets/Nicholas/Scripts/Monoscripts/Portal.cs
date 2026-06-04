using System;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform connectedPortal;
    public int portalType;
    private HashSet<GameObject> teleportedObjects = new HashSet<GameObject>();
    private void Start()
    {
        Portal[] newPortals = FindObjectsByType<Portal>(FindObjectsSortMode.None);
        for (int i = 0; i < newPortals.Length; i++)
        {
            if(portalType == 1)
            {
                if (newPortals[i].gameObject.CompareTag("Portal2"))
                {
                    connectedPortal = newPortals[i].transform;
                    newPortals[i].connectedPortal = transform;
                }
            }
            if (portalType == 2)
            {
                if (newPortals[i].gameObject.CompareTag("Portal1"))
                {
                    connectedPortal = newPortals[i].transform;
                    newPortals[i].connectedPortal = transform;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (teleportedObjects.Contains(collision.gameObject)) return;
        try
        {
            if(connectedPortal.TryGetComponent(out Portal destinationPortal))
                destinationPortal.teleportedObjects.Add(collision.gameObject);
            collision.transform.position = connectedPortal.position;
        }
        catch (UnassignedReferenceException ex)
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
