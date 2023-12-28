using System;
using System.Collections.Generic;
using Cinemachine;
using GamePlay.Other.Navigators;
using StaticData.Prefabs;
using UI.GamePlay;
using UnityEngine;

namespace Services
{
    public class ObjectsProvider
    {
        private Hud _hud;


        public event Action HudLoaded;

        public Hud Hud
        {
            get => _hud;
            set
            {
                _hud = value;
                HudLoaded?.Invoke();
            }
        }

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