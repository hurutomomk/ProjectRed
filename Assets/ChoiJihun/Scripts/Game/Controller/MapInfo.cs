using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfo : MonoBehaviour
{
    [SerializeField] 
    private bool hasNorthDoor = false;
    public bool HasNorthDoor
    {
        get => hasNorthDoor;
    }
    
    [SerializeField] 
    private bool hasEastDoor = false;
    public bool HasEastDoor
    {
        get => hasEastDoor;
    }
    
    [SerializeField] 
    private bool hasSouthDoor = false;
    public bool HasSouthDoor
    {
        get => hasSouthDoor;
    }
    
    [SerializeField] 
    private bool hasWestDoor = false;
    public bool HasWestDoor
    {
        get => hasWestDoor;
    }
}
