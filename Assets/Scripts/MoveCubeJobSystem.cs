using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

public class MoveCubeJobSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float time = (float)Time.ElapsedTime;

        // Iterate over all cube entities
        Entities.ForEach((ref Translation translation, in WaveDataComponent waveData) =>
        {
            // Change y coordinate of cube entity to make cubes follow sinusoidal standing wave pattern
            translation.Value.y = Mathf.Sin(translation.Value.x + waveData.frequency * time) * waveData.amplitude;
        }).ScheduleParallel();
    }
}
