using UnityEngine;

namespace Players
{   
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Player Config")]
    public sealed class PlayerConfig : ScriptableObject
    {
        [SerializeField] private Texture2D m_cursorTexture;
        [SerializeField][Range(0, 100)] private float m_speed = 5;
        [SerializeField][Min(0)] private float m_angularSpeed = 500f;

        public float speed => m_speed;
        public Texture2D cursorTexture => m_cursorTexture;
        public float angularSpeed => m_angularSpeed;
    }
}

