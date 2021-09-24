using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private static EntityManager entityManager;
    private static Entity cubeEntity;

    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private int numberOfCubes;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveFrequency;
    [SerializeField] private float moveMagnitude;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
        // Get entity manager instance
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        // Get world settings
        GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);

        // Convert cube prefab into an entity
        cubeEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(cubePrefab, settings);

        CreateCubes();
    }

    private void CreateCubes()
    {
        NativeArray<Entity> cubeArray = new NativeArray<Entity>(numberOfCubes, Allocator.Temp);
        entityManager.Instantiate(cubeEntity, cubeArray);

        for (int i = 0; i < numberOfCubes; i++)
        {
            // Make a line of cubes
            float3 position = new float3(-numberOfCubes / 2 + i + 0.5f, 0, 0);

            Entity cubeInstance = cubeArray[i];

            entityManager.SetComponentData(cubeInstance, new Translation { Value = position });
            entityManager.SetComponentData(cubeInstance, new CubeComponent
            {
                moveSpeed = moveSpeed,
                moveFrequency = moveFrequency,
                moveMagnitude = moveMagnitude
            });
        }

        cubeArray.Dispose();
    }
}
