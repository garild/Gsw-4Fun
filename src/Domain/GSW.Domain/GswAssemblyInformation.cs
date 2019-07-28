using System.Reflection;

namespace GSW.Domain
{
    public class GswAssemblyInformation
    {
        public static Assembly Assemblies { get; } = Assembly.GetExecutingAssembly();
    }
}
