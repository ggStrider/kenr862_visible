using System;
using System.Collections.Generic;
using Internal.Core.Inputs;
using Internal.Core.UI.Bases;
using UnityEngine;
using Zenject;

namespace Internal.Core.UI
{
    public class EscManager : IInitializable, IDisposable
    {
        private readonly Stack<IEscClosable> _stack = new();
        private InputReader _inputReader;

        [Inject]
        private void Construct(InputReader inputReader)
        {
            _inputReader = inputReader;
        }

        public void Initialize()
        {
            _inputReader.OnEscPressedInput += HandleEsc;
        }

        public void Dispose()
        {
            if (_inputReader != null)
            {
                _inputReader.OnEscPressedInput += HandleEsc;
            }
        }

        public void AddToStack(IEscClosable toAdd)
        {
            _stack.Push(toAdd);
        }

        public void HandleEsc()
        {
            if (_stack.Count == 0)
            {
                Debug.Log("Should Open Settings. Thats TODO");
            }
            else
            {
                var newest = _stack.Pop();
                newest.Close();
            }
        }
    }
}