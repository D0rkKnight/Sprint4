using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace uClicker
{
    public class BuildingSorter : MonoBehaviour, IComparer<ScriptableObject>
    {
        public bool sortByCost = true;

        public int Compare(ScriptableObject x, ScriptableObject y)
        {
            if (sortByCost)
            {
                return (x as Building).Cost.Amount.CompareTo((y as Building).Cost.Amount);
            }
            else
            {
                return x.name.CompareTo(y.name);
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}