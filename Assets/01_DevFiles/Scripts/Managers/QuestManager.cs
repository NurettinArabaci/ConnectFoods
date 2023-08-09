using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoSingleton<QuestManager>
{

    public List<Quest> _quests= new List<Quest>();

    [SerializeField] QuestInfo infoPrefab;
    [SerializeField] Transform infoParent;


    public void InitQuest(List<Quest> quest)
    {
        _quests = quest;

        for (int i = 0; i < _quests.Count; i++)
        {
            QuestInfo _info = Instantiate(infoPrefab, infoParent);
            _info.InitQuest(_quests[i]);

        }
    }

    public void DecreaseQuest(SugarType type, int decreaseNum)
    {
       
        for (int i = 0; i < _quests.Count; i++)
        {
            if (_quests[i].SugarType == type)
            {
                _quests[i].sugarAmount -= decreaseNum;
                if (_quests[i].sugarAmount <= 0)
                    _quests.Remove(_quests[i]);
                if (_quests.Count <= 0)
                    LevelManager.Instance.OpenLevelPanel();
            }
        }
        
    }


}