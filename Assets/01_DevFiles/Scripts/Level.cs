using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _levelNum;

    private void Start()
    {
        _levelNum.SetText($"Level: {_data._levelNumber}");
    }

    public LevelData _data;
}
[System.Serializable]
public class LevelData
{
    public bool _isLocked;
    public int _levelNumber;
    public int _totalMove;
    public int _row;
    public int _column;

}