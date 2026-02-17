using Entities.Enemies;
using Infrastructure.States;
using Players;
using UI;
using UnityEngine;

namespace Infrastructure
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private MainMenuView m_mainMenuView;
        [SerializeField] private SpawnerEnemy m_enemySpawner;
        [SerializeField] private PlayerController m_playerController;
        [SerializeField] private DeadMenuView m_deadMenuView;

        private void Awake()
        {
            var stateMachine = new StateMachine();

            stateMachine.Initialize(
                new MainMenuState(stateMachine, m_mainMenuView), 
                new PauseMenuState(stateMachine), 
                new DeadState(stateMachine, m_deadMenuView), 
                new GamePlayState(stateMachine, m_enemySpawner, m_playerController)); 

            stateMachine.ChangeState<MainMenuState>();
        }
    }
}
