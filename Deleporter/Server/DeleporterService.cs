using System;
using System.Reflection;

namespace DeleporterCore.Server
{
    internal class DeleporterService : MarshalByRefObject
    {
        public void RegisterAssemblyProvider(AssemblyProvider assemblyProvider)
        {
            AppDomain.CurrentDomain.AssemblyResolve += delegate(object sender, ResolveEventArgs args) {
                byte[] assemblyData = assemblyProvider.GetAssembly(args.Name);
                return assemblyData != null ? Assembly.Load(assemblyData) : null;
            };
        }

        public SerializableDelegate<Action> ExecuteAction(SerializableDelegate<Action> serializableDelegate)
        {
            serializableDelegate.Delegate();
            return serializableDelegate;
        }

        public FuncExecutionResult<T> ExecuteFunction<T>(SerializableDelegate<Func<T>> serializableDelegate)
        {
            var delegateResult = serializableDelegate.Delegate();
            return new FuncExecutionResult<T> {
                DelegateCalled = serializableDelegate,
                DelegateCallResult = delegateResult
            };
        }

        public override object InitializeLifetimeService()
        {
            return null; // Don't expire this object
        }
    }
}