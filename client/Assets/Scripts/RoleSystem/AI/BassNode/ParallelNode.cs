
using System.Collections.Generic;

namespace BTAI
{
    public class ParallelNode : BassNode
    {
        public override bool OnEnter(float fdt)
        {
            foreach (var item in _childrent)
            {
                item.OnEnter(fdt);
            }

            return true;
        }
    }
}
