using UnityEngine;

namespace Players
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Player Config")]
    public sealed class PlayerConfig : ScriptableObject
    {
        [SerializeField][Range(0, 100)] private float m_speed = 5;

        public float speed => m_speed;
    }
}

