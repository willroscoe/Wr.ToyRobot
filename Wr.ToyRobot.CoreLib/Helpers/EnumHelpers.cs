using System;

namespace Wr.ToyRobot.CoreLib.Helpers
{
    public static class EnumHelpers
    {
        
        /// <summary>
        /// Gets the custom attribute data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns>T</returns>
        public static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);

            return attributes.Length > 0
              ? (T)attributes[0]
              : null;
        }
    }
}
