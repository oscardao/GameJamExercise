using System;
using UnityEngine;

namespace OsukaCreative.Utility.RangedVariables {
    [Serializable]
    public class RangedFloat {
        [SerializeField]
        private float _min;
        [SerializeField]
        private float _max;

        public float Value { get { return UnityEngine.Random.Range(this._min, this._max); } }

    }

}