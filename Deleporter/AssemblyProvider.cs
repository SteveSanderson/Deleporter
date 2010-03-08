using System;
using System.IO;
using System.Linq;

namespace DeleporterCore
{
    internal class AssemblyProvider : MarshalByRefObject
    {
        public byte[] GetAssembly(string assemblyName)
        {
            try
            {
                // If the assembly is already loaded, use that
                var matchingAssembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.FullName == assemblyName);
                // Otherwise, look on disk
                if (matchingAssembly == null)
                    matchingAssembly = AppDomain.CurrentDomain.Load(assemblyName);
                // Now return the assembly bytes
                if (matchingAssembly != null && matchingAssembly.Location != null)
                    return File.ReadAllBytes(matchingAssembly.Location);
            }
            catch (FileNotFoundException) { /* Means the assembly wasn't known */ }

            return null;
        }

        public override object InitializeLifetimeService()
        {
            return null; // Don't expire this object
        }
    }
}