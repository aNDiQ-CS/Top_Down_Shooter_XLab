using Magic.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Magic.Systems
{
    internal class SpellCaster
    {
        private Transform m_casterTransform;

        public SpellCaster(Transform casterTransform)
        {
            m_casterTransform = casterTransform;
        }

        public void Cast(BaseSpellData spell, Vector3 worldPosition)
        {
            if (!spell)
                return; 

            switch (spell)
            {
                case SelfSpellData selfSpell: CastSelf(selfSpell); break;
                case TargetSpellData targetSpell: CastTarget(targetSpell, worldPosition); break;
                case NonTargetSpellData nonTargetSpell: CastNonTarget(nonTargetSpell); break;
                case AoESpellData aoeSpell:
                    {
                        CastAoE(aoeSpell, aoeSpell.isTarget 
                            ? worldPosition
                            : m_casterTransform.position); 
                    }
                    break;
            }
        }

        private void CastSelf(SelfSpellData spell) { }
        private void CastTarget(TargetSpellData spell, Vector3 worldPosition) 
        {
            Debug.Log("Casting" + spell.name + " to " + worldPosition);
        }
        private void CastNonTarget(NonTargetSpellData spell) { }
        private void CastAoE(AoESpellData spell, Vector3 worldPosition) { }        
    }
}
