using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.Logic.Cooldown
{
    public class CooldownSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CooldownComponent> cooldownFilter = null;

        public void Run()
        {
            foreach (var i in cooldownFilter)
            {
                ref var cooldownComponent = ref cooldownFilter.Get1(i);
                ref var entity = ref cooldownFilter.GetEntity(i);

                ref var cooldown = ref cooldownComponent.Cooldown;
                ref var currentCooldown = ref cooldownComponent.CurrentCooldown;

                currentCooldown -= Time.deltaTime;

                if (CooldownIsUp(currentCooldown))
                {
                    currentCooldown = cooldown;
                    entity.Get<CooldownEvent>();
                }
            }
        }

        private bool CooldownIsUp(float currentCooldown) =>
            currentCooldown <= 0;
    }
}