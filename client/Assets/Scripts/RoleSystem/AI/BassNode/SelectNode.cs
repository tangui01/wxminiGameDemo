
using System.Collections.Generic;

namespace BTAI
{
    public class SelectNode : BassNode
    {
        public override bool OnEnter(float fdt)
        {
            foreach (var item in _childrent)
            {
                var isRet = item.OnEnter(fdt);

                if(isRet)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
