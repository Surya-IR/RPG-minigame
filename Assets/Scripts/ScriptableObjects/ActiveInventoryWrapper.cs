using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActiveInventoryWrapper
{
    public List<ActiveItem> list;
}

[Serializable]
public class ActiveItem
{
    public string id;
    public int amount;
}