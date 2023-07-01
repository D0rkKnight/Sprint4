using System.Collections.Generic;
using UnityEngine;

// SO for generable manifest
[CreateAssetMenu(fileName = "SOManifest", menuName = "Sprint4/SOManifest", order = 0)]
public class SOManifest : ScriptableObject {
    public List<ScriptableObject> items;
}