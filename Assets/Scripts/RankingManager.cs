using UnityEngine;

public class RankingManager : ManagerBase
{
    private const string SaveKey = "RankingData";
    public RankingData rankingData = new RankingData();

    protected override void Awake()
    {
        base.Awake();
        Load();

    }

    public void AddScore(string characterName, float clearTime)
    {
        rankingData.entries.Add(new RankingEntry(characterName, clearTime));

        // タイム昇順でソート
        rankingData.entries.Sort((a, b) => a.clearTime.CompareTo(b.clearTime));

        // 上位10件に制限
        if (rankingData.entries.Count > 10)
        {
            rankingData.entries.RemoveRange(10, rankingData.entries.Count - 10);
        }

        Save();
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(rankingData);
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(SaveKey))
        {
            string json = PlayerPrefs.GetString(SaveKey);
            rankingData = JsonUtility.FromJson<RankingData>(json);
        }
        else
        {
            rankingData = new RankingData();
        }
    }

    public void ClearRanking()
    {
        PlayerPrefs.DeleteKey(SaveKey);
        PlayerPrefs.Save();
        rankingData = new RankingData();
    }
}