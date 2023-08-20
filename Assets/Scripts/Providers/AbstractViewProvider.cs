using System.Collections.Generic;
using UnityEngine;

namespace RingInWater.View.Providers
{
    /// <summary>
    /// Провайдер представлений с ID, который выдает их по этим ID
    /// или сразу всех массивом.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AbstractViewProvider<T> : ScriptableObject where T: IViewWithId
    {
        [SerializeField] protected T[] views = new T[0];

        /// <summary>
        /// Использовать для ускорения выдачи представлений.
        /// </summary>
        protected Dictionary<int, T> viewsDictionary;
        /// <summary>
        /// Массив всех имеющихся представлений.
        /// </summary>
        public T[] viewsArray
        {
            get => this.views;
        }
        private void FillDictionary()
        {
            this.viewsDictionary = new Dictionary<int, T>(this.views.Length);
            foreach(T view in this.views)
            {
                if (this.viewsDictionary.ContainsKey(view.idInt))
                {
                    Debug.LogError($"Key dublicate in {this.name}!");
                }
                else
                {
                    this.viewsDictionary.Add(view.idInt, view);
                }
            }
        }

        /// <summary>
        /// Получить представленгие по его id.
        /// </summary>
        /// <param name="id"></param>
        public virtual T GetViewByID(int id)
        {
            if (this.viewsDictionary == null)
            {
                FillDictionary();
            }

            if(this.viewsDictionary.TryGetValue(id, out T view))
            {
                return view;
            }
            else
            {
                Debug.LogError($"View with id: {id} was not found!");
                return default;
            }
        }
    }
}