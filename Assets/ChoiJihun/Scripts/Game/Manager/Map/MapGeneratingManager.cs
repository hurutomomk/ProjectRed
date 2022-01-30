using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class MapGeneratingManager : MonoBehaviour
{
    #region [var]

    #region [01. instance]
    /// <summary>
    /// インスタンス
    /// </summary>
    public static MapGeneratingManager Instance { get; private set; }
    #endregion
    
    #region [02. reference]
    
    #endregion
    
    #region [03. map data]
    /// <summary>
    /// MapList : Start
    /// </summary>
    [SerializeField] 
    private List<GameObject> mapStart = new List<GameObject>();
    public List<GameObject> MapStart
    {
        get => mapStart;
    } 
    
    [SerializeField] 
    private List<GameObject> mapTypeN = new List<GameObject>();
    public List<GameObject> MapTypeN
    {
        get => mapTypeN;
    } 
    [SerializeField] 
    private List<GameObject> mapTypeE = new List<GameObject>();
    public List<GameObject> MapTypeE
    {
        get => mapTypeE;
    } 
    [SerializeField] 
    private List<GameObject> mapTypeS = new List<GameObject>();
    public List<GameObject> MapTypeS
    {
        get => mapTypeS;
    } 
    [SerializeField] 
    private List<GameObject> mapTypeW = new List<GameObject>();
    public List<GameObject> MapTypeW
    {
        get => mapTypeW;
    } 
    [SerializeField] 
    private List<GameObject> mapTypeNE = new List<GameObject>();
    public List<GameObject> MapTypeNE
    {
        get => mapTypeNE;
    } [SerializeField] 
    private List<GameObject> mapTypeNS = new List<GameObject>();
    public List<GameObject> MapTypeNS
    {
        get => mapTypeNS;
    } [SerializeField] 
    private List<GameObject> mapTypeNW = new List<GameObject>();
    public List<GameObject> MapTypeNW
    {
        get => mapTypeNW;
    } 
    [SerializeField] 
    private List<GameObject> mapTypeES = new List<GameObject>();
    public List<GameObject> MapTypeES
    {
        get => mapTypeES;
    } 
    [SerializeField] 
    private List<GameObject> mapTypeEW = new List<GameObject>();
    public List<GameObject> MapTypeEW
    {
        get => mapTypeEW;
    } [SerializeField] 
    private List<GameObject> mapTypeSW = new List<GameObject>();
    public List<GameObject> MapTypeSW
    {
        get => mapTypeSW;
    } [SerializeField] 
    private List<GameObject> mapTypeNES = new List<GameObject>();
    public List<GameObject> MapTypeNES
    {
        get => mapTypeNES;
    } [SerializeField] 
    private List<GameObject> mapTypeNEW = new List<GameObject>();
    public List<GameObject> MapTypeNEW
    {
        get => mapTypeNEW;
    } [SerializeField] 
    private List<GameObject> mapTypeNSW = new List<GameObject>();
    public List<GameObject> MapTypeNSW
    {
        get => mapTypeNSW;
    } [SerializeField] 
    private List<GameObject> mapTypeESW = new List<GameObject>();
    public List<GameObject> MapTypeESW
    {
        get => mapTypeESW;
    } 
    [SerializeField] 
    private List<GameObject> mapTypeNESW = new List<GameObject>();
    public List<GameObject> MapTypeNESW
    {
        get => mapTypeNESW;
    } 
    #endregion
    
    #region [04. trasform]
    /// <summary>
    /// 生成MapのRoot
    /// </summary>
    [SerializeField] 
    public Transform mapRoot;
    #endregion
    
    #region [05. information]
    /// <summary>
    /// 最大MapCollectNum
    /// </summary>
    [SerializeField] 
    private int maxTotalMapCollectNum;
    public int MaxTotalMapCollectNum
    {
        get => maxTotalMapCollectNum;
    }
    #endregion
    
    #endregion

    
    #region [func]
    /// <summary>
    /// コンストラクタ
    /// </summary>
    private void Awake()
    {
        
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    private void Start()
    {
        // インスタンス
        Instance = this;
        // 破棄不可
        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// 自動生成開始
    /// </summary>
    public void StartGenerating(Action onFinished = null)
    {
        Debug.LogFormat("MapGenerating Started", DColor.cyan);

        var randomNum = UnityEngine.Random.Range(0, 15);
        
        // 生成
        var instancedMap = Instantiate(this.mapStart[randomNum], this.mapRoot);
        // リストに追加
        MapCollector.Instance.AddMapToList(instancedMap);
        
        onFinished?.Invoke();
    }
    #endregion
}
