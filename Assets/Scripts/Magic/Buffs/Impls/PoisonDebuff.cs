using Entities;
using Magic.Buffs.Base;
using System;
using UnityEngine;

namespace Magic.Buffs.Impls
{
    [Serializable]
    public sealed class PoisonDebuff : TimedBuff
    {        
        [SerializeField][Min(0)] private float m_interval = 1f;
        [SerializeField][Min(0)] private float m_damagePerSeconds = 2f;

        private IHealth m_health;

        [NonSerialized] private float m_timer;

        public PoisonDebuff(string id, float duration, float interval, float dps) : base(id, duration)
        {
            m_interval = interval;
            m_damagePerSeconds = dps;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            m_health = container.GetComponent<IHealth>();
        }

        protected override void OnDeinitializing()
        {
            m_timer = 0;
            m_health = null;
            base.OnDeinitializing();
        }

        protected override void OnUpdated(float deltaTime)
        {
            if (m_health == null)
            {
                Deinitialize();
            }

            if (m_timer < m_interval)
            {
                m_timer += deltaTime;
            }
            else
            {
                m_timer = 0;
                // TODO: Attack
            }
        }

        public override object Clone() => new PoisonDebuff(Id, duration, m_interval, m_damagePerSeconds);        
    }
}
