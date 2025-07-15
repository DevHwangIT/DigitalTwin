using System;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Machine : MonoBehaviour
{
    [Header("- Link")]
    [SerializeField] private List<Link> links = new List<Link>();
    public List<Link> Links => links;
}
