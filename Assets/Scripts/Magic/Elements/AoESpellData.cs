using UnityEngine;

namespace Magic.Elements
{
    [CreateAssetMenu(fileName = "AoESpellData", menuName = "XLab/Magic/Spells/AoESpellData")]
    public class AoESpellData : BaseSpellData
    {
        [SerializeField] private bool m_isTarget;
        [SerializeField][Min(0f)] private float m_radius;
        public float radius => m_radius;
        public bool isTarget => m_isTarget;
    }
}

