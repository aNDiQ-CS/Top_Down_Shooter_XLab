using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Players
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(NavMeshMouseResolver))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerConfig m_config;
        [SerializeField] private PlayerMovement m_playerMovement;
        [SerializeField] private NavMeshMouseResolver m_navMeshMouseResolver;

        private Camera m_camera;

        private void OnValidate()
        {
            if (!m_playerMovement)
            {
                m_playerMovement = GetComponent<PlayerMovement>();
            }

            if (!m_navMeshMouseResolver)
            {
                m_navMeshMouseResolver = GetComponent<NavMeshMouseResolver>();
            }
        }

        private void Start()
        {
            m_playerMovement.Initialize(m_config.speed);
            m_navMeshMouseResolver.Initialize(Camera.main);
        }

        private void Update()
        {
            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                Vector3 mousePos = Mouse.current.position.ReadValue();
                Vector3? navPoint = m_navMeshMouseResolver.GetNavMeshPoint(mousePos);

                if (navPoint.HasValue) 
                {
                    m_playerMovement.SetDestination(navPoint.Value);
                }
                
            }
        }
    }
}

