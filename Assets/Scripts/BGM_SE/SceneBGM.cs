using UnityEngine;

public class SceneBGM : MonoBehaviour
{
    [SerializeField] private AudioClip bgmClip;

    void Start()
    {
        if (AudioManager.Instance != null && bgmClip != null)
        {
            AudioManager.Instance.PlayBGM(bgmClip);
        }
    }
}
