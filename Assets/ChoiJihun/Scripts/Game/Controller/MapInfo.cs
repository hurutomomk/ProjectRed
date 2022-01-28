using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfo : MonoBehaviour
{
    #region [var]
    /// <summary>
    /// NrothDoor
    /// </summary>
    [SerializeField] 
    private bool hasNorthDoor = false;
    public bool HasNorthDoor
    {
        get => hasNorthDoor;
    }
    /// <summary>
    /// EastDoor
    /// </summary>
    [SerializeField] 
    private bool hasEastDoor = false;
    public bool HasEastDoor
    {
        get => hasEastDoor;
    }
    /// <summary>
    /// SouthDoor
    /// </summary>
    [SerializeField] 
    private bool hasSouthDoor = false;
    public bool HasSouthDoor
    {
        get => hasSouthDoor;
    }
    /// <summary>
    /// WestDoor
    /// </summary>
    [SerializeField] 
    private bool hasWestDoor = false;
    public bool HasWestDoor
    {
        get => hasWestDoor;
    }
    #endregion
    
}
