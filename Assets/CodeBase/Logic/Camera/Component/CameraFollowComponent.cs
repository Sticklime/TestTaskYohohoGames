using System;
using UnityEngine;

namespace CodeBase.Logic.Camera.Component
{
    [Serializable]
    public struct CameraFollowComponent
    {
        public Vector3 Offset;
        public float SmoothSpeed;
    }
}