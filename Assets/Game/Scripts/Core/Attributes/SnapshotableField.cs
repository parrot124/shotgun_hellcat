using System;

namespace Game.Scripts.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class SnapshotableField : Attribute 
    {
        private object value;

        public SnapshotableField()
        {
            
        }
    }
}
