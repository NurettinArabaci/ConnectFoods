using System.Collections.Generic;

[System.Serializable]
public struct LevelData
{
    public bool _isLocked;
    public int _levelNumber;
    public int _totalMove;
    public List<Quest> quests;

}
