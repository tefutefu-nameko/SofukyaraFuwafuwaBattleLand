using System.Collections;
using UnityEngine;
using TMPro;

/// <summary>
/// ダメージフローティングテキストを生成するサービス。
/// </summary>
public class DamageTextService : ManagerBase, IDamageTextService
{
    [Header("Damage Text Settings")]
    [SerializeField] Canvas damageTextCanvas;
    [SerializeField] float textFontSize = 20;
    [SerializeField] TMP_FontAsset textFont;
    [SerializeField] Camera referenceCamera;

    public void GenerateFloatingText(string text, Transform target, float duration = 1f, float speed = 1f)
    {
        if (!damageTextCanvas || !target) return;
        if (!referenceCamera) referenceCamera = Camera.main;
        StartCoroutine(GenerateFloatingTextCoroutine(text, target, duration, speed));
    }

    IEnumerator GenerateFloatingTextCoroutine(string text, Transform target, float duration, float speed)
    {
        GameObject textObj = new GameObject("Damage Floating Text");
        RectTransform rect = textObj.AddComponent<RectTransform>();
        TextMeshProUGUI tmPro = textObj.AddComponent<TextMeshProUGUI>();
        tmPro.text = text;
        tmPro.horizontalAlignment = HorizontalAlignmentOptions.Center;
        tmPro.verticalAlignment = VerticalAlignmentOptions.Middle;
        tmPro.fontSize = textFontSize;
        if (textFont) tmPro.font = textFont;
        rect.position = referenceCamera.WorldToScreenPoint(target.position);

        Destroy(textObj, duration);

        textObj.transform.SetParent(damageTextCanvas.transform);
        textObj.transform.SetSiblingIndex(0);

        WaitForEndOfFrame w = new WaitForEndOfFrame();
        float t = 0;
        float yOffset = 0;
        Vector3 lastKnownPosition = target.position;
        while (t < duration)
        {
            if (!rect) break;

            tmPro.color = new Color(tmPro.color.r, tmPro.color.g, tmPro.color.b, 1 - t / duration);

            if (target) lastKnownPosition = target.position;

            yOffset += speed * Time.deltaTime;
            rect.position = referenceCamera.WorldToScreenPoint(lastKnownPosition + new Vector3(0, yOffset));

            yield return w;
            t += Time.deltaTime;
        }
    }
}

