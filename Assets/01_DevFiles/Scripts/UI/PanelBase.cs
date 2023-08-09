using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public enum PanelType
{
    Start,
    Win,
    Lose,    
    Tutorial,
    Level
}

public class PanelBase : MonoBehaviour
{
    public PanelType panelType;

    protected Button button;

    protected virtual void Awake()
    {
        button = GetComponentInChildren<Button>();

        button.onClick.AddListener(() => ButtonOnClick());
    }

    protected virtual void ButtonOnClick(){}

    protected virtual void OnEnable()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 0.15f);
    }

    public void OnResetPanel()
    {
        gameObject.SetActive(false);
    }

    public virtual void PanelActive(PanelType _panelType)
    {
        if (panelType != _panelType) return;

        gameObject.SetActive(true);
        
    }

    public virtual void PanelPassive(PanelType _panelType)
    {
        if (panelType != _panelType) return;

        transform.DOScale(Vector3.zero, 0.2f).OnComplete(() => gameObject.SetActive(false));
    }
}
