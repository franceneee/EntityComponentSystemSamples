using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

// ISystem does not have to be added to the scene.
public partial struct RotationSystem : ISystem
{
    [BurstCompile] // BurstCompile attribute marks the OnUpdate to be Burst compiled, which is valid as long as the OnUpdate does not access any managed objects 
    // invoked once every frame
    public void OnUpdate(ref SystemState state)
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        // Loop over every entity having a LocalTransform component and RotationSpeed component.
        // In each iteration, transform is assigned a read-write reference to the LocalTransform,
        // and speed is assigned a read-only reference to the RotationSpeed component.
        foreach (var (transform, speed) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotationSpeed>>())
        {
            // ValueRW and ValueRO both return a ref to the actual component value.
            // The difference is that ValueRW does a safety check for read-write access while
            // ValueRO does a safety check for read-only access.
            transform.ValueRW = transform.ValueRO.RotateY(speed.ValueRO.RadiansPerSecond * deltaTime);
        }
    }
}