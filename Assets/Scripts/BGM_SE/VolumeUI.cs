using UnityEngine;
using UnityEngine.UI;

public class VolumeUI : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider seSlider;

    void Start()
    {
        // 起動時にAudioManagerの設定をスライダーに反映
        if (AudioManager.Instance != null)
        {
            bgmSlider.value = AudioManager.Instance.bgmVolume;
            seSlider.value = AudioManager.Instance.seVolume;
        }

        // スライダー操作時のイベント登録
        // まだ設定されていない場合のみ追加（重複登録防止はInspector設定次第だが念のため）
        bgmSlider.onValueChanged.RemoveAllListeners();
        seSlider.onValueChanged.RemoveAllListeners();

        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        seSlider.onValueChanged.AddListener(OnSEVolumeChanged);
    }

    public void OnBGMVolumeChanged(float value)
    {
        AudioManager.Instance.SetBGMVolume(value);
    }

    public void OnSEVolumeChanged(float value)
    {
        AudioManager.Instance.SetSEVolume(value);
    }
}