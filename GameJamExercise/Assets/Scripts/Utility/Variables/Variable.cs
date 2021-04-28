using UnityEngine;
using UnityEngine.Events;

namespace OsukaCreative.Utility.Variables {

    public abstract class BaseVariable<T> : ScriptableObject {
        [SerializeField]
        private T value;
        public T Value {
            get { return this.value; }
            set {
                this.value = value;
                this.onChange.Invoke();
            }
        }

        [SerializeField]
        public UnityEvent onChange = new UnityEvent();

    }

    public abstract class BaseReference<T, TVar> where TVar : BaseVariable<T> {
        [SerializeField]
        private bool useConstant = true;
        [SerializeField]
        private T constantValue;
        [SerializeField]
        private TVar variable;

        public T Value {
            get { return this.useConstant ? this.constantValue : this.variable.Value; }
            set {
                if (this.useConstant) {
                    this.constantValue = value;
                } else {
                    this.variable.Value = value;
                }
            }
        }

    }

}
