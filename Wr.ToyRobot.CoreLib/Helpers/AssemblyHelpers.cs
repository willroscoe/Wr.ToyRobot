using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wr.ToyRobot.CoreLib.Models.GridItems;

namespace Wr.ToyRobot.CoreLib.Helpers
{
    public static class AssemblyHelpers
    {
        public static void GetAllGridItemTypes()
        {

            

            int sdfsdf = 0;

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetEntryAssembly();

            foreach (System.Reflection.TypeInfo ti in assembly.DefinedTypes.Where(x => x.IsClass))
            {
                if (ti.ImplementedInterfaces.Contains(typeof(IGridItem)) && ti.IsSubclassOf(typeof(GridItemBase)))
                {
                    int sdf = 0;// yield return (IGridItem)assembly.CreateInstance(ti.FullName);
                }
            }

        }

        private static IEnumerable<T> GetAllTypesOf<T>()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetEntryAssembly();

            foreach (System.Reflection.TypeInfo ti in assembly.DefinedTypes.Where(x => x.IsClass))
            {
                if (ti.ImplementedInterfaces.Contains(typeof(T)) && ti.IsSubclassOf(typeof(GridItemBase)))
                {
                    yield return (T)assembly.CreateInstance(ti.FullName);
                }
            }

            /*foreach (Type type in Assembly.GetAssembly(typeof(T)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract))// && myType.IsSubclassOf(typeof(BaseImporter))))
            {
                yield return (T)Activator.CreateInstance(type);
            }*/

        }

    }
}
