using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.StateMachine
{
    public class StateManager<T>
    {
        private HashSet<AbstractState<T>> States;
        public T Entity;

        public StateManager(T entity)
        {
            Entity = entity;
            States = new HashSet<AbstractState<T>>();
        }
        public StateManager(T entity, params AbstractState<T>[] states)
        {
            Entity = entity;
            States = new HashSet<AbstractState<T>>(states);
        }
        public StateManager(T entity, HashSet<AbstractState<T>> states)
        {
            Entity = entity;
            States = states;
        }
        public bool GetNewState(ref AbstractState<T> current)
        {
            ulong priority = 0;
            AbstractState<T> previous = current;
            int rule = current.WhiteSet.Count != 0 ? 1 : current.BlackSet.Count != 0 ? -1 : 0;
            switch (rule)
            {
                case 1:
                    foreach (AbstractState<T> state in States)
                        if (current.WhiteSet.Contains(state) && state.EnterCondition(Entity) && priority < state.priority)
                        {
                            priority = state.priority;
                            current = state;
                        }
                    break;
                case -1:
                    foreach (AbstractState<T> state in States)
                        if (!current.BlackSet.Contains(state) && state.EnterCondition(Entity) && priority < state.priority)
                        {
                            priority = state.priority;
                            current = state;
                        }
                    break;
                case 0:
                    foreach (AbstractState<T> state in States)
                        if (state.EnterCondition(Entity) && priority < state.priority)
                        {
                            priority = state.priority;
                            current = state;
                        }
                    break;
            }
            return previous != current;
        }
        public StateManager<T> AddState(AbstractState<T> newState)
        {
            foreach (AbstractState<T> state in States)
            {
                if (state.Name.Equals(newState.Name))
                {
                    Console.WriteLine("State with name \"" + newState.Name + "\" already exists");
                    return this;
                }
                if (state.priority == newState.priority)
                {
                    Console.WriteLine("State with priority \"" + newState.priority + "\" already exists");
                    return this;
                }
            }
            States.Add(newState);
            return this;
        }

        public State<T> StateNewInstanse(string name, ulong priority)
        {
            State<T> newState = new State<T>(name, priority);
            AddState(newState);
            return newState;
        }
        public State<T> StateNewInstanse(string name, ulong priority, Action<T> stateLogic)
        {
            State<T> newState = new State<T>(name, priority, stateLogic);
            AddState(newState);
            return newState;
        }
        public State<T> StateNewInstanse(string name, ulong priority, Action<T> stateLogic, Func<T, bool> enterCondition)
        {
            State<T> newState = new State<T>(name, priority, stateLogic, enterCondition);
            AddState(newState);
            return newState;
        }
        public State<T> StateNewInstanse(string name, ulong priority, Action<T> stateLogic, Action<T> onStateEnter, Action<T> onStateExit)
        {
            State<T> newState = new State<T>(name, priority, stateLogic, onStateEnter, onStateExit);
            AddState(newState);
            return newState;
        }
        public State<T> StateNewInstanse(string name, ulong priority, Action<T> stateLogic, Action<T> onStateEnter, Action<T> onStateExit, Func<T, bool> enterCondition)
        {
            State<T> newState = new State<T>(name, priority, stateLogic, onStateEnter, onStateExit, enterCondition);
            AddState(newState);
            return newState;
        }
    }
}
