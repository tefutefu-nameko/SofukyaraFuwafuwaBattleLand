using UnityEngine;

/// <summary>
/// マネージャ系コンポーネントの共通基底クラス。
/// 現状の挙動を変えず、ライフサイクルの統一フックだけ提供する。
/// </summary>
public abstract class ManagerBase : MonoBehaviour
{
    protected virtual void Awake() { }
    protected virtual void OnEnable() { }
    protected virtual void OnDisable() { }
}

