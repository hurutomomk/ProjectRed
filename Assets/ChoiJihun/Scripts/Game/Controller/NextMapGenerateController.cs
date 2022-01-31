using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    /// <summary>
    /// 次のマップ生成方向の順番をリストアップ
    /// </summary>
    private List<int> doorDirectionNumList = new List<int>(){0, 1, 2, 3};
    #endregion
    
    #endregion


    #region [func]
    /// <summary>
    /// コンストラクタ
    /// </summary>
    private void Start()
    {
        // Listの中身をランダムに並び替え
        this.doorDirectionNumList = this.doorDirectionNumList.OrderBy(a => Guid.NewGuid()).ToList();
        
        // Door方向チェック
        this.CheckDoorDirection();
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
        // 次のMap座標にすでにMapがない場合
        else
        {
            // 次のMap座標からNorth方向にMap有無チェック
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
        
        // 次のMap座標にすでにMapがある場合
        var isFutureMapExist = MapCollector.Instance.CheckMapPosWithList(futureMapPos);
        if (isFutureMapExist)
        {
            var existMapInfo = MapCollector.Instance.TempMatchedMap.GetComponent<MapInfo>();
            // Door判定
            switch (doorDirection)
            {
                case 0:
                    if (existMapInfo.HasSouthDoor)
                        this.hasNorthDoor = true;
                    break;
                case 1:
                    if (existMapInfo.HasWestDoor)
                        this.hasEastDoor = true;
                    break;
                case 2:
                    if (existMapInfo.HasNorthDoor)
                        this.hasSouthDoor = true;
                    break;
                case 3:
                    if (existMapInfo.HasEastDoor)
                        this.hasWestDoor = true;
                    break;
            }
        }
        else
        {
            // Door判定
            switch (doorDirection)
            {
                case 0:
                    this.hasNorthDoor = false;
                    break;
                case 1:
                    this.hasEastDoor = false;
                    break;
                case 2:
                    this.hasSouthDoor = false;
                    break;
                case 3:
                    this.hasWestDoor = false;
                    break;
            }
        }

        
        Debug.LogFormat($" ::: {MapCollector.Instance.collectedMapList.Count + 1} :::");
        Debug.LogFormat($" ::: {MapCollector.Instance.collectedMapList.Count + 1} ::: futureMapPos = {futureMapPos}");
        Debug.LogFormat($" ::: {MapCollector.Instance.collectedMapList.Count + 1} ::: N = {this.hasNorthDoor} ::: E = {this.hasEastDoor}" +
                        $" ::: S = {this.hasSouthDoor} ::: W = {this.hasWestDoor}");
        Debug.LogFormat($" ");
        
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
        var randomNum = 0;
        var creatTarget = MapGeneratingManager.Instance.MapStart[randomNum];
        switch (doorDirectionNum)
        {
            case 1:
                randomNum = UnityEngine.Random.Range(0, MapGeneratingManager.Instance.MapTypeNESW.Count);
                creatTarget = MapGeneratingManager.Instance.MapTypeNESW[randomNum];
                break;
            case 2:
                randomNum = UnityEngine.Random.Range(0, MapGeneratingManager.Instance.MapTypeNES.Count);
                creatTarget = MapGeneratingManager.Instance.MapTypeNES[randomNum];
                break;
            case 3:
                randomNum = UnityEngine.Random.Range(0, MapGeneratingManager.Instance.MapTypeNEW.Count);
                creatTarget = MapGeneratingManager.Instance.MapTypeNEW[randomNum];
                break;
            case 4:
                randomNum = UnityEngine.Random.Range(0, MapGeneratingManager.Instance.MapTypeNE.Count);
                creatTarget = MapGeneratingManager.Instance.MapTypeNE[randomNum];
                break;
            case 5:
                randomNum = UnityEngine.Random.Range(0, MapGeneratingManager.Instance.MapTypeNSW.Count);
                creatTarget = MapGeneratingManager.Instance.MapTypeNSW[randomNum];
                break;
            case 6:
                randomNum = UnityEngine.Random.Range(0, MapGeneratingManager.Instance.MapTypeNS.Count);
                creatTarget = MapGeneratingManager.Instance.MapTypeNS[randomNum];
                break;
            case 7:
                randomNum = UnityEngine.Random.Range(0, MapGeneratingManager.Instance.MapTypeNW.Count);
                creatTarget = MapGeneratingManager.Instance.MapTypeNW[randomNum];
                break;
            case 8:
                randomNum = UnityEngine.Random.Range(0, MapGeneratingManager.Instance.MapTypeN.Count);
                creatTarget = MapGeneratingManager.Instance.MapTypeN[randomNum];
                break;
            case 9:
                randomNum = UnityEngine.Random.Range(0, MapGeneratingManager.Instance.MapTypeESW.Count);
                creatTarget = MapGeneratingManager.Instance.MapTypeESW[randomNum];
                break;
            case 10:
                randomNum = UnityEngine.Random.Range(0, MapGeneratingManager.Instance.MapTypeES.Count);
                creatTarget = MapGeneratingManager.Instance.MapTypeES[randomNum];
                break;
            case 11:
                randomNum = UnityEngine.Random.Range(0, MapGeneratingManager.Instance.MapTypeEW.Count);
                creatTarget = MapGeneratingManager.Instance.MapTypeEW[randomNum];
                break;
            case 12:
                randomNum = UnityEngine.Random.Range(0, MapGeneratingManager.Instance.MapTypeE.Count);
                creatTarget = MapGeneratingManager.Instance.MapTypeE[randomNum];
                break;
            case 13:
                randomNum = UnityEngine.Random.Range(0, MapGeneratingManager.Instance.MapTypeSW.Count);
                creatTarget = MapGeneratingManager.Instance.MapTypeSW[randomNum];
                break;
            case 14:
                randomNum = UnityEngine.Random.Range(0, MapGeneratingManager.Instance.MapTypeS.Count);
                creatTarget = MapGeneratingManager.Instance.MapTypeS[randomNum];
                break;
            case 15:
                randomNum = UnityEngine.Random.Range(0, MapGeneratingManager.Instance.MapTypeW.Count);
                creatTarget = MapGeneratingManager.Instance.MapTypeW[randomNum];
                break;
            case 16:
                Debug.LogFormat("Error ::: There is No Map can be spawned", DColor.red);
                break;
        }


        if (MapGeneratingManager.Instance.MaxTotalMapCollectNum > MapCollector.Instance.currentTotalMapCollectNum)
        {
            // 生成
            var instancedMap = Instantiate(creatTarget, MapGeneratingManager.Instance.mapRoot);
            instancedMap.transform.position = nextMapPos;
            // リストに追加
            MapCollector.Instance.AddMapToList(instancedMap);
            MapCollector.Instance.currentTotalMapCollectNum += this.mapInfo.MapCollectNum;
            
            Debug.LogFormat($" ::: {MapCollector.Instance.collectedMapList.Count} :::", DColor.cyan);
            Debug.LogFormat($" ::: {MapCollector.Instance.collectedMapList.Count} ::: nextMapPos = {nextMapPos}", DColor.cyan);
            Debug.LogFormat($" ::: {MapCollector.Instance.collectedMapList.Count} ::: doorDirectionNum = {doorDirectionNum}", DColor.cyan);
            Debug.LogFormat($" ");
        }
        else
        {
            Debug.LogFormat("Map Generating is Done", DColor.cyan);
            return;
        }
        
    }
    #endregion
}
