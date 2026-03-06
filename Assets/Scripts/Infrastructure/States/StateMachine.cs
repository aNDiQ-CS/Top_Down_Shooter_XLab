    using Entities.Enemies;
using Markers;
using Players;
using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace Infrastructure.States.Legacy
{
    public class StateMachine : MonoBehaviour
    {
        private IState m_state;
        private Dictionary<Type, IState> m_states = new();

        public void Initialize(params IState[] states)
        {
            if (m_states.Count > 0) return;

            foreach(var state in states)
            {
                m_states.Add(state.GetType(), state);
            }
        }

        public void ChangeState<T>()
            where T: IState
        {
            m_state?.Exit();
            {
                m_state = m_states[typeof(T)];
            }
            m_state.Enter();
        }

        private void Update()
        {
            
        }
    }

    public interface IState
    {
        public void Enter();
        public void Update() { }
        public void Exit();
    }

    public class PauseMenuState : IState
    {
        private readonly StateMachine m_stateMachine;
        private readonly Loading m_loading;
        private readonly PauseMenuView m_pauseMenuView;

        public PauseMenuState(StateMachine stateMachine,
            Loading loading, PauseMenuView pauseMenuView)
        {
            m_stateMachine = stateMachine;
        }

        public void Enter()
        {
            Time.timeScale = 0;
        }

        public void Exit() => throw new Exception();
    }

    public class GameplayState : IState
    {
        private readonly StateMachine m_stateMachine;
        private readonly EnemySpawner m_enemySpawner;
        private readonly PlayerController m_playerController;
        private readonly TargetMarkerObserver m_targetMarkerObserver;

        public GameplayState(
            StateMachine stateMachine,
            EnemySpawner enemySpawner,
            PlayerController playerController,
            TargetMarkerObserver targetMarkerObserver)
        {
            m_stateMachine = stateMachine;
            m_enemySpawner = enemySpawner;
            m_playerController = playerController;
            m_targetMarkerObserver = targetMarkerObserver;
        }

        public void Enter()
        {
            m_enemySpawner.Spawn();
            m_playerController.health.Died += OnDied;
        }

        public void Exit()
        {
            m_playerController.health.Died -= OnDied;
        }

        private void OnDied()
        {
            m_stateMachine.ChangeState<DeadState>();
        }
    }

    public class DeadState : IState
    {
        private readonly StateMachine m_stateMachine;
        private readonly DeadMenuView m_deadMenuView;

        public DeadState(StateMachine stateMachine, DeadMenuView deadMenuView)
        {
            m_stateMachine = stateMachine;
            m_deadMenuView = deadMenuView;

            deadMenuView.gameObject.SetActive(false);
        }
        public void Enter()
        {
            m_deadMenuView.GoToMenuClicked += OnGoToMenuClicked;
            m_deadMenuView.gameObject.SetActive(true);
        }

        public void Exit()
        {
            m_deadMenuView.GoToMenuClicked -= OnGoToMenuClicked;
            m_deadMenuView.gameObject.SetActive(false);
        }

        private void OnGoToMenuClicked()
        {
            SceneManager.LoadScene(GlobalConstants.Scenes.Main);
        }
    }
}