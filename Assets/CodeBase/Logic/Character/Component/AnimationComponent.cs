using System;
using UnityEngine;

namespace CodeBase.Logic.Character
{
    [Serializable]
    public struct AnimationComponent
    {
        public AnimationStateType animationState;
        public Animator animator;
    }
}