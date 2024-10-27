using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image targetImage; // �\��������Image�R���|�[�l���g

    private void Start()
    {
        if (targetImage != null)
        {
            targetImage.gameObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (targetImage != null)
        {
            targetImage.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (targetImage != null)
        {
            targetImage.gameObject.SetActive(false);
        }
    }
}
