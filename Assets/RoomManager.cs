using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    // serves to check if player exitted the room but also spawn point for next room
    public Transform endPos;
    public LayerMask playerLayer;
    public bool setOnAwake;

    private RoomGenerator roomGenerator;
    private bool canCreateNextRoom = true;

    void Awake()
    {
        if (setOnAwake)
        {
            roomGenerator = GameObject.Find("GameManager").GetComponent<RoomGenerator>();
            roomGenerator.SetRoomEndY(endPos.position.y);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
