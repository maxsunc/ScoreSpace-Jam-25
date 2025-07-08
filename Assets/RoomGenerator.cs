using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public GameObject[] roomPrefabs;

    private GameObject[] currentRooms;
    // when player exceeds make another room (at the next roomendY?)
    public float roomEndY;
    public float nextRoomEndY;
    private int roomNumber;
    private Transform player;

    private int lastIndexInstantiated;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if(roomEndY <= player.position.y)
        {
            // generate a new room
            int index = 0;
            do
            {
                index = Random.Range(0, roomPrefabs.Length);
            } while (index == lastIndexInstantiated);
            lastIndexInstantiated = index;
            
            GameObject b = Instantiate(roomPrefabs[index], new Vector3(0, nextRoomEndY, 0), Quaternion.identity);
            
        }


    }

    public void SetRoomEndY(float value)
    {
        roomEndY = nextRoomEndY;
        nextRoomEndY = value + 2;
    }

}
