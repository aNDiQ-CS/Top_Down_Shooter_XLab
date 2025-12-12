using UnityEngine;

namespace Magic.Elements
{
    [CreateAssetMenu(fileName = "AoESpellData", menuName = "XLab/Magic/Spells/AoESpellData")]
    public class AoESpellData : BaseSpellData
    {
        [SerializeField][Min(0f)] private float m_radius;
        public float radius => m_radius;
    }
}

