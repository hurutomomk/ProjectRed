using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    #region [var]

    #region [01. instance]
    
    /// <summary>
    /// インスタンス
    /// </summary>
    public static GameManager Instance { get; private set; }

    #endregion
    
    #region [02. reference]

    /// <summary>
    /// MapGenerator
    /// </summary>
    [SerializeField]
    private MapGeneratingManager mapGeneratingManager;

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
        
        // マップ自動生成シーケンス
        this.MapGeneratingSequence();
    }

    #region [01. Map Generating Sequence]

    /// <summary>
    /// マップ自動生成シーケンス
    /// </summary>
    private void MapGeneratingSequence()
    {
        // 開始
        this.mapGeneratingManager.StartGenerating();
    }

    #endregion

    #endregion
}
