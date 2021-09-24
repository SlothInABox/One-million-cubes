using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class Testing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Get the entity manager
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        // Create a basic entity
        entityManager.CreateEntity();
    }
}
