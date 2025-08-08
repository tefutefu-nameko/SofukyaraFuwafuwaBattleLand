using UnityEngine;
using TMPro; // TextMeshProを使用するための名前空間
using UnityEngine.EventSystems;

public class ButtonColorToggle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TMP_Text buttonText; // TMP_Text型に変更
    private Color originalColor;
    public Color hoverColor = Color.white; // ホバー時の色

    void Start()
    {
        // ボタンの子オブジェクトからTMP_Textコンポーネントを取得
        buttonText = GetComponentInChildren<TMP_Text>();
        if (buttonText != null)
        {
            originalColor = buttonText.color; // 元の色を保存
        }
        else
        {
            Debug.LogError("TMP_Text component not found in children.");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            buttonText.color = hoverColor; // ホバー時に色を変更
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            buttonText.color = originalColor; // 元の色に戻す
        }
    }
}
