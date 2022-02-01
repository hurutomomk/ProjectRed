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
    /// 確実にドアが存在する場合のトリガー
    /// </summary>
    private bool hasNorthDoor = false;
    private bool hasEastDoor = false;
    private bool hasSouthDoor = false;
    private bool hasWestDoor = false;

    /// <summary>
    /// ドアの存在がどちらでもいい場合のトリガー
    /// </summary>
    private bool doesNorthDoorNotMatter = false;
    private bool doesEastDoorNotMatter = false;
    private bool doesSouthDoorNotMatter = false;
    private bool doesWestDoorNotMatter = false;

    public List<string> mustHaveDoorDirection = new List<string>();
    public List<string> neverHaveDoorDirection = new List<string>();

    public List<GameObject> tempQueueList = new List<GameObject>();
    public List<GameObject> generatingQueueList = new List<GameObject>();
    public List<GameObject> removingQueueList = new List<GameObject>();
    #endregion

    #endregion


    #region [func]

    /// <summary>
    /// コンストラクタ
    /// </summary>
    private void Start()
    {
        // Door方向チェック
        this.CheckDoorDirection();
    }

    /// <summary>
    /// Door方向チェック
    /// </summary>
    private void CheckDoorDirection()
    {
        if (this.mapInfo.HasNorthDoor)
            // Door方向にMapがあるかを判定
            this.CheckSpawnedMapOnNextPos(0);

        if (this.mapInfo.HasEastDoor)
            // Door方向にMapがあるかを判定
            this.CheckSpawnedMapOnNextPos(1);

        if (this.mapInfo.HasSouthDoor)
            // Door方向にMapがあるかを判定
            this.CheckSpawnedMapOnNextPos(2);

        if (this.mapInfo.HasWestDoor)
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
                            this.SetNewMapList(() => { this.GenerateNextMap(nextMapPos); });
                        });
                    });
                });
            });
        }
        
    }

    private void SetNewMapList(Action onFinished)
    {
        foreach (var map in MapGeneratingManager.Instance.MapList.Where(map => map.name.IndexOf(mustHaveDoorDirection[0]) > 0))
        {
            if (mustHaveDoorDirection.Count > 1)
            {
                if (map.name.IndexOf(mustHaveDoorDirection[1]) > 0)
                {
                    if (mustHaveDoorDirection.Count > 2)
                    {
                        if (map.name.IndexOf(mustHaveDoorDirection[2]) > 0)
                        {
                            if (mustHaveDoorDirection.Count == 4)
                            {
                                if (map.name.IndexOf(mustHaveDoorDirection[3]) > 0)
                                {
                                    this.generatingQueueList.Add(map);
                                }
                            }
                            else
                                this.generatingQueueList.Add(map);
                        }
                    }
                    else
                        this.generatingQueueList.Add(map);
                }
            }
            else
                this.generatingQueueList.Add(map);
        }

        for (int num = 0; num < neverHaveDoorDirection.Count; num++)
        {
            foreach (GameObject map
                     in MapGeneratingManager.Instance.MapList.ToArray().Where(a =>
                         a.name.IndexOf(neverHaveDoorDirection[num], StringComparison.Ordinal) > 0))
            {
                if (!this.removingQueueList.Contains(map))
                    this.removingQueueList.Add(map);
            }
        }
        
        foreach (GameObject map in this.removingQueueList)
        {
            if (this.generatingQueueList.Contains(map))
            {
                this.generatingQueueList.Remove(map);
                Debug.LogFormat($"Remove this map from list::: {map} ::: ", DColor.cyan);
            }
        }

        onFinished?.Invoke();
    }

    private void addMap(Action onFinished)
    {
        foreach (GameObject map
                 in MapGeneratingManager.Instance.MapList.ToArray()
                     .Where(a => a.name.IndexOf(mustHaveDoorDirection[0], StringComparison.Ordinal) > 0))
        {
            if (!this.tempQueueList.Contains(map))
            {
                this.tempQueueList.Add(map);
            }
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
                        this.SetDoorCondition(this.hasNorthDoor = true, this.doesNorthDoorNotMatter = false, 0);
                    else
                        this.SetDoorCondition(this.hasNorthDoor = false, this.doesNorthDoorNotMatter = false, 0);
                    break;
                case 1:
                    if (existMapInfo.HasWestDoor)
                        this.SetDoorCondition(this.hasEastDoor = true, this.doesEastDoorNotMatter = false, 1);
                    else
                        this.SetDoorCondition(this.hasEastDoor = false, this.doesEastDoorNotMatter = false, 1);
                    break;
                case 2:
                    if (existMapInfo.HasNorthDoor)
                        this.SetDoorCondition(this.hasSouthDoor = true, this.doesSouthDoorNotMatter = false, 2);
                    else
                        this.SetDoorCondition(this.hasSouthDoor = false, this.doesSouthDoorNotMatter = false, 2);
                    break;
                case 3:
                    if (existMapInfo.HasEastDoor)
                        this.SetDoorCondition(this.hasWestDoor = true, this.doesWestDoorNotMatter = false, 3);
                    else
                        this.SetDoorCondition(this.hasWestDoor = false, this.doesWestDoorNotMatter = false, 3);
                    break;
            }
        }
        else
        {
            // Door判定
            switch (doorDirection)
            {
                case 0:
                    this.SetDoorCondition(this.hasNorthDoor = false, this.doesNorthDoorNotMatter = true, 0);
                    break;
                case 1:
                    this.SetDoorCondition(this.hasEastDoor = false, this.doesEastDoorNotMatter = true, 1);
                    break;
                case 2:
                    this.SetDoorCondition(this.hasSouthDoor = false, this.doesSouthDoorNotMatter = true, 2);
                    break;
                case 3:
                    this.SetDoorCondition(this.hasWestDoor = false, this.doesWestDoorNotMatter = true, 3);
                    break;
            }
        }

        onFinished?.Invoke();
    }

    private void SetDoorCondition(bool hasThatDoor, bool thatDoorDoesntMatter, int doorDirection)
    {
        var doorString = "";
        switch (doorDirection)
        {
            case 0:
                doorString = "N";
                break;
            case 1:
                doorString = "E";
                break;
            case 2:
                doorString = "S";
                break;
            case 3:
                doorString = "W";
                break;
        }

        if (hasThatDoor)
        {
            Debug.LogFormat($"It must have this ::: {doorString} ::: ", DColor.green);
            
            this.mustHaveDoorDirection.Add(doorString);
        }

        else
        {
            if (thatDoorDoesntMatter) return;
            
            Debug.LogFormat($"It must not have this ::: {doorString} ::: ", DColor.yellow);

            this.neverHaveDoorDirection.Add(doorString);
        }
    }

    /// <summary>
    /// 次のMapを生成
    /// </summary>
    /// <param name="nextMapPos"></param>
    /// <param name="doorDirectionNum"></param>
    private void GenerateNextMap(Vector2 nextMapPos)
    {
        var randomNum = UnityEngine.Random.Range(0, this.generatingQueueList.Count);
        var creatTarget = this.generatingQueueList[randomNum];

        foreach (GameObject map in this.generatingQueueList)
        {
            Debug.LogFormat($" ::::::::::::::::::::::::::::::::::::::: {map.name}");
        }
        
        if (MapGeneratingManager.Instance.MaxTotalMapCollectNum > MapCollector.Instance.currentTotalMapCollectNum)
        {
            // 生成
            var instancedMap = Instantiate(creatTarget, MapGeneratingManager.Instance.mapRoot);
            instancedMap.transform.position = nextMapPos;
            // リストに追加
            MapCollector.Instance.AddMapToList(instancedMap);
            MapCollector.Instance.currentTotalMapCollectNum += this.mapInfo.MapCollectNum;

            this.generatingQueueList.Clear();
            this.removingQueueList.Clear();
            this.mustHaveDoorDirection.Clear();
            this.neverHaveDoorDirection.Clear();
            
            Debug.LogFormat($" :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::: " +
                            $"{MapCollector.Instance.collectedMapList.Count} + {instancedMap.name}", DColor.yellow);
        }
        else
        {
            Debug.LogFormat("Map Generating is Done", DColor.cyan);
            return;
        }
    }

#endregion
}
