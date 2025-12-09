using UnityEngine;

/// <summary>
/// UI系コンポーネントの共通基底クラス。
/// 既存の挙動を変えないよう、ライフサイクルフックと表示制御のユーティリティのみを提供する。
/// </summary>
public abstract class UIViewBase : MonoBehaviour
{
    protected virtual void Awake() { }
    protected virtual void OnEnable() { }
    protected virtual void OnDisable() { }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetVisible(bool visible)
    {
        if (visible) Show();
        else Hide();
    }
}

