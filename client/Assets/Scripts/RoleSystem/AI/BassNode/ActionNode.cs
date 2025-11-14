
using System;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace BTAI
{
    public class ActionNode : BassNode
    {
        private Action _action;
        public ActionNode(Action action)
        {
            _action = action;
        }

        public override bool OnEnter(float fdt)
        {
            _action.Invoke();

            return true;
        }
    }
}
