using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SugarType
{
    Orange,
    Strawberry,
    Lemon,
    Lime,
    Blueberry
}

public class Sugar : MonoBehaviour
{
    [Serializable]
    public class SugarData
    {
        public Sprite sprite;
        public SugarType type;
    }

    public SugarData datas;

    public SugarType sugarType => datas.type;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private SpriteRenderer _frame;
    [SerializeField] private GameObject _explodeVFX;
    private Animation anim;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        anim = GetComponent<Animation>();
        _frame.enabled = false;
    }

    LineRenderer line;
    Collider mColl=> GetComponent<Collider>();

    private void OnEnable()
    {
        SugarEvents.OnHasMove += CheckNeighbours;
    }

    private void OnDisable()
    {
        SugarEvents.OnHasMove -= CheckNeighbours;
    }

    public void UpdateDatas()
    {
        _spriteRenderer.sprite = datas.sprite;
    }

    void CheckNeighbours()
    {
        int neighbourCount = 0;

        Collider[] neighbours = Physics.OverlapSphere(transform.position, 1.5f);

        
        foreach (var item in neighbours)
        {
            
            if (item == mColl) continue;
            if (item.GetComponent<Sugar>().sugarType == sugarType)
                neighbourCount++;

            
        }
        if (neighbourCount >= 2)
        {
            SugarManager.Instance.moveCount++;
        }

        MoveDown();

    }

    public void Die()
    {
        UnSelected();

        Instantiate(_explodeVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void Selected()
    {
        _frame.enabled = true;
        anim.Play();
    }

    public void UnSelected()
    {
        _frame.enabled = false;
        anim.Stop();
        ResetLine();
    }

    public void MoveDown()
    {
        if (transform.position.y <= 0) return;

        

        if (!SugarManager.Instance._sugarPoseDic.ContainsKey(transform.position + Vector3.down))
        {
            if (SugarManager.Instance._sugarPoseDic.ContainsKey(transform.position + Vector3.up))
                SugarManager.Instance._sugarPoseDic[transform.position + Vector3.up].MoveDown();
            StartCoroutine(MoveDownCR());
            
        }
        
    }

    public IEnumerator MoveDownCR()
    {
        SugarManager.Instance._sugarPoseDic.Remove(transform.position);
        
        float curPos = transform.position.y;
        float targetPos = curPos-1;
        
        while (transform.position.y-targetPos>0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.down, Time.deltaTime * 20);
            ResetLine();

            yield return null;
        }
        
        transform.position = new Vector3(transform.position.x, Mathf.Round(transform.position.y), transform.position.z);
        SugarManager.Instance._sugarPoseDic.Add(transform.position, this);

        MoveDown();


    }

    public void SetLine( Vector3 pos)
    {
        line.SetPosition(0, (Vector2)transform.position);
        line.SetPosition(1, (Vector2)pos);
    }
    

    private void Start()
    {

        UpdateDatas();
    }

    public void ResetLine()
    {
        line.SetPosition(0, (Vector2)transform.position);
        line.SetPosition(1, (Vector2)transform.position);
    }
}
