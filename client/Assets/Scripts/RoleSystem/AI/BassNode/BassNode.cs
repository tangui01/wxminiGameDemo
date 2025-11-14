
using System.Collections.Generic;

namespace BTAI
{
    public class BassNode
    {
        protected AIMgr target = null;

        protected BassNode parent = null;

        protected List<BassNode> _childrent = new List<BassNode>();

        public virtual void SetTarget(AIMgr target)
        {
            this.target = target;
        }

        public virtual void SetParent(BassNode parent)
        {
            this.parent = parent;
        }

        public virtual void InputChild(BassNode child)
        {
            _childrent.Add(child);
        }

        public virtual bool OnEnter(float fdt)
        {
            return true;
        }

        public virtual void Clear()
        {
            foreach (var item in _childrent)
            {
                item.Clear();
            }

            _childrent.Clear();
        }
    }
}
