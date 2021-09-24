using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Burst;

public class MoveCubeJobSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;

        JobHandle jobHandle = Entities.ForEach((ref Translation translation, in CubeComponent cubeComponent) =>
        {
            translation.Value.x += cubeComponent.moveSpeed * deltaTime;
            translation.Value.y = Mathf.Sin(translation.Value.x * cubeComponent.moveFrequency) * cubeComponent.moveMagnitude;
        }).Schedule(inputDeps);

        return jobHandle;
    }
}
