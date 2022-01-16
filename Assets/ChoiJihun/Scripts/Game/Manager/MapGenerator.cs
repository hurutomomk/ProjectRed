using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    #region [var]

    #region [01. Instance]
    
    /// <summary>
    /// インスタンス
    /// </summary>
    public static MapGenerator Instance { get; private set; }

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

    #endregion
}
