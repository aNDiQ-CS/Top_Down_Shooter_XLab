using Entities.Enemies.Data;
using System;
using UnityEngine;

namespace Entities.Enemies
{
    internal class Enemy : MonoBehaviour
    {
        public event Action<Enemy> Died;

        [SerializeField] private EnemyData m_enemyData;
        private EnemyData m_data;

        [SerializeField] private HealthComponent m_health;
        // TODO: Add HealthComponent
        // TODO: Add Movement
        // TODO: Add AttackComponent

        public HealthComponent health => m_health;

        private void Awake()
        {
            Initialize(m_enemyData);
        }

        private void OnEnable()
        {
            m_health.ValueChanged += () =>
            {
                Debug.Log($"Health Changed: {m_health.value}");
            };

            m_health.Died += OnDied;
        }

        private void OnDisable()
        {
            m_health.Died -= OnDied;
        }

        public void Initialize(EnemyData data)
        {
            m_data = data;
            m_health.Initialize(data.health);
        }

        private void OnDied()
        {
            Died?.Invoke(this);
        }
    }
}
