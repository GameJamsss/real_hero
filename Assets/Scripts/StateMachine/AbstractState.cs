using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StateMachine
{
    
    public abstract class AbstractState<T> : MonoBehaviour
    {
        public List<AbstractState<T>> BlackList = new List<AbstractState<T>>();
        public List<AbstractState<T>> WhiteList = new List<AbstractState<T>>();
        public List<AbstractState<T>> TransitFrom = new List<AbstractState<T>>();

        private List<StateModifier<T>> Modifiers = new List<StateModifier<T>>();

        public bool Lock = false;
        public ulong Priority = 0;
        
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

        abstract protected void OnEnterLogic(T entity);
        abstract protected void OnUpdateLogic(T entity);
        abstract protected void OnExitLogic(T entity);
        public void OnEnter(T entity)
        {
            OnEnterLogic(entity);
            Modifiers.ForEach(m => m.EnterModify(entity));
        }
        public void OnUpdate(T entity)
        {
            OnUpdateLogic(entity);
            Modifiers.ForEach(m => m.UpdateModify(entity));
        }
        public void OnExit(T entity)
        {
            OnExitLogic(entity);
            Modifiers.ForEach(m => m.ExitModify(entity));
        }
        public void AddModifier(StateModifier<T> m)
        {
            Modifiers.Add(m);
        }
        virtual public bool EnterCondition(T entity)
        {
            return true;
        }
        public AbstractState<T> ToBlack(AbstractState<T> state)
        {
            BlackList.Add(state);
            return this;
        }
        public AbstractState<T> ToWhite(AbstractState<T> state)
        {
            WhiteList.Add(state);
            return this;
        }

        public AbstractState<T> ToTransitFrom(AbstractState<T> state)
        {
            TransitFrom.Add(state);
            return this;
        }
    }
    
}
