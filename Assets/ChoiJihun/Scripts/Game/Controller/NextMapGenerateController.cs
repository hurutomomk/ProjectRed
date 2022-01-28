using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Unity.VisualScripting;
using UnityEngine;

public class NextMapGenerateController : MonoBehaviour
{
    #region [var]
    
    #region [01. reference]
    /// <summary>
    /// Map情報
    /// </summary>
    [SerializeField] 
    private MapInfo mapInfo;
    #endregion
    
    #region [02. map data]
    /// <summary>
    /// トリガー
    /// </summary>
    private bool hasNorthDoor = false;
    private bool hasEastDoor = false;
    private bool hasSouthDoor = false;
    private bool hasWestDoor = false;
    #endregion
    
    #endregion


    #region [func]
    /// <summary>
    /// コンストラクタ
    /// </summary>
    private void Start()
    {
        // Door方向チェック
        CheckDoorDirection();
    }

    /// <summary>
    /// Door方向チェック
    /// </summary>
    private void CheckDoorDirection()
    {
        if(this.mapInfo.HasNorthDoor)
            // Door方向にMapがあるかを判定
            this.CheckSpawnedMapOnNextPos(0);
        
        if(this.mapInfo.HasEastDoor)
            // Door方向にMapがあるかを判定
            this.CheckSpawnedMapOnNextPos(1);
    
        if(this.mapInfo.HasSouthDoor)
            // Door方向にMapがあるかを判定
            this.CheckSpawnedMapOnNextPos(2);
        
        if(this.mapInfo.HasWestDoor)
            // Door方向にMapがあるかを判定
            this.CheckSpawnedMapOnNextPos(3);
    }

    /// <summary>
    /// Door方向にMapがあるかを判定
    /// </summary>
    /// <param name="doorDirection"></param>
    private void CheckSpawnedMapOnNextPos(int doorDirection)
    {
        // 次のMap座標
        var nextMapPos = 
            GridManager.Instance.NextGridPos(this.transform.position, doorDirection);

        // 次のMap座標にすでにMapがある場合
        if (MapCollector.Instance.CheckMapPosWithList(nextMapPos))
            return;
        else
        {
            // North方向にMap有無チェック
            this.CheckSpawnedMapOnFuturePos(nextMapPos, 0, () =>
            {
                // East方向にMap有無チェック
                this.CheckSpawnedMapOnFuturePos(nextMapPos, 1, () =>
                {
                    // South方向にMap有無チェック
                    this.CheckSpawnedMapOnFuturePos(nextMapPos, 2, () =>
                    {
                        // West方向にMap有無チェック
                        this.CheckSpawnedMapOnFuturePos(nextMapPos, 3, () =>
                        {
                            // 次のMap生成
                            this.GenerateNextMap(nextMapPos, 
                                // Doorパターンナンバー
                                this.SetDoorDirectionNum(this.hasNorthDoor, this.hasEastDoor, this.hasSouthDoor, this.hasWestDoor));
                        });
                    });
                });
            });
        }
    }

    /// <summary>
    /// Map有無チェック
    /// </summary>
    /// <param name="nextPos"></param>
    /// <param name="doorDirection"></param>
    /// <param name="onFinished"></param>
    private void CheckSpawnedMapOnFuturePos(Vector2 nextPos, int doorDirection, Action onFinished)
    {
        // その次のMap座標
        var futureMapPos = GridManager.Instance.NextGridPos(nextPos, doorDirection);
        // Door判定
        var isThereDoor = !MapCollector.Instance.CheckMapPosWithList(futureMapPos);
        switch (doorDirection)
        {
            case 0:
                this.hasNorthDoor = isThereDoor;
                break;
            case 1:
                this.hasEastDoor = isThereDoor;
                break;
            case 2:
                this.hasSouthDoor = isThereDoor;
                break;
            case 3:
                this.hasWestDoor = isThereDoor;
                break;
        }

        onFinished?.Invoke();
    }

    /// <summary>
    /// Doorパータンナンバー
    /// </summary>
    /// <param name="north"></param>
    /// <param name="east"></param>
    /// <param name="south"></param>
    /// <param name="west"></param>
    /// <returns></returns>
    private int SetDoorDirectionNum(bool north, bool east, bool south, bool west)
    {
        if (north)
        {
            return east ? south ? west ? 1 : 2 : west ? 3 : 4 : south ? west ? 5 : 6 : west ? 7 : 8;
        }
        else
        {
            return east ? south ? west ? 9 : 10 : west ? 11 : 12 : south ? west ? 13 : 14 : west ? 15 : 16;
        }
    } 

    /// <summary>
    /// 次のMapを生成
    /// </summary>
    /// <param name="nextMapPos"></param>
    /// <param name="doorDirectionNum"></param>
    private void GenerateNextMap(Vector2 nextMapPos, int doorDirectionNum)
    {
        Debug.LogFormat($" nextMapPos = {nextMapPos}   :::::    doorDirectionNum = {doorDirectionNum}");
    }
    #endregion
}
