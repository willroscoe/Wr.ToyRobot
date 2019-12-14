using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;

namespace Wr.ToyRobot.CoreLib.Helpers
{
    public static class Extensions
    {
        public static T CloneObjectSerializable<T>(this T obj) where T : class
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, obj);
            ms.Position = 0;
            object result = bf.Deserialize(ms);
            ms.Close();
            return (T)result;
        }

        /// <summary>
        /// Perform a deep Copy of the object, using Json as a serialisation method. NOTE: Private members are not cloned using this method.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static T CloneObject<T>(this T source)
        {
            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            // initialize inner objects individually
            // for example in default constructor some list property initialized with some values,
            // but in 'source' these items are cleaned -
            // without ObjectCreationHandling.Replace default constructor values will be added to result
            //var deserializeSettings = new JsonSerializerOptions() {   =  } Json JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };

            return JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(source));
        }
    }
}
