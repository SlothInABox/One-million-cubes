using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

public class MoveCubeJobSystem : JobComponentSystem
{
    // Use burst compiler automatically
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float time = (float)Time.ElapsedTime;
        GameManager gameManager = GameManager.Instance;
        // Get current frequency and magnitude from GameManager. These values shouldn't change when built but they are left editable for testing.
        float frequency = gameManager.moveFrequency;
        float amplitude = gameManager.moveMagnitude;

        // Iterate over all cube entities
        JobHandle jobHandle = Entities.ForEach((ref Translation translation, in CubeComponent cubeComponent) =>
        {
            // Change y coordinate of cube entity to make cubes follow sinusoidal standing wave pattern
            translation.Value.y = Mathf.Sin(translation.Value.x + frequency * time) * amplitude;
        }).Schedule(inputDeps);

        return jobHandle;
    }
}
