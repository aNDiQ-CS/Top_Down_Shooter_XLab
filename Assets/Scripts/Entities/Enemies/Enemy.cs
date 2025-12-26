using Entities.Enemies.Data;
using Entities.Enemies.Systems;
using System;
using UnityEngine;

namespace Entities.Enemies
{
    internal class Enemy : MonoBehaviour
    {
        public event Action<Enemy> Died;

        [SerializeField] private AttackEnemy m_attack;                    
        private EnemyData m_data;
        private EnemyStateMachine m_stateMachine;
        private Transform m_playerTransform;
        [SerializeField] private HealthComponent m_health;
        // TODO: Add HealthComponent
        // TODO: Add Movement
        // TODO: Add AttackComponent                

        private void OnEnable()
        {
            m_health.ValueChanged += () =>
            {
                Debug.Log($"Health Changed: {m_health.value}");
            };

            m_health.Died += OnDied;
        }

        private void Update()
        {
            if (m_stateMachine.currentState is EnemyState.Dead || !m_data) return;

            UpdateState();
        }

        public void Initialize(EnemyData data, Transform playerTransform)
        {
            m_data = data;
            m_health.Initialize(data.health);
            m_attack.Initialize(data.spell, playerTransform, data.attackTime);

            m_stateMachine ??= new EnemyStateMachine();
        }

        private void UpdateState()
        {
            bool isInAttackRange = IsInRange();

            switch (m_stateMachine.currentState)
            {
                case EnemyState.Idle: HandleIdleState(isInAttackRange); break;
                case EnemyState.Attack: HandleAttackState(isInAttackRange); break;
            }
        }

        private void HandleIdleState(bool isInAttackRange)
        {
            if (m_data.enemyType == AttackEnemyType.Range && isInAttackRange)
            {
                m_stateMachine.ChangeState(EnemyState.Attack);
            }
        }

        private void HandleAttackState(bool isInAttackRange)
        {
            m_attack.TryAttack();

            if (!isInAttackRange)
            {
                if (m_data.enemyType == AttackEnemyType.Melee)
                {
                    m_stateMachine.ChangeState(EnemyState.Move);
                }
                else
                {
                    m_stateMachine.ChangeState(EnemyState.Idle);
                }
                
            }
        }        

        private bool IsInRange()
        {
            if (!m_playerTransform) return false;

            var distance = Vector3.Distance(transform.position, m_playerTransform.position);
            return distance <= m_data.attackTime;
        }

        private void OnDisable()
        {
            m_health.Died -= OnDied;
        }

        private void OnDied()
        {
            Died?.Invoke(this);
        }
    }
}
