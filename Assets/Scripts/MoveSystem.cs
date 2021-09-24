using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;

//public class MoveSystem : ComponentSystem
//{
//    protected override void OnUpdate()
//    {
//        Entities.ForEach((ref Translation translation, ref CubeComponent cubeComponent) =>
//        {
//            translation.Value.x += cubeComponent.moveSpeed * Time.DeltaTime;
//            translation.Value.y = Mathf.Sin(translation.Value.x * cubeComponent.moveFrequency) * cubeComponent.moveMagnitude;
//        });
//    }
//}
