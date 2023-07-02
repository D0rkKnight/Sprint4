using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using uClicker;

[CreateAssetMenu(fileName = "BuildingsProfile", menuName = "Sprint4/BuildingsProfile", order = 0)]
public class BuildingsProfile : ScriptableObject
{
    public Building[] buildings;
}