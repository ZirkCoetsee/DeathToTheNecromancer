using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    [SerializeField]
    private Room rootRoom = new Room();

    [SerializeField]
    private GameObject floorPrefab, wallPrefab;

    /// Responsibility of this class 
    /// Generate a random amount of rooms
    /// Draw the rooms 
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        GenerateRooms(rootRoom, 10);
        DrawAllRooms(rootRoom);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Returns last room at the moment
    private Room GenerateRooms(Room next, int amount)
    {
        if (amount == 0)
            return next;
        if (next.startingRoom)
        {
            int dominantDir = Random.Range(0, 4);
            //DrawRoom(next);
            //foreach (Room neighbour in next._nextRooms)
            //{
            //    DrawRoom(neighbour);
            //}
            next = GenerateRooms(next._nextRooms[dominantDir].AddRoom(dominantDir), amount - 1);
        }
        else
        {
            int randomDir = Random.Range(0, 4);
            if (randomDir == next._entrancePosition)
                return GenerateRooms(next, amount);
            //Instantiate(floorPrefab, new Vector3(next._pos.x * 5f, next._pos.y * 5f, 0f), Quaternion.Inverse(this.gameObject.transform.rotation));
            //DrawRoom(next);
            next = GenerateRooms(next.AddRoom(randomDir), amount - 1);
        }
        return next;

        //if (amount == 0)
        //    return next;
        //else
        //{
        //    //Debug.Log("Room created at: " + next._pos.ToString());
        //    Instantiate(floorPrefab, new Vector3(next._pos.x * 5f, next._pos.y * 5f, 0f), Quaternion.Inverse(this.gameObject.transform.rotation));
        //    next = GenerateRooms(next.AddRoom(0), amount - 1); 
        //}
        //return next;
    }


    public void DrawAllRooms(Room root)
    {
        DrawRoom(root);

        for (int i = 0; i < 4; i++)
        {
            if(i != root._entrancePosition && root._nextRooms[i] != null)
            {
                DrawAllRooms(root._nextRooms[i]);
            }
        }
        //foreach (Room neighbour in rootRoom._nextRooms)
        //{
        //    if (neighbour != null || )
        //    {
        //        DrawRoom(neighbour);
        //        DrawAllRooms(neighbour);
        //    }   
        //}
    }
    public void DrawRoom(Room room)
    {
        float offset = 6f;
        //DrawFloor
        GameObject floor = Instantiate(floorPrefab, new Vector3(room._pos.x * offset, room._pos.y * offset, 0f), Quaternion.identity);

        //Draw Walls
        if (!room._doors[0])
            Instantiate(wallPrefab, new Vector3((room._pos.x) * offset - 5f, room._pos.y * offset + 5f, 0f), Quaternion.Euler(0, 90, 90));
        if (!room._doors[2])
            Instantiate(wallPrefab, new Vector3(room._pos.x * offset, room._pos.y * offset, 0f), Quaternion.Euler(180, 90, 90));
        if (!room._doors[3])
            Instantiate(wallPrefab, new Vector3(room._pos.x * offset - 5f, room._pos.y * offset, 0f), Quaternion.Euler(-90, 90, 90));
        if (!room._doors[1])
            Instantiate(wallPrefab, new Vector3(room._pos.x * offset, room._pos.y * offset + 5f, 0f), Quaternion.Euler(90, 90, 90));
    }
}
