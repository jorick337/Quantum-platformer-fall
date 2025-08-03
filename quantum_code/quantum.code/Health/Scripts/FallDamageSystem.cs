using Photon.Deterministic;

namespace Quantum
{
    public unsafe class FallDamageSystem : SystemMainThreadFilter<FallDamageSystem.Filter>
    {
        public struct Filter
        {
            public EntityRef Entity;
            public Transform3D* Transform;
            public CharacterController3D* CharacterController;
            public HealthComponent* Health;
            public FallStateComponent* FallState;
        }

        public override void Update(Frame f, ref Filter filter)
        {
            FP currentY = filter.Transform->Position.Y;

            if (!filter.FallState->IsFalling && !filter.CharacterController->Grounded)
            {
                StartFalling(ref filter, currentY);
            }
            else if (filter.FallState->IsFalling && filter.CharacterController->Grounded && filter.Health->CurrentHealth > 0)
            {
                ProcessFallDamage(f, ref filter, currentY);
            }
        }

        private void StartFalling(ref Filter filter, FP currentY)
        {
            filter.FallState->IsFalling = true;
            filter.FallState->StartY = currentY;
        }

        private void ProcessFallDamage(Frame f, ref Filter filter, FP currentY)
        {
            FallStateConfig config = f.FindAsset<FallStateConfig>(filter.FallState->Config.Id);

            FP minFallHeight = config.MinHeight;
            FP fallDamage = config.Damage;

            FP fallDistance = filter.FallState->StartY - currentY;

            if (fallDistance > minFallHeight)
            {
                filter.Health->CurrentHealth -= fallDamage;
            }

            filter.FallState->IsFalling = false;
        }
    }
}