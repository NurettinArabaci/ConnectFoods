using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelManager : MonoSingleton<LevelManager>
{
    [SerializeField] List<GameObject> _levels = new List<GameObject>();

    const string Levels = "Levels";
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
        LoadAllLevels();
        
    }
    private void Start()
    {
        LoadLevel();
    }

    private void LoadAllLevels()
    {
        _levels = Resources.LoadAll<GameObject>(Levels).ToList();
    }

    private void OpenActiveLevel()
    {
        Instantiate(_levels[ActiveLevel], transform);
    }

    public void LoadLevel()
    {
        if (LevelAmount == 0)
        {

        }
        DestroyOtherLevels();
        OpenActiveLevel();
    }

    public void NextLevel()
    {
        LevelAmount++;
        LoadLevel();
        GameStateEvent.Fire_OnChangeGameState(GameState.Begin);
    }



    void DestroyOtherLevels()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
