using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public bool startingRoom = false;
    public Vector2 _pos;

    public int _entrancePosition;
    public Room[] _nextRooms = new Room[4]; //This and doors can be used for the same functionality maybe?
    public bool[] _doors = new bool[4];
    public Room previousRoom;

    //Prefabs
    [SerializeField]
    private GameObject floorPrefab, wallPrefab;

    private Room(Room prev, Vector2 pos, int entranceDoor)
    {
        startingRoom = false;
        _pos = pos;
        _entrancePosition = entranceDoor;
        for (int i = 0; i < 4; i++)
        {
            if (i == _entrancePosition)
                _doors[i] = true;
            else
                _doors[i] = false;
        }
        previousRoom = prev;

        //SetPrefabs(prev.floorPrefab, prev.wallPrefab);
    }

    public Room()
    {
        startingRoom = true;
        _pos = new Vector2(5f, 5f); //make this the centre of the map
        for (int i = 0; i < 4; i++)
        {
            _doors[i] = true;
            _nextRooms[i] = AddRoom(i);
        }
    }


    //Crud functions


    //Creates a new room and returns it 
    //If the first check fails the room this is called from's instance is returned
    public Room AddRoom(int doorPos)
    {
        if (doorPos < 0 || doorPos > 3)
            return this;
        Vector2 dir = GetPosition(doorPos);
        Vector2 newPos = new Vector2(_pos.x + dir.x, _pos.y + dir.y);
        if (!startingRoom)
            _doors[doorPos] = true;
        _nextRooms[doorPos] = new Room(this, newPos, GetEntrance(doorPos));
        return _nextRooms[doorPos];
    }

    private int GetEntrance(int doorPos)
    {
        if (doorPos == 0)
            return 2;
        if (doorPos == 1)
            return 3;
        if (doorPos == 2)
            return 0;
        if (doorPos == 3)
            return 1;
        else return 0;
    }

    private Vector2 GetPosition(int doorPos)
    {
        if (doorPos == 0)
            return new Vector2(-1, 0);
        if (doorPos == 1)
            return new Vector2(0, 1);
        if (doorPos == 2)
            return new Vector2(1, 0);
        if (doorPos == 3)
            return new Vector2(0, -1);
        else return new Vector2(0, 0);
    }

    //public void DrawRoom(Room room)
    //{
    //    float offset = 6f;
    //    //DrawFloor
    //    GameObject floor = Instantiate(floorPrefab, new Vector3(room._pos.x * offset, room_pos.y * offset, 0f), Quaternion.identity);

    //    //Draw Walls
    //    //if (!_doors[0])
    //        Instantiate(wallPrefab, new Vector3((room._pos.x) * offset - 5f, room._pos.y * offset + 5f, 0f), Quaternion.Euler(0, 90, 90));
    //    //if (!_doors[2])
    //        Instantiate(wallPrefab, new Vector3(room._pos.x * offset, _pos.y * offset, 0f), Quaternion.Euler(180, 90, 90));
    //    //if (!_doors[3])
    //        Instantiate(wallPrefab, new Vector3(_pos.x * offset - 5f, _pos.y * offset, 0f), Quaternion.Euler(-90, 90, 90));
    //    //if (!_doors[1])
    //        Instantiate(wallPrefab, new Vector3(_pos.x * offset, _pos.y * offset + 5f, 0f), Quaternion.Euler(90, 90, 90));
    //}
}
