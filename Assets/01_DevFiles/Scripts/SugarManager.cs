using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SugarEvents
{
    public static event System.Action OnHasMove;
    public static void Fire_OnHasMove() { OnHasMove.Invoke(); }
}

public class SugarManager : MonoSingleton<SugarManager>, IPointerDownHandler, IPointerUpHandler
{
    public List<Sugar> sugarList = new List<Sugar>();
    [HideInInspector] public List<Sugar> initSugars = new List<Sugar>();

    [SerializeField] List<Sugar.SugarData> _sugarPrefabs = new List<Sugar.SugarData>();

    

    public Dictionary<Vector3, Sugar> _sugarPoseDic = new Dictionary<Vector3, Sugar>();


    [SerializeField] Sugar _mainPrefab;

    [SerializeField] int width, height;

    public int moveCount = 0;

    private Vector3 pose;
    private Vector3 _dir;

    private bool isDragging;
    private Coroutine calculateCR;
    private RaycastHit hit;
    private Sugar _selectedSugar;

    public bool HasMove
    {
        get => moveCount > 0;
    }

    private Sugar LastMemberList
    {
        get => sugarList[ListCount - 1];
    }

    private int ListCount
    {
        get => sugarList.Count;
    }

    protected override void Awake()
    {
        base.Awake();
        InstantiateSugar();
    }

    private void Start()
    {
        _dir = Vector3.back * Camera.main.transform.position.z;

        CheckHasMove();
    }

    public void CleanList()
    {
        
        initSugars.ForEach((x) => x.UnSelected());
        sugarList.Clear();
        
    }

    public void CheckListCountAndDestroy()
    {
        if (sugarList.Count < 3) return;
        int increaseNum=1;
        foreach (var item in sugarList)
        {
            _sugarPoseDic.Remove(item.transform.position);
            initSugars.Remove(item);

            item.Die();

            Sugar sugar = Instantiate(_mainPrefab, new Vector2(item.transform.position.x, height + increaseNum), Quaternion.identity, transform);
            sugar.datas = _sugarPrefabs[Random.Range(0, _sugarPrefabs.Count)];
            initSugars.Add(sugar);
            increaseNum++;

            _sugarPoseDic.Add(sugar.transform.position, sugar);

            
        }
    }


    void InstantiateSugar()
    {
        for (int x = 0; x < width; x++)
        {
            
            for (int y = 0; y < height; y++)
            {
                Sugar sugar = Instantiate(_mainPrefab, new Vector2(x, y), Quaternion.identity, transform);
                sugar.datas = _sugarPrefabs[Random.Range(0, _sugarPrefabs.Count)];
                initSugars.Add(sugar);
                _sugarPoseDic.Add(sugar.transform.position,sugar);

            }
        }
    }

    void MixedSugars()
    {
        foreach (var item in initSugars)
        {
            item.datas = _sugarPrefabs[Random.Range(0, _sugarPrefabs.Count)];
            item.UpdateDatas();
        }
    }
    

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;

        calculateCR = StartCoroutine(CalculateCR());


    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        CheckListCountAndDestroy();
        CleanList();

        CheckHasMove();

    }

    void CheckHasMove()
    {

        moveCount = 0;
        SugarEvents.Fire_OnHasMove();

        while (!HasMove)
        {
            
            MixedSugars();
            SugarEvents.Fire_OnHasMove();
        }
    }


    IEnumerator CalculateCR()
    {
        while (isDragging)
        {
            LineRendererMethod();
            if(ListCount>0)
                sugarList[ListCount - 1].SetLine(pose);
            yield return null;
        }
        calculateCR = null;
    }

    

    void LineRendererMethod()
    {

        pose = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        

        if (Physics.Raycast(pose, _dir, out hit, Mathf.Infinity))
        {
            _selectedSugar = hit.collider.GetComponent<Sugar>();


            if (ListCount > 2 && _selectedSugar == sugarList[ListCount - 2])
            {
                LastMemberList.ResetLine();
                LastMemberList.UnSelected();
                sugarList.Remove(LastMemberList);
            }

            if (sugarList.Contains(_selectedSugar))
            {
                for (int i = ListCount-1; i > sugarList.IndexOf(_selectedSugar); i--)
                {
                    sugarList[i].ResetLine();
                    sugarList[i].UnSelected();
                    sugarList.Remove(sugarList[i]);
                    
                }
                return;
            }

            if (ListCount <= 0)
            {
                sugarList.Add(_selectedSugar);
                _selectedSugar.Selected();
            }

            else
            {
                if (sugarList[0].sugarType != _selectedSugar.sugarType) return;
                if (Vector3.Distance(LastMemberList.transform.position, _selectedSugar.transform.position) > 1.5f) return;

                sugarList.Add(_selectedSugar);
                _selectedSugar.Selected();
                sugarList[ListCount - 2].SetLine(LastMemberList.transform.position);

            }

            
        }
        

    }
}
