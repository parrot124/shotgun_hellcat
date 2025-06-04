using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class Snapshoter : MonoBehaviour
{
    private ISnapshotable snapshotable;
    private List<PropertyInfo> fields;
    
    private void Awake()
    {
        snapshotable = GetComponent<ISnapshotable>();
        fields = snapshotable.GetType().GetProperties(BindingFlags.Instance | BindingFlags.NonPublic).
            Where(x=>Attribute.IsDefined(x, typeof(SnapshotableField))).ToList();
    }
}
