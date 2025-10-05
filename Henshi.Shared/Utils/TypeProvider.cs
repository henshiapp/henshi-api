using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Henshi.Web.Utils;

class TypeProvider
{
    public static IReadOnlyList<Assembly> GetReferencedAssemblies(Assembly? rootAssembly)
    {
        var visited = new HashSet<string>();
        var queue = new Queue<Assembly?>();
        var listResult = new List<Assembly>();

        var root = rootAssembly ?? Assembly.GetEntryAssembly();
        queue.Enqueue(root);

        do
        {
            var asm = queue.Dequeue();

            if (asm == null)
                break;

            listResult.Add(asm);

            foreach (var reference in asm.GetReferencedAssemblies())
            {
                if (!visited.Contains(reference.FullName))
                {
                    // Load will add assembly into the application domain of the caller. loading assemblies explicitly to AppDomain, because assemblies are loaded lazily
                    // https://learn.microsoft.com/en-us/dotnet/api/system.reflection.assembly.load
                    queue.Enqueue(Assembly.Load(reference));
                    visited.Add(reference.FullName);
                }
            }
        } while (queue.Count > 0);

        return listResult.Distinct().ToList().AsReadOnly();
    }

    public static IReadOnlyList<Assembly> GetApplicationPartAssemblies(Assembly rootAssembly)
    {
        var rootNamespace = rootAssembly.GetName().Name!.Split('.').First();
        var list = rootAssembly!.GetCustomAttributes<ApplicationPartAttribute>()
            .Where(x => x.AssemblyName.StartsWith(rootNamespace, StringComparison.InvariantCulture))
            .Select(name => Assembly.Load(name.AssemblyName))
            .Distinct();

        return list.ToList().AsReadOnly();
    }
}
