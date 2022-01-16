using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region [var]

    #region [01. Instance]
    
    /// <summary>
    /// インスタンス
    /// </summary>
    public static GameManager Instance { get; private set; }

    #endregion
    
    

    #endregion

    #region [func]

    /// <summary>
    /// コンストラクタ
    /// </summary>
    private void Awake()
    {
        // FPS制限
        Application.targetFrameRate = 60;
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
        
        // 画面スリープ不可
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    #endregion
}
