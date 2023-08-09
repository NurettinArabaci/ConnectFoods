using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _levelNum;
    public LevelData _data;

    private void Start()
    {
        _levelNum.SetText($"Level: {_data._levelNumber}");

        GetComponent<Button>().onClick.AddListener(() =>
        {
            QuestManager.Instance.InitQuest(_data.quests);
            LevelManager.Instance.CloseLevelPanel();
        });

        GetComponent<Button>().interactable = !_data._isLocked;
    }



}