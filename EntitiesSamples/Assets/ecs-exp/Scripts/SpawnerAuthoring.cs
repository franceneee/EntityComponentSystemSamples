using Unity.Entities;
using UnityEngine;

public class SpawnerAuthoring : MonoBehaviour
{
    public GameObject CubePrefab;

    class Baker : Baker<SpawnerAuthoring>
    {
        public override void Bake(SpawnerAuthoring authoring)
        {
            var entity = GetEntity(authoring, TransformUsageFlags.None);
            // Because the spawner entity won’t be visible, it doesn’t need any Transform components, 
            // so TransformUsageFlags.None is specified in the first GetEntity call
            var spawner = new HumanSpawner
            {
                HumanPrefab = GetEntity(authoring.CubePrefab, TransformUsageFlags.Dynamic)
            };
            AddComponent(entity, spawner);
        }
    }
}

partial struct HumanSpawner : IComponentData
{
    public Entity HumanPrefab;
}