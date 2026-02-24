using Players;
using UnityEngine;

namespace Infrastructure.States
{
    public class BootstrapState : MonoBehaviour, IState
    {
        [SerializeField] private MouseResolver m_mouseResolver;
        [SerializeField] private PlayerSpawnPoint m_playerSpawnPoint;

        private StateMachine m_stateMachine;

        public void Initialize(StateMachine stateMachine)
        {
            m_stateMachine = stateMachine;
        }

        public void Enter()
        {
            var playerFactory = new PlayerFactory("Prefabs/Player");
            ServiceLocator.Register<PlayerFactory>(playerFactory);
            ServiceLocator.Register<PlayerSpawnPoint>(m_playerSpawnPoint);
            ServiceLocator.Register<MouseResolver>(m_mouseResolver);
            m_stateMachine.ChangeState<GamePlayState>();
        }

        public void Exit()
        {

        }
    }
}