using UnityEngine;
using Infrastructure.States;
using Entities.Enemies;
using UI;
using Players;
using Markers;
using Cameras;

namespace Infrastructure
{
    public class Bootstrap: MonoBehaviour
    {
        [SerializeField] private BootstrapState m_bootStrapState;
        [SerializeField] private DeadMenuView m_deadMenuView;
        [SerializeField] private SpawnerEnemy m_enemySpawner;
        [SerializeField] private PlayerController m_playerController;
        [SerializeField] private PlayerSpawnPoint m_spawnPoint;
        [SerializeField] private TargetMarkerObserver m_targetMarkerObserver;
        [SerializeField] private AimLineMarker m_aimLineMarker;
        [SerializeField] private CameraFollow m_cameraFollow;
        [SerializeField] private PauseMenuState m_pauseMenuState;
        private void Awake()
        {
            var stateMachine = new StateMachine();
            m_bootStrapState.Initialize(stateMachine);

            stateMachine.Initialize(
                m_bootStrapState,
                new PauseMenuState(stateMachine), 
                new DeadState(stateMachine, m_deadMenuView), 
                new GamePlayState(stateMachine, m_enemySpawner, m_playerController,
                m_targetMarkerObserver, m_aimLineMarker, m_cameraFollow));

            stateMachine.ChangeState<BootstrapState>();
        }
    }
}