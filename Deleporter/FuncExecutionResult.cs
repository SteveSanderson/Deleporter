using System;

namespace DeleporterCore
{
    [Serializable]
    internal class FuncExecutionResult<T>
    {
        public SerializableDelegate<Func<T>> DelegateCalled { get; set; }
        public T DelegateCallResult { get; set; }
    }
}