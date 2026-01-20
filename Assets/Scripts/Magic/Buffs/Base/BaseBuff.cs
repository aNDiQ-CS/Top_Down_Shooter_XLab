using Magic.Buffs;
using System;
using UnityEngine;

namespace Magic.Buffs.Base
{
    [Serializable]
    public abstract class BaseBuff : IBuff
    {

        [field: SerializeField]
        public string Id { get; private set; }
        protected BuffContainer container { get; private set; }
        public void Initialize(BuffContainer container)
        {
            this.container = container;
        }

        public BaseBuff() { }

        protected BaseBuff(string id)
        {
            Id = id;
        }

        protected virtual void OnInitialize() { }

        public void Deinitialize()
        {
            OnDeinitializing();

            container.Remove(this);
            container = null;
        }

        protected virtual void OnDeinitializing() { }

        public virtual void Update(float deltaTime) { }

        abstract public object Clone();        
    }
}
