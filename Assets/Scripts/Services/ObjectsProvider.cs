using System.Collections.Generic;
using Additional.Game;
using Cinemachine;
using GamePlay.Other.Navigators;
using StaticData.Prefabs;
using UI.GamePlay;
using UnityEngine;

namespace Services
{
    public class ObjectsProvider : MonoSingleton<ObjectsProvider>
    {
        public Hud Hud { get; set; }
        public GameObject Character { get; set; }
        public Rigidbody CharacterRigidbody { get; set; }
        public RoomsMap RoomsMap { get; set; }
        public Room[] Rooms { get; set; }
        public Room CurrentRoom { get; set; }
        public DirectionArrow DirectionArrow { get; set; }
        public List<Transform> AliveEnemies { get; set; }

        public Camera MainCamera { get; set; }
        public CinemachineVirtualCamera VirtualCamera { get; set; }
        public Camera UiCamera { get; set; }
        public AudioSource MusicSource { get; set; }
    }
}