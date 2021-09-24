using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class Cube : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponent(entity, typeof(CubeComponent));
        dstManager.AddComponent(entity, typeof(MoveSpeedComponent));
    }
}