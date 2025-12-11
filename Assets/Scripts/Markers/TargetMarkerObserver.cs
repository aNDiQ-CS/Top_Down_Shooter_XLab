using Players;
using UnityEngine;

namespace Markers
{
    public sealed class TargetMarkerObserver : MonoBehaviour
    {
        // Ссылка на маркер и движение игрока
        [SerializeField] private TargetMarker m_targetMarker;
        [SerializeField] private PlayerMovement m_playerMovement;

        private void OnEnable()
        {
            // Подписываемся на остановку и изменение цели.
            m_playerMovement.Stopped += OnPlayerStopped;
            m_playerMovement.DestinationChanged += OnDestinationChanged;
        }

        private void OnDisable()
        {
            // Не забываем отписаться.
            m_playerMovement.Stopped -= OnPlayerStopped;
            m_playerMovement.DestinationChanged -= OnDestinationChanged;
        }

        // Причим маркер при остановки игрока.
        private void OnPlayerStopped() =>
            m_targetMarker.Hide();

        // Показываем маркер при достижение цели.
        private void OnDestinationChanged(Vector3 worldPosition) =>
            m_targetMarker.Show(worldPosition);
    }
}