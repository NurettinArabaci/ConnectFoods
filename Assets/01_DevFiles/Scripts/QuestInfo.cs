using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestInfo : MonoBehaviour
{
    [SerializeField] Image icon;
    TextMeshProUGUI text;
    int sugarAmount;

    private void Awake()
    {
        icon = GetComponentInChildren<Image>();

    }

    public void InitQuest(Quest quest)
    {
        //Sugar.SugarData _data = new Sugar.SugarData(SugarManager.Instance._sugarPrefabs[0].type, out Sprite _sprite);
        //icon.sprite = _sprite;
    }

}
