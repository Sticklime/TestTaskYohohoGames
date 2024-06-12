using System;
using UnityEngine;

namespace CodeBase.Logic.Character
{
    [Serializable]
    public struct MovableComponent
    {
        public CharacterController CharacterController;
        public float Speed;
    }
}