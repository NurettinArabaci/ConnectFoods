using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanel : PanelBase
{
    protected override void ButtonOnClick()
    {
        GameStateEvent.Fire_OnChangeGameState(GameState.Play);

    }
}
