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
    public static Entity cubeEntity;

    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Mesh cubeMesh;
    [SerializeField] private Material cubeMaterial;

    public float moveFrequency;
    public float moveMagnitude;
    public int waveWidth;

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
        //cubeEntity = MakeEntity();
        CreateCubes();
    }

    private void CreateCubes()
    {
        // Total number of cubes
        int numberOfCubes = waveWidth * waveWidth;

        // Create cube entity instances
        NativeArray<Entity> cubeArray = new NativeArray<Entity>(numberOfCubes, Allocator.Temp);
        entityManager.Instantiate(cubeEntity, cubeArray);

        // Position each cube in grid
        for (int x = 0; x < waveWidth; x++)
        {
            for (int z = 0; z < waveWidth; z++)
            {
                // Cubes are separated by a gap of size 0.5
                float3 position = new float3(-waveWidth + x * 1.5f, 0, z * 1.5f);

                // Access proper cube entity
                Entity cubeInstance = cubeArray[x * waveWidth + z];
                // Set cube translation component
                entityManager.SetComponentData(cubeInstance, new Translation { Value = position });
            }
        }
        cubeArray.Dispose();
    }

    private Entity MakeEntity()
    {
        EntityArchetype cubeArchetype = entityManager.CreateArchetype(
            typeof(Translation),
            typeof(Rotation),
            typeof(RenderMesh),
            typeof(RenderBounds),
            typeof(LocalToWorld),
            typeof(CubeComponent)
            );

        Entity cubeEntity = entityManager.CreateEntity(cubeArchetype);

        entityManager.AddSharedComponentData(cubeEntity, new RenderMesh
        {
            mesh = cubeMesh,
            material = cubeMaterial
        });

       return cubeEntity;
    }
}
