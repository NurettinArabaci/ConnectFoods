using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] List<PanelBase> panels = new List<PanelBase>();



    private void OnEnable()
    {
        GameStateEvent.OnBeginGame += OnResetGame;
        GameStateEvent.OnPlayGame += OnPlayGame;
        GameStateEvent.OnWinGame += OnWinGame;
        GameStateEvent.OnLoseGame += OnLoseGame;
    }

    public void OnResetGame()
    {
        foreach (var item in panels)
        {
            item.OnResetPanel();
            item.PanelActive(PanelType.Start);
            
        }
    }

    public void OnLoseGame()
    {
        foreach (var item in panels)
        {
            item.OnResetPanel();
            item.PanelActive(PanelType.Lose);
        }
    }

    private void OnWinGame()
    {
        foreach (var item in panels)
        {
            item.PanelActive(PanelType.Win);
        }
    }

    private void OnPlayGame()
    {
        foreach (var item in panels)
        {
            item.PanelPassive(PanelType.Start);
        }
    }

    private void OnDisable()
    {
        GameStateEvent.OnBeginGame -= OnResetGame;
        GameStateEvent.OnPlayGame -= OnPlayGame;
        GameStateEvent.OnWinGame -= OnWinGame;
        GameStateEvent.OnLoseGame -= OnLoseGame;
    }
}
