using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Rendering;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private static EntityManager entityManager;
    private static Entity cubeEntity;

    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private int numberOfCubes;
    [SerializeField] private Vector3 positionRange;

    public static GameManager GetInstance()
    {
        return instance;
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
            Vector3 position = new Vector3(
                Random.Range(-positionRange.x, positionRange.x),
                Random.Range(-positionRange.y, positionRange.y),
                Random.Range(-positionRange.z, positionRange.z));

            Entity cubeInstance = cubeArray[i];

            entityManager.SetComponentData(cubeInstance, new Translation { Value = position });
        }

        cubeArray.Dispose();
    }
}
