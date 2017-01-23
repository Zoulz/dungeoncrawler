using strange.extensions.signal.impl;
using UnityEngine;

namespace trailmarch.view
{
    public class InputHandlerView : BaseView
    {
        public Signal<int> InputSignal { get { return _inputSignal; } }
        private Signal<int> _inputSignal = new Signal<int>();

        public override void Init()
        {
        }

        public override void Dispose()
        {
        }

        public void Update()
        {
            if (Input.GetKey(KeyCode.W))
            {
                _inputSignal.Dispatch(1);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                _inputSignal.Dispatch(2);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                _inputSignal.Dispatch(3);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                _inputSignal.Dispatch(4);
            }
        }
    }
}
