using Entities.Enemies.Data;
using Magic.Spells.Data;
using Magic.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Entities.Enemies.Systems
{
    internal class AttackEnemy : MonoBehaviour
    {
        private Transform m_target;
        private IReadOnlyList<SpellEnemyData> m_spells;
        private SpellCaster m_spellCaster;

        private float m_cooldownTimer;
        private float m_attackTime;
        private bool m_isInitialized;
        private int m_maxCount;
        private int m_count;
        private BaseSpellData m_defaultSpell;

        public void Initialize(IReadOnlyList<SpellEnemyData> spells, Transform target, float attackTime, BaseSpellData defaultSpell)
        {
            if (m_isInitialized)
            {
                return;
            }

            m_defaultSpell = defaultSpell;
            m_spellCaster = new SpellCaster(transform);
            m_spells = spells.OrderBy(spell => spell.count).ToArray();
            m_maxCount = m_spells.LastOrDefault().count;
            m_attackTime = attackTime;
            m_target = target;
            m_isInitialized = true;
        }

        private void Update()
        {
            if (!m_isInitialized) return;            

            if (m_cooldownTimer > 0)
            {
                m_cooldownTimer -= Time.deltaTime;
            }

        }

        public bool TryAttack()
        {
            if (!m_isInitialized || !m_target) return false;
            if (m_cooldownTimer > 0) return false;

            m_count++;
            var spell = m_spells.FirstOrDefault(spell => spell.count == m_count);

            if (m_count >= m_maxCount)
            {
                m_count = 0;
            }

            if (spell.spell is null)
            {
                m_spellCaster.Cast(m_defaultSpell, m_target.position);
            }
            else
            {
                m_spellCaster.Cast(spell.spell, m_target.position);
            }
            m_cooldownTimer = m_attackTime;

            return true;
        }

    }
}
