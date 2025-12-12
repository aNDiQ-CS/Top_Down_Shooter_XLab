using System.Collections.Generic;
using UnityEngine;

namespace Magic.Elements
{
    [CreateAssetMenu(fileName = "SpellsDatabase", menuName = "XLab/Magic/Spells/SpellsDatabase")]
    public sealed class SpellsDatabase : ScriptableObject
    {
        [SerializeReference] private BaseSpellData[] m_spells;

        public IReadOnlyList<BaseSpellData> spells => m_spells;
    }
}
