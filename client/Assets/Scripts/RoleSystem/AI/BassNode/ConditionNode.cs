
using System;
using System.Collections.Generic;

namespace BTAI
{
    public class ConditionNode : BassNode
    {
        private Func<bool> _Func;

        public ConditionNode(Func<bool> func) 
        {
            _Func = func;
        }

        public override bool OnEnter(float fdt)
        {
            return _Func.Invoke();
        }
    }
}
