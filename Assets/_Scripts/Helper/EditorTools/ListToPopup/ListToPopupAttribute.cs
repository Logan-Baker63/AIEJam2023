using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListToPopupAttribute : PropertyAttribute
{
    public Type type;
    public string propertyName;

    public ListToPopupAttribute(Type _type, string _propertyName)
    {
        type = _type;
        propertyName = _propertyName;
    }
}
