using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Unity.VisualScripting;
using UnityEngine;

public class NextMapGenerateController : MonoBehaviour
{
    [SerializeField] private MapInfo mapInfo;

    private bool hasNorthDoor = false;
    private bool hasEastDoor = false;
    private bool hasSouthDoor = false;
    private bool hasWestDoor = false;

    private int doorDirectionNum;
    
    /// <summary>
    /// コンストラクタ
    /// </summary>
    private void Start()
    {
        Debug.LogFormat("00000000000000000000", DColor.yellow);
        
        // 
        CheckDoorDirection();
    }

    /// <summary>
    /// 
    /// </summary>
    private void CheckDoorDirection()
    {
        if(this.mapInfo.HasNorthDoor)
            this.CheckSpawnedMapOnNextPos(0);
        
        if(this.mapInfo.HasEastDoor)
            this.CheckSpawnedMapOnNextPos(1);
    
        if(this.mapInfo.HasSouthDoor)
            this.CheckSpawnedMapOnNextPos(2);
        
        if(this.mapInfo.HasWestDoor)
            this.CheckSpawnedMapOnNextPos(3);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="doorDirection"></param>
    private void CheckSpawnedMapOnNextPos(int doorDirection)
    {
        var nextMapPos = 
            GridManager.Instance.NextGridPos(this.transform.position, doorDirection);

        if (MapCollector.Instance.CheckMapPosWithList(nextMapPos))
            return;
        else
        {
            this.CheckSpawnedMapOnFuturePosWithNorthDoor(nextMapPos, () =>
            {
                this.CheckSpawnedMapOnFuturePosWithEastDoor(nextMapPos, () =>
                {
                    this.CheckSpawnedMapOnFuturePosWithSouthDoor(nextMapPos, () =>
                    {
                        this.CheckSpawnedMapOnFuturePosWithWestDoor(nextMapPos, () =>
                        {
                            this.doorDirectionNum = 
                                this.SetDoorDirectionNum(this.hasNorthDoor, this.hasEastDoor, this.hasSouthDoor, this.hasWestDoor);

                            this.GenerateNextMap(nextMapPos, this.doorDirectionNum);
                        });
                    });
                });
            });
        }
    }

    private void CheckSpawnedMapOnFuturePosWithNorthDoor(Vector2 nextPos, Action onFinished)
    {
        var futureMapPos = GridManager.Instance.NextGridPos(nextPos, 0);

        this.hasNorthDoor = !MapCollector.Instance.CheckMapPosWithList(futureMapPos);
        
        onFinished?.Invoke();
    }
    
    private void CheckSpawnedMapOnFuturePosWithEastDoor(Vector2 nextPos, Action onFinished)
    {
        var futureMapPos = GridManager.Instance.NextGridPos(nextPos, 1);

        this.hasEastDoor = !MapCollector.Instance.CheckMapPosWithList(futureMapPos);
        
        onFinished?.Invoke();
    }
    
    private void CheckSpawnedMapOnFuturePosWithSouthDoor(Vector2 nextPos, Action onFinished)
    {
        var futureMapPos = GridManager.Instance.NextGridPos(nextPos, 2);

        this.hasSouthDoor = !MapCollector.Instance.CheckMapPosWithList(futureMapPos);
        
        onFinished?.Invoke();
    }
    
    private void CheckSpawnedMapOnFuturePosWithWestDoor(Vector2 nextPos, Action onFinished)
    {
        var futureMapPos = GridManager.Instance.NextGridPos(nextPos, 3);

        this.hasWestDoor = !MapCollector.Instance.CheckMapPosWithList(futureMapPos);
        
        onFinished?.Invoke();
    }

    private int SetDoorDirectionNum(bool north, bool east, bool south, bool west)
    {
        var directionNum = 0;
        
        if (north)
        {
            if (east)
            {
                if (south)
                {
                    if (west)
                    {
                        directionNum = 1;
                    }
                    else
                    {
                        directionNum = 2;
                    }
                }
                else
                {
                    if (west)
                    {
                        directionNum = 3;
                    }
                    else
                    {
                        directionNum = 4;
                    }
                }
            }
            else
            {
                if (south)
                {
                    if (west)
                    {
                        directionNum = 5;
                    }
                    else
                    {
                        directionNum = 6;
                    }
                }
                else
                {
                    if (west)
                    {
                        directionNum = 7;
                    }
                    else
                    {
                        directionNum = 8;
                    }
                }
            }
        }
        else
        {
            if (east)
            {
                if (south)
                {
                    if (west)
                    {
                        directionNum = 9;
                    }
                    else
                    {
                        directionNum = 10;
                    }
                }
                else
                {
                    if (west)
                    {
                        directionNum = 11;
                    }
                    else
                    {
                        directionNum = 12;
                    }
                }
            }
            else
            {
                if (south)
                {
                    if (west)
                    {
                        directionNum = 13;
                    }
                    else
                    {
                        directionNum = 14;
                    }
                }
                else
                {
                    if (west)
                    {
                        directionNum = 15;
                    }
                    else
                    {
                        directionNum = 16;
                    }
                }
            }
        }

        return directionNum;
    } 

    private void GenerateNextMap(Vector2 nextMapPos, int doorDirectionNum)
    {
        Debug.LogFormat($" nextMapPos = {nextMapPos}   :::::    doorDirectionNum = {doorDirectionNum}");
    }
}
