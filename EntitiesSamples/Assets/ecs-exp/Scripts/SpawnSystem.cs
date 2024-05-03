using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct SpawnSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<HumanSpawner>();
    }

    // The SpawnSystem is a system that you only want to update once,
    // so in the OnUpdate, the Enabled property of the SystemState 
    // is set to false, which prevents subsequent updates of the system. 
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        state.Enabled = false;
        var prefab = SystemAPI.GetSingleton<HumanSpawner>().HumanPrefab;
        var instances = state.EntityManager.Instantiate(prefab, 5000, Allocator.Temp);
        var random = new Random(123);
        foreach (var entity in instances)
        {
            var transform = SystemAPI.GetComponentRW<LocalTransform>(entity);
            transform.ValueRW.Position = random.NextFloat3(new float3(30, 30, 30));
        }
    }
}