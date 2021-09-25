using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

[GenerateAuthoringComponent]
public struct WaveDataComponent : IComponentData
{
    public float amplitude;
    public float frequency;
}
