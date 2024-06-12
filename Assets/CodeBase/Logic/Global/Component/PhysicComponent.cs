using System;
using UnityEngine;

namespace CodeBase.Logic.Global
{
    [Serializable]
    public struct PhysicComponent
    {
        public Rigidbody Rigibody;
        public Collider Collider;
    }
}