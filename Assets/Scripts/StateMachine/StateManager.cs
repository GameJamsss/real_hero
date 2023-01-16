using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StateMachine
{
    public class StateManager<T> : MonoBehaviour
    {
        private List<AbstractState<T>> States;
        public T Entity;

        public StateManager(T entity)
        {
            Entity = entity;
            States = new List<AbstractState<T>>();
        }
        public StateManager(T entity, params AbstractState<T>[] states)
        {
            Entity = entity;
            new List<AbstractState<T>>(states).ForEach(s => AddState(s));
        }
        public StateManager(T entity, List<AbstractState<T>> states)
        {
            Entity = entity;
            states.ForEach(s => AddState(s));
        }
        public bool GetNewState(ref AbstractState<T> current)
        {
            if (!current.Lock)
            {
                ulong priority = 0;
                AbstractState<T> previous = current;
                switch (current.WhiteList.Count != 0 ? 1 : current.BlackList.Count != 0 ? -1 : 0)
                {
                    case 1:
                        foreach (AbstractState<T> state in States)
                            if (previous.WhiteList.Exists(s => s.Name == state.Name)
                                && (state.TransitFrom.Count == 0 || state.TransitFrom.Contains(previous))
                                && state.EnterCondition(Entity)
                                && priority < state.Priority)
                            {
                                priority = state.Priority;
                                current = state;
                            }
                        break;
                    case -1:
                        foreach (AbstractState<T> state in States)
                        {
                            if (previous.BlackList.TrueForAll(s => s.Name != state.Name)
                                && (state.TransitFrom.Count == 0 || state.TransitFrom.Contains(previous))
                                && state.EnterCondition(Entity)
                                && priority < state.Priority)
                            {
                                priority = state.Priority;
                                current = state;
                            }
                        }
                        break;
                    case 0:
                        foreach (AbstractState<T> state in States)
                        {
                            if (state.EnterCondition(Entity)
                                && (state.TransitFrom.Count == 0 || state.TransitFrom.Contains(previous))
                                && priority < state.Priority)
                            {
                                priority = state.Priority;
                                current = state;
                            }
                        }
                        break;
                }
                return previous.Name != current.Name;
            }
            else 
            {
                return false;
            }
        }
        public StateManager<T> AddState(AbstractState<T> newState)
        {
            foreach (AbstractState<T> state in States)
            {
                if (state.Name.Equals(newState.Name))
                {
                    Debug.Log("State with name: " + newState.Name + " already exists");
                    return this;
                }
                if (state.Priority == newState.Priority)
                {
                    Debug.Log("State with priority: " + newState.Priority + " already exists");
                    return this;
                }
            }
            States.Add(newState);
            return this;
        }

        public State<T> NewStateInstance(string name, ulong priority)
        {
            State<T> newState = new State<T>(name, priority);
            AddState(newState);
            return newState;
        }
        public State<T> NewStateInstance(string name, ulong priority, Action<T> stateLogic)
        {
            State<T> newState = new State<T>(name, priority, stateLogic);
            AddState(newState);
            return newState;
        }
        public State<T> NewStateInstance(string name, ulong priority, Action<T> stateLogic, Func<T, bool> enterCondition)
        {
            State<T> newState = new State<T>(name, priority, stateLogic, enterCondition);
            AddState(newState);
            return newState;
        }
        public State<T> NewStateInstance(string name, ulong priority, Action<T> stateLogic, Action<T> onStateEnter, Action<T> onStateExit)
        {
            State<T> newState = new State<T>(name, priority, stateLogic, onStateEnter, onStateExit);
            AddState(newState);
            return newState;
        }
        public State<T> NewStateInstance(string name, ulong priority, Action<T> stateLogic, Action<T> onStateEnter, Action<T> onStateExit, Func<T, bool> enterCondition)
        {
            State<T> newState = new State<T>(name, priority, stateLogic, onStateEnter, onStateExit, enterCondition);
            AddState(newState);
            return newState;
        }
    }
}
