using System.Collections;
using UnityEngine;

/// <summary>
/// 敵キャラクターの共通基底クラス。
/// 既存の挙動を変えないよう、共通の参照取得と簡易ユーティリティのみを提供する。
/// </summary>
public abstract class EnemyBase : MonoBehaviour
{
    [Header("Damage Feedback")]
    public Color damageColor = new Color(1, 0, 0, 1);
    public float damageFlashDuration = 0.2f;
    public float deathFadeTime = 0.6f;

    [Header("Despawn")]
    public float despawnDistance = 20f;

    protected Transform player;
    protected SpriteRenderer spriteRenderer;
    protected Color originalColor;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer) originalColor = spriteRenderer.color;
    }

    protected virtual void Start()
    {
        PlayerStats ps = FindObjectOfType<PlayerStats>();
        if (ps) player = ps.transform;
    }

    /// <summary>プレイヤーとの距離によるデスポーン判定。</summary>
    protected bool ShouldDespawn()
    {
        return player && Vector2.Distance(transform.position, player.position) >= despawnDistance;
    }

    /// <summary>ダメージを受けた際のフラッシュ演出。</summary>
    protected IEnumerator DamageFlashCoroutine()
    {
        if (!spriteRenderer) yield break;
        spriteRenderer.color = damageColor;
        yield return new WaitForSeconds(damageFlashDuration);
        spriteRenderer.color = originalColor;
    }

    /// <summary>共通のダメージ処理入口。個別の挙動は派生側で実装。</summary>
    public abstract void TakeDamage(float damage);

    /// <summary>死亡処理。派生側で具体的な挙動を実装。</summary>
    protected abstract void Die();

    /// <summary>距離オーバー時のデスポーン。必要に応じて派生で上書き。</summary>
    protected virtual void Despawn()
    {
        gameObject.SetActive(false);
    }
}

