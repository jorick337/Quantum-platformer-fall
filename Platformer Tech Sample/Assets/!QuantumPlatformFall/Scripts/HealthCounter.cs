using Quantum;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Health
{
    public class HealthCounter : MonoBehaviour
    {
        [SerializeField] private Text _text;

        private DispatcherSubscription _quantumOnInitializeHealthEvent;
        private DispatcherSubscription _quantumOnChangedHealthEvent;

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