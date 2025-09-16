using System;
using System.Collections.Generic;
using Gameplay;
using Unity.Collections;
using UnityEngine;

namespace Internal.Gameplay.Environment.Rooms
{
    public class PlayerRoomManager : MonoBehaviour
    {
        [field: SerializeField] public Room RoomWherePlayerNow { get; private set; }
        [field: SerializeField] public PlayerRoomTrigger[] RoomTriggers { get; private set; }

        [field: SerializeField, ReadOnly] public List<Room> AllRooms { get; private set; }

        public event Action<Room> OnWherePlayerNowRoomChanged;

        private void Awake()
        {
            foreach (var trigger in RoomTriggers)
            {
                AllRooms.Add(trigger.Room);
            }
        }

        public void SetRoomWherePlayerNow(Room room)
        {
            RoomWherePlayerNow = room;
            OnWherePlayerNowRoomChanged?.Invoke(room);
        }
    }

    [Serializable]
    public class Room
    {
        [field: SerializeField] public Light RoomLight { get; private set; }
        [field: SerializeField] public FlickeringLight FlickeringLight { get; private set; }
        [field: SerializeField] public Transform KenrWaypoint { get; private set; }
    }
}