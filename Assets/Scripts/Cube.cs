using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class Cube : MonoBehaviour, IConvertGameObjectToEntity
{
    // Use to convert cube prefab into a cube entity
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        // Empty component used for selecting only cube entities
        dstManager.AddComponent(entity, typeof(CubeComponent));
    }
}