using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.StateMachine
{
    public class State<T> : AbstractState<T>
    {
        public Action<T> OnEnterD;
        public Action<T> InStateD;
        public Action<T> OnExitD;
        public Func<T, bool> EnterConditionD = _ => true;

        public State(string name, ulong priority)
        {
            Name = name;
            base.priority = priority;
        }
        public State(string name, ulong priority, Action<T> stateLogic)
        {
            Name = name;
            InStateD = stateLogic;
            base.priority = priority;
        }
        public State(string name, ulong priority, Action<T> stateLogic, Func<T, bool> enterCondition)
        {
            Name = name;
            InStateD = stateLogic;
            EnterConditionD = enterCondition;
            base.priority = priority;
        }
        public State(string name, ulong priority, Action<T> stateLogic, Action<T> onStateEnter, Action<T> onStateExit)
        {
            Name = name;
            InStateD = stateLogic;
            OnEnterD = onStateEnter;
            OnExitD = onStateExit;
            base.priority = priority;
        }
        public State(string name, ulong priority, Action<T> stateLogic, Action<T> onStateEnter, Action<T> onStateExit, Func<T, bool> enterCondition)
        {
            Name = name;
            InStateD = stateLogic;
            OnEnterD = onStateEnter;
            OnExitD = onStateExit;
            EnterConditionD = enterCondition;
            base.priority = priority;
        }
        override public void OnEnter(T entity)
        {
            OnEnterD?.Invoke(entity);
        }
        override public void InState(T entity)
        {
            InStateD?.Invoke(entity);
        }
        override public void OnExit(T entity)
        {
            OnExitD?.Invoke(entity);
        }
        override public bool EnterCondition(T entity)
        {
            if (EnterConditionD == null) return true;
            return EnterConditionD.Invoke(entity);
        }
        public State<T> SetName(string name)
        {
            Name = name;
            return this;
        }
        public State<T> SetOnStateEnter(Action<T> enterLogic)
        {
            OnEnterD = enterLogic;
            return this;
        }
        public State<T> SetStateLogic(Action<T> stateLogic)
        {
            InStateD = stateLogic;
            return this;
        }
        public State<T> SetOnStateExit(Action<T> onStateExit)
        {
            OnExitD = onStateExit;
            return this;
        }
        public new State<T> ToBlack(AbstractState<T> state)
        {
            base.ToBlack(state);
            return this;
        }
        public new State<T> ToWhite(AbstractState<T> state)
        {
            base.ToWhite(state);
            return this;
        }
        public State<T> SetEnterCondition(Func<T, bool> con)
        {
            EnterConditionD = con;
            return this;
        }
        public static State<T> GetEmpty()
        {
            return new State<T>("null_state", 0);
        }
    }
}
