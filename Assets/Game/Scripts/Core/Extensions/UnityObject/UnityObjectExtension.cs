using UnityEngine;

namespace Game.Scripts.Extensions
{
    //UnityObject InstanceID to EntityID converter extension
    public static class UnityObjectExtension
    {
        public static long GetID(this Object unityObject)
        {
            return (long)EntityId.ToULong(unityObject.GetEntityId());
        }
        
        public static int GetID32(this Object unityObject)
        {
            return (int)GetID(unityObject);
        }
        
    }
}
