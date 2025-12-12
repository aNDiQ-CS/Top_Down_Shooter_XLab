using UnityEngine;

namespace Magic.Elements
{
    [CreateAssetMenu(fileName = "TargetSpellData", menuName = "XLab/Magic/Speels/TargetSpellData")]
    public class TargetSpellData : BaseSpellData
    {
        [SerializeField] private float m_speed;
        public float speed => m_speed;
    }
}

