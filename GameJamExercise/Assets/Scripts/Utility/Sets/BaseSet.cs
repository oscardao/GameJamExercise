using System.Collections.Generic;
using UnityEngine;

namespace OsukaCreative.Utility.Sets {

    public abstract class BaseSet<T> : ScriptableObject {
        [SerializeField]
        private List<T> items = new List<T>();
        [SerializeField]
        private bool allowDuplicates = false;

        public int Length { get { return this.items.Count; } }

        public void Add(T item) {
            if (!this.items.Contains(item) || this.allowDuplicates) {
                this.items.Add(item);
            }
        }

        public void Remove(T item) {
            if (this.items.Contains(item)) {
                this.items.Remove(item);
            }
        }

        public T Get(int index) {
            return this.items[index];
        }

        public T RemoveAt(int index) {
            T item = this.items[index];
            this.items.RemoveAt(index);
            return item;
        }

        public bool Contains(T item) {
            return this.items.Contains(item);
        }

        public void Clear() {
            this.items.Clear();
        }

        public void Shuffle() {
            int count = this.Length;

            while (count > 1) {
                count--;
                int randomIndex = Random.Range(0, count - 1);

                T temporary = this.items[count];
                this.items[count] = this.items[randomIndex];
                this.items[randomIndex] = temporary;
            }
        }
    }

}