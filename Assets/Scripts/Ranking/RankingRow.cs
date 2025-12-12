using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankingRow : MonoBehaviour
{
    [SerializeField] TMP_Text rankText;
    [SerializeField] TMP_Text timeText;
    [SerializeField] Image icon;

    public void Set(int rank, float clearTime, Sprite sprite)
    {
        rankText.text = rank.ToString();
        int minutes = Mathf.FloorToInt(clearTime / 60);
        int seconds = Mathf.FloorToInt(clearTime % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        icon.sprite = sprite;
    }
}