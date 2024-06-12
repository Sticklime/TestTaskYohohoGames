using System;

namespace CodeBase.Logic.Cooldown
{
    [Serializable]
    public struct CooldownComponent
    {
        public float Cooldown;
        public float CurrentCooldown;
    }
}