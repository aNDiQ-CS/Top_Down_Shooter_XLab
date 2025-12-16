using Assets.Scripts.Magic.Systems;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Players
{
    [RequireComponent(typeof(PlayerMovement))]    
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerConfig m_config;
        [SerializeField] private PlayerMovement m_playerMovement;
        [SerializeField] private MouseResolver m_navMeshMouseResolver;
        [SerializeField] private MagicInputHelper m_magicInputHelper;


        private Camera m_camera;
        private PlayerRotationCalculator m_playerRotationCalculator;

        private void OnValidate()
        {
            if (!m_playerMovement)
            {
                m_playerMovement = GetComponent<PlayerMovement>();
            }

            if (!m_navMeshMouseResolver)
            {
                m_navMeshMouseResolver = GetComponent<MouseResolver>();
            }
        }

        private void Start()
        {
            m_playerMovement.Initialize(m_config.speed, m_config.angularSpeed);
            m_navMeshMouseResolver.Initialize(Camera.main);
            m_playerRotationCalculator = new PlayerRotationCalculator(Camera.main, transform);
            
            SetupCursor();
        }

        private void Update()
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            var lookPoint = m_playerRotationCalculator.Calculate(mousePos);
            m_playerMovement.RotateTowards(lookPoint);

            if (Mouse.current.rightButton.wasPressedThisFrame)
            {                
                Vector3? navPoint = m_navMeshMouseResolver.GetNavMeshPoint();

                if (navPoint.HasValue) 
                {
                    m_playerMovement.SetDestination(navPoint.Value);
                }
                
            }

            m_magicInputHelper.Update();
        }

        private void SetupCursor()
        {
            var texture = m_config.cursorTexture;

            if (texture)
            {
                var hotspot = new Vector2(texture.width / 2, texture.height / 2);
                Cursor.SetCursor(texture, hotspot, CursorMode.Auto);
            }
        }
    }
}

