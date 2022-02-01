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
    private List<GameObject> mapList = new List<GameObject>();
    public List<GameObject> MapList
    {
        get => mapList;
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
    /// 自動生成開始：一つ目のマップを生成
    /// </summary>
    public void StartGenerating(Action onFinished = null)
    {
        Debug.LogFormat("MapGenerating Started", DColor.cyan);

        var randomNum = UnityEngine.Random.Range(0, 15);
        
        // 生成
        var instancedMap = Instantiate(this.mapList[randomNum], this.mapRoot);
        // リストに追加
        MapCollector.Instance.AddMapToList(instancedMap);
        
        onFinished?.Invoke();
    }
    #endregion
    
}
