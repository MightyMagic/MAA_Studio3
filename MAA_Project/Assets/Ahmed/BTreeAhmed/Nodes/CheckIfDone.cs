namespace Ahmed.BTreeAhmed.Nodes
{
    public class CheckIfDone : BTNode
    {
        private bool _done;

        // public CheckIfDone(bool done)
        // {
        //     this._done = _done;
        // }
        public override BTNodeState Evaluate() // maybe I need an idle node
    
        {
            if (_done)
            {
                return BTNodeState.FAILURE;
            }
            else
            {
                _done = true;
                return BTNodeState.SUCCESS;
            }
        }
    }
}
