using UnityEngine;

namespace Game.LoadScene
{
    public enum SceneId
    {
        Menu = 0,
        MenuAuto = 1,
        Game = 2
    }

    public class SceneReference : MonoBehaviour
    {
        public SceneId SceneId;

        public string GetTitle() => SceneId.ToString();
    }
}