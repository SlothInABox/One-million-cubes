using Unity.Entities;

public struct CubeComponent : IComponentData
{
    public float moveSpeed;
    public float moveFrequency;
    public float moveMagnitude;
}
