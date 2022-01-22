using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneratingManager : MonoBehaviour
{
    #region [var]

    #region [01. Instance]
    
    /// <summary>
    /// インスタンス
    /// </summary>
    public static MapGeneratingManager Instance { get; private set; }

    #endregion
    
    #region [02. reference]

    

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
    public void StartGenerating()
    {
        Debug.LogFormat("MapGenerating Started", DColor.cyan);
    }

    #endregion
}
