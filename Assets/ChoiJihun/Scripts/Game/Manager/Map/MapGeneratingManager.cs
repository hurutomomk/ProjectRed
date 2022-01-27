using System;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private List<GameObject> mapStart = new List<GameObject>();
    #endregion
    
    #region [04. trasform]
    /// <summary>
    /// 生成MapのRoot
    /// </summary>
    [SerializeField] private Transform mapRoot;
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

        // 生成
        var instancedMap = Instantiate(this.mapStart[0], this.mapRoot);
        // リストに追加
        MapCollector.Instance.AddMapToList(instancedMap);
        
        onFinished?.Invoke();
    }
    #endregion
}
