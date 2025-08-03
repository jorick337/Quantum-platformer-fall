using Quantum;
using UnityEngine;

namespace Game.Health.UI
{
    public class GameOver : MonoBehaviour
    {
        private DispatcherSubscription _quantumOnHealthDeadEvent;

        [SerializeField] private CanvasGroup _canvasGroup;

        private void OnEnable() => _quantumOnHealthDeadEvent = QuantumEvent.Subscribe<EventOnHealthDead>(this, ShowGameOver);
        private void OnDisable() => QuantumEvent.Unsubscribe(_quantumOnHealthDeadEvent);

        private void SetActiveCanvasGroup(bool active)
        {
            _canvasGroup.alpha = active ? 1f : 0f;
            _canvasGroup.blocksRaycasts = active;
            _canvasGroup.interactable = active;
        }

        private void ShowGameOver(EventOnHealthDead e) => SetActiveCanvasGroup(true);
    }
}