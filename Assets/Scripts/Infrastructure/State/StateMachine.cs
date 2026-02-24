using Cameras;
using Entities.Enemies;
using Markers;
using Players;
using System;
using System.Collections.Generic;
using System.Data;
using UI;
using UnityEngine;
namespace Infrastructure.States
{
    public class StateMachine
    {
        private IState m_state;
        private Dictionary<Type, IState> m_states = new();

        public void Initialize(params IState[] states)
        {
            if (m_states.Count > 0) return;

            foreach (var state in states)
            {
                m_states.Add(state.GetType(), state);
            }

        }

        public void ChangeState<T>() where T: IState
        {
            m_state?.Exit();
            m_state = m_states[typeof(T)];
            m_state.Exit();
        }
    }

    public interface IState
    {
        public void Enter();
        public void Exit();
    }

    public class MainMenuState : IState
    {
        private readonly StateMachine m_stateMachine;
        private readonly MainMenuView m_mainMenuView;
        public MainMenuState(StateMachine stateMachine, MainMenuView mainMenuView)
        {
            m_stateMachine = stateMachine;
            m_mainMenuView = mainMenuView;

            m_mainMenuView.gameObject.SetActive(false);
        }

        public void Enter()
        {
            m_mainMenuView.gameObject.SetActive(true);
            m_mainMenuView.PlayClicked += OnPlayClicked;
            m_mainMenuView.ExitClicked += OnExitClicked;
        }

        private void OnExitClicked()
        {
            m_stateMachine.ChangeState<GamePlayState>();
        }

        private void OnPlayClicked()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
#endif

            Application.Quit();
        }

        public void Exit()
        {
            m_mainMenuView.PlayClicked -= OnPlayClicked;
            m_mainMenuView.ExitClicked -= OnExitClicked;
            m_mainMenuView.gameObject.SetActive(false);
        }
    }

    public class GamePlayState : IState
    {
        private readonly Vector3 m_playerPosition;
        private readonly SpawnerEnemy m_spawnerEnemy;
        private readonly StateMachine m_stateMachine;
        private readonly PlayerController m_playerController;
        private readonly TargetMarkerObserver m_targetMarkerObserver;
        private readonly AimLineMarker m_aimLineMarker;
        private readonly CameraFollow m_cameraFollow;

        public GamePlayState(StateMachine stateMachine, 
            SpawnerEnemy spawnerEnemy,
            PlayerController playerController,
            TargetMarkerObserver targetMarkerObserver,
            AimLineMarker aimlineMarker,
            CameraFollow cameraFollow)
        {
            m_spawnerEnemy = spawnerEnemy;
            m_stateMachine = stateMachine;
            m_playerController = playerController;            
            m_targetMarkerObserver = targetMarkerObserver;
            m_aimLineMarker = aimlineMarker;
            m_cameraFollow = cameraFollow;
        }
        public void Enter()
        {
            var playerPosition = ServiceLocator.Resolve<PlayerSpawnPoint>();
            ServiceLocator.Resolve<IPlayerFactorySettings>().position = m_playerPosition;
            ServiceLocator.Resolve<PlayerFactory>().Create();
            

            m_spawnerEnemy.Spawn();
            m_playerController.health.Died += OnDiedChanged;
        }

        private void OnDiedChanged()
        {
            m_stateMachine.ChangeState<DeadState>();
        }

        public void Exit()
        {
            m_playerController.health.Died -= OnDiedChanged;
        }
    }

    public class PauseMenuState : IState
    {
        private readonly StateMachine m_stateMachine;
        public PauseMenuState(StateMachine stateMachine)
        {
            m_stateMachine = stateMachine;
        }
        public void Enter() => throw new NotImplementedException();

        public void Exit() => throw new NotImplementedException();
    }

    public class DeadState : IState
    {
        private readonly StateMachine m_stateMachine;
        private readonly DeadMenuView m_deadMenuView;
        public DeadState(StateMachine stateMachine, DeadMenuView deadMenuView)
        {
            m_stateMachine = stateMachine;
            m_deadMenuView = deadMenuView;
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
            m_stateMachine.ChangeState<MainMenuState>();
        }
    }

}

