using System;
using UnityEngine;

namespace OsukaCreative.Utility.RangedVariables {
    [Serializable]
    public class RangedInt {
        [SerializeField]
        private int _min;
        [SerializeField]
        private int _max;

        public int Value { get { return UnityEngine.Random.Range(this._min, this._max + 1); } }

    }

}