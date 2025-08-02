using System.Globalization;
using System.Linq;
using Quantum;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Health.UI
{
    public class HealthCounter : MonoBehaviour
    {
        private DispatcherSubscription _quantumOnInitializeHealthEvent;
        private DispatcherSubscription _quantumOnChangedHealthEvent;

        [SerializeField] private Text _text;

        private void Awake()
        {
            var game = QuantumRunner.Default?.Game;
            if (game == null)
                return;

            var frame = game.Frames.Predicted;

            foreach (var item in frame.GetComponentIterator<HealthComponent>())
            {
                var health = item.Component.CurrentHealth;
                SetText(health.ToString());
            }
        }

        private void OnEnable()
        {
            _quantumOnInitializeHealthEvent = QuantumEvent.Subscribe<EventOnHealthInitialized>(this, OnHealthInitialize);
            _quantumOnChangedHealthEvent = QuantumEvent.Subscribe<EventOnHealthChanged>(this, OnHealthChanged);
        }

        private void OnDisable()
        {
            QuantumEvent.UnsubscribeListener(_quantumOnChangedHealthEvent);
            QuantumEvent.UnsubscribeListener(_quantumOnInitializeHealthEvent);
        }

        private void SetText(string text)
        {
            if (_text != null)
                _text.text = text;
        }

        private void OnHealthChanged(EventOnHealthChanged e) => SetText(e.newValue.ToString());
        private void OnHealthInitialize(EventOnHealthInitialized e) => SetText(e.value.ToString());
    }
}