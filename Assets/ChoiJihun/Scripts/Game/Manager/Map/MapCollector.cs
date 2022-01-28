using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapCollector : MonoBehaviour
{
    #region [var]

    #region [01. instance]
    /// <summary>
    /// インスタンス
    /// </summary>
    public static MapCollector Instance { get; set; }
    #endregion
    
    #region [03. map data]
    /// <summary>
    /// 生成したMapのリスト
    /// </summary>
    public List<GameObject> collectedMapList = new List<GameObject>();
    #endregion
    
    #endregion

    
    
    #region [func]

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public void Start()
    {
        Instance = this;
    }

    /// <summary>
    /// リストに生成したMapを追加
    /// </summary>
    /// <param name="mapObj"></param>
    public void AddMapToList(GameObject mapObj)
    {
        collectedMapList.Add(mapObj);
    }

    /// <summary>
    /// リスト上のMapとPositionが重複するかどうかをチェック
    /// </summary>
    /// <param name="targetMapPos"></param>
    /// <returns></returns>
    public bool CheckMapPosWithList(Vector2 targetMapPos)
    {
        var isMatched = false;
        
        foreach (GameObject mapTransform in collectedMapList)
        {
            if (mapTransform.transform.position == new Vector3(targetMapPos.x, targetMapPos.y, 0))
            {
                Debug.LogFormat($" TargetPos = {targetMapPos}    :::::    ExistMapPos = {mapTransform.transform.position}");
                isMatched = true;
            }
        }

        return isMatched;
    }

    #endregion
}
