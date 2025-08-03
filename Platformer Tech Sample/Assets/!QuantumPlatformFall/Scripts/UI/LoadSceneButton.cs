using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.LoadScene.UI
{
    [RequireComponent(typeof(SceneReference))]
    public class LoadSceneButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private SceneReference _sceneReference;

        private void OnEnable() => _button.onClick.AddListener(Load);
        private void OnDisable() => _button.onClick.RemoveListener(Load);

        private void Load() 
        {
            QuantumRunner.Default?.Shutdown();
            SceneManager.LoadScene(_sceneReference.GetTitle());
        }
    }
}