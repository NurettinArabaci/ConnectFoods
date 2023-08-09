using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelManager : MonoSingleton<LevelManager>
{
    [SerializeField] List<GameObject> _levels = new List<GameObject>();

    [SerializeField] Level _levelPrefab;

    [SerializeField] public List<LevelData> levels = new List<LevelData>();
    [SerializeField] Transform _levelParent;
    PanelBase panelBase;

    

    const string Level = "Level";

    public int LevelAmount
    {
        get { return PlayerPrefs.GetInt(Level, 0); }
        set { PlayerPrefs.SetInt(Level, value); }
    }


    int ActiveLevel
    {
        get
        {
            return LevelAmount % _levels.Count;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        panelBase=GetComponentInChildren<PanelBase>();


    }
    private void Start()
    {
        for (int i = 0; i < levels.Count; i++)
        {
            Level _level = Instantiate(_levelPrefab, _levelParent);

            _level._data = levels[i];
        }
    }
    private void OpenActiveLevel()
    {
        Instantiate(_levels[ActiveLevel], transform);
    }

    public void OpenLevelPanel()
    {
        panelBase.PanelActive(PanelType.Level);
    }

    public void CloseLevelPanel()
    {
        panelBase.PanelPassive(PanelType.Level);
    }

    public void NextLevel()
    {
        LevelAmount++;
        GameStateEvent.Fire_OnChangeGameState(GameState.Begin);
    }
}
