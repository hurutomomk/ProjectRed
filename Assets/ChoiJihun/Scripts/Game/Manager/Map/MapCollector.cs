using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapCollector : MonoBehaviour
{
    public static MapCollector Instance { get; set; }
    
    public List<GameObject> collectedMapList = new List<GameObject>();

    public void Start()
    {
        Instance = this;
    }

    public void AddMapToList(GameObject mapObj)
    {
        collectedMapList.Add(mapObj);
    }

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
}
