using Photon.Deterministic;

namespace Quantum
{
    public partial class HealthConfig
    {
        public FP MaxHealth = 100;
        public FP CurrentHealth = 100;
        public bool IsDead = false;
    }
}