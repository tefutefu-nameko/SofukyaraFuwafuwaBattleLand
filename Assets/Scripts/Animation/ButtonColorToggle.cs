using UnityEngine;
using TMPro; // TextMeshPro���g�p���邽�߂̖��O���
using UnityEngine.EventSystems;

public class ButtonColorToggle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TMP_Text buttonText; // TMP_Text�^�ɕύX
    private Color originalColor;
    public Color hoverColor = Color.white; // �z�o�[���̐F

    void Start()
    {
        // �{�^���̎q�I�u�W�F�N�g����TMP_Text�R���|�[�l���g���擾
        buttonText = GetComponentInChildren<TMP_Text>();
        if (buttonText != null)
        {
            originalColor = buttonText.color; // ���̐F��ۑ�
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
            buttonText.color = hoverColor; // �z�o�[���ɐF��ύX
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            buttonText.color = originalColor; // ���̐F�ɖ߂�
        }
    }
}
