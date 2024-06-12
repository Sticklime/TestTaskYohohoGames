using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.Logic.Character
{
    public class AnimationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTag, AnimationComponent> _movableFilter = null;

        private static readonly int _idleStateHash = Animator.StringToHash("IsIdle");
        private static readonly int _runStateHash = Animator.StringToHash("IsRun");

        public void Run()
        {
            foreach (var i in _movableFilter)
            {
                ref var animationComponent = ref _movableFilter.Get2(i);

                ref var animationState = ref animationComponent.animationState;
                ref var animator = ref animationComponent.animator;
                
                switch (animationState)
                {
                    case AnimationStateType.Idle:
                        animator.SetTrigger(_idleStateHash);
                        animator.SetBool(_runStateHash,false);
                        break;
                    case AnimationStateType.Run:
                        animator.SetBool(_runStateHash,true);
                        break;
                    default:
                        animator.SetTrigger(_idleStateHash);
                        break;
                }
            }
        }
    }
}