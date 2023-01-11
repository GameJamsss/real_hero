using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StateMachine
{
    
    public abstract class AbstractState<T> : MonoBehaviour
    {
        public List<AbstractState<T>> BlackSet = new List<AbstractState<T>>();
        public List<AbstractState<T>> WhiteSet = new List<AbstractState<T>>();

        public List<StateModifier<T>> Modifiers = new List<StateModifier<T>>();

        public bool Lock = false;
        protected ulong _priority = 0;
        public ulong Priority
        {
            get => _priority;
            set
            {
                _priority = value + 1;
            }
        }
        public string Name;
        public void OnEnterModifier(T entity)
        {
            Modifiers.ForEach(modifier => modifier.EnterModify(entity));
        }
        public void UpdateModifier(T entity)
        {
            Modifiers.ForEach(modifier => modifier.UpdateModify(entity));
        }
        public void OnExitModifier(T entity)
        {
            Modifiers.ForEach(modifier => modifier.ExitModify(entity));
        }
        abstract public void OnEnter(T entity);
        abstract public void OnUpdate(T entity);
        abstract public void OnExit(T entity);
        virtual public bool EnterCondition(T entity)
        {
            return true;
        }
        public AbstractState<T> ToBlack(AbstractState<T> state)
        {
            BlackSet.Add(state);
            return this;
        }
        public AbstractState<T> ToWhite(AbstractState<T> state)
        {
            WhiteSet.Add(state);
            return this;
        }
    }
    
}
