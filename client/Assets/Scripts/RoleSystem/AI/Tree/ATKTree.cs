
using UnityEngine;

namespace BTAI
{
    public class ATKTree : BassTree
    {
        public static BassNode Create(AIMgr roleAIAddin)
        {
            var root = new SelectNode();

            var checkNode = new SequenceNode();
            checkNode.InputChild(new ConditionNode(roleAIAddin.IsAtkState));
            checkNode.InputChild(new ActionNode(roleAIAddin.ActAtk));

            root.InputChild(checkNode);

            var getNode = new SequenceNode();
            getNode.InputChild(new ConditionNode(roleAIAddin.IsNoTarget));
            getNode.InputChild(new ActionNode(roleAIAddin.SetTarget));

            root.InputChild(getNode);

            var move = new SequenceNode();
            move.InputChild(new ConditionNode(roleAIAddin.NoInAtkDistance));
            move.InputChild(new ActionNode(roleAIAddin.MoveToTarget));
            root.InputChild(move);

            var atk = new SequenceNode();
            atk.InputChild(new ConditionNode(roleAIAddin.IsAtkTime));
            atk.InputChild(new ActionNode(roleAIAddin.ActAtk));
            root.InputChild(atk);

            var idle = new SequenceNode();
            idle.InputChild(new ConditionNode(roleAIAddin.IsNoAtk));
            idle.InputChild(new ActionNode(roleAIAddin.ActIdle));
            root.InputChild(idle);
            //
            return root;
        }
    }
    
}
