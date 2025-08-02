using Photon.Deterministic;

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
                Initialize(f, ref filter);
            }

            if (!filter.Health->IsDead && filter.Health->CurrentHealth <= FP._0)
            {
                Die(f, ref filter);
            }
            
            if (filter.Health->PreviousHealth != filter.Health->CurrentHealth)
            {
                Change(f, ref filter);
            }
        }

        private void Initialize(Frame f, ref Filter filter)
        {
            var config = f.FindAsset<HealthConfig>(filter.Health->Config.Id);
            filter.Health->PreviousHealth = config.MaxHealth;
            filter.Health->CurrentHealth = config.MaxHealth;

            f.Events.OnHealthInitialized((int)filter.Health->CurrentHealth);
            filter.Health->IsInitialize = true;
        }

        private void Die(Frame f, ref Filter filter)
        {
            filter.Health->IsDead = true;
            filter.Health->CurrentHealth = 0;
            f.Events.OnHealthDead();

            f.Destroy(filter.Entity);
        }

        private void Change(Frame f, ref Filter filter)
        {
            int lastValue = (int)filter.Health->PreviousHealth;
            int newValue = (int)filter.Health->CurrentHealth;

            f.Events.OnHealthChanged(lastValue, newValue);
            filter.Health->PreviousHealth = newValue;
        }
    }
}