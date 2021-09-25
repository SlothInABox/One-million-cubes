using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;
using Unity.Jobs;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;
    // Number of cubes on each side of the grid (1000 width = 1000000 cubes).
    [SerializeField] private int gridWidth;
    // Amount of space between cubes
    [SerializeField] private float cubeSpacing;

    // initialize store for entity and entity manager.
    private Entity cubeEntityPrefab;
    private EntityManager entityManager;

    private void Awake()
    {
        // Get current entity manager from active world.
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get game object conversion settings from active world, don't use a blob.
        GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        // Convert cube prefab into a cube entity prefab.
        cubeEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(cubePrefab, settings);

        InstantiateCubeGrid();
    }

    // Member function for instantiating a new cube entity.
    private void InstantiateCube(in float3 position)
    {
        Entity cubeEntity = entityManager.Instantiate(cubeEntityPrefab);
        // Set position of the cube.
        entityManager.SetComponentData(cubeEntity, new Translation
        {
            Value = position
        });
    }

    // Member function for creating a grid of cube entities in the scene
    private void InstantiateCubeGrid()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridWidth; z++)
            {
                // Calculate the cube position from it's spacing and indexes.
                InstantiateCube(new float3(x * cubeSpacing, 0f, z * cubeSpacing));
            }
        }
    }
}
