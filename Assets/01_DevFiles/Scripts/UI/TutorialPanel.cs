using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanel : PanelBase
{
    protected override void Awake()
    {
        GameStateEvent.OnBeginGame += GameStateEvent_OnBeginGame;
        GameStateEvent.OnPlayGame += GameStateEvent_OnPlayGame;       
    }

    private void GameStateEvent_OnPlayGame()
    {
        PanelPassive(PanelType.Tutorial);
    }

    private void GameStateEvent_OnBeginGame()
    {
        if (LevelManager.Instance.LevelAmount == 0)
        {
            PanelActive(PanelType.Tutorial);
            return;
        }
        PanelPassive(PanelType.Tutorial);
    }

    private void OnDisable()
    {
        GameStateEvent.OnBeginGame -= GameStateEvent_OnBeginGame;
        GameStateEvent.OnPlayGame -= GameStateEvent_OnPlayGame;
    }
}
