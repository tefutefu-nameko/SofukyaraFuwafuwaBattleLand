using System;
using System.Collections.Generic;

[System.Serializable]
public class RankingEntry
{
    public string characterName;
    public float clearTime;

    public RankingEntry(string name, float time)
    {
        characterName = name;
        clearTime = time;
    }
}

[System.Serializable]
public class RankingData
{
    public List<RankingEntry> entries = new List<RankingEntry>();
}