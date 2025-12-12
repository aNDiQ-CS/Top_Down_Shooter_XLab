using UnityEngine;


namespace Magic.Elements
{
    [CreateAssetMenu(fileName = "MagicConfig", menuName = "XLab/Magic/Spells/MagicConfig")]
    public class MagicConfig : ScriptableObject
    {
        [SerializeField] private ElementsData m_elementsData;
        [SerializeField] private SpellsDatabase m_spellsDatabase;

        [SerializeField][Min(1)] private int m_maxElements = 3;
        [SerializeField][Min(0)] private float m_cancelCooldown = 0.3f;

        public ElementsData elementsData => m_elementsData;
        public SpellsDatabase spellsDatabase => m_spellsDatabase;
        public int maxElements => m_maxElements;

        public float cancelCooldown => m_cancelCooldown;
    }
}

