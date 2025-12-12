using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RankingUI : MonoBehaviour
{
    [SerializeField] private GameObject headerPrefab; // ヘッダー用プレハブ
    [SerializeField] private GameObject rankingRowPrefab;
    [SerializeField] private Transform contentParent; // ScrollViewのContent
    [SerializeField] private RankingManager rankingManager;
    [SerializeField] private ScrollRect scrollRect;

    // CharacterData を直接参照できるようにする（ResourcesやScriptableObjectから検索）
    [SerializeField] private List<CharacterData> characterList;

    private void OnEnable()
    {
        RefreshUI();

        // スクロール位置をリセット（上端に戻す）
        if (scrollRect != null)
        {
            Canvas.ForceUpdateCanvases(); // レイアウト更新を強制
            scrollRect.verticalNormalizedPosition = 1f;
        }
    }

    public void RefreshUI()
    {
        // 既存の行を削除
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // ヘッダーを最初に生成
        if (headerPrefab != null)
        {
            Instantiate(headerPrefab, contentParent);
        }

        List<RankingEntry> entries = rankingManager.rankingData.entries;

        for (int i = 0; i < entries.Count; i++)
        {
            GameObject rowObj = Instantiate(rankingRowPrefab, contentParent);
            RankingRow row = rowObj.GetComponent<RankingRow>();

            CharacterData charData = characterList.Find(c => c.Name == entries[i].characterName);

            Sprite sprite = charData != null ? charData.Icon : null;

            row.Set(i + 1, entries[i].clearTime, sprite);
        }

        // 下に余白を追加
        AddBottomSpacer();

    }

    private void AddBottomSpacer()
    {
        GameObject spacer = new GameObject("Spacer", typeof(RectTransform));
        spacer.transform.SetParent(contentParent);

        RectTransform rt = spacer.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(0, 200); // 200pxくらい余白
    }


    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}