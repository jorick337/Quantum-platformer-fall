namespace Quantum.Health
{
    public unsafe class HealthSystem : SystemMainThreadFilter<HealthSystem.Filter>
    {
        public struct Filter
        {
            public EntityRef Entity;
            public HealthComponent* Health;
        }

        public override void Update(Frame f, ref Filter filter)
        {
            if (!filter.Health->IsInitialize)
            {
                var config = f.FindAsset<HealthConfig>(filter.Health->Config.Id);
                f.Events.OnHealthInitialized((int)config.CurrentHealth);
                filter.Health->IsInitialize = true;
            }
        }
    }
}