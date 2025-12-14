using UnityEngine;

namespace Utils
{
    /// <summary>
    /// Disables the Renderer (Sprite or Mesh) of this object when the game starts.
    /// Useful for placing visual objects in the Editor (e.g. collision boxes) that should be invisible in-game.
    /// </summary>
    public class HideInGame : MonoBehaviour
    {
        void Start()
        {
            Renderer r = GetComponent<Renderer>();
            if (r != null)
            {
                r.enabled = false;
            }
        }
    }
}
