using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LosePanel : PanelBase
{
    private void Start()
    {
        panelType = PanelType.Lose;
    }

    protected override void ButtonOnClick()
    {
        //PlayerManager.Instance.StartRecording();
        PanelPassive(PanelType.Lose);
    }
}
