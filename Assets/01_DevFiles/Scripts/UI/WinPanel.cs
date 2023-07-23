using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPanel : PanelBase
{
    protected override void ButtonOnClick()
    {
        LevelManager.Instance.NextLevel();
    }
}
