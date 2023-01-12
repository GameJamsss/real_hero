using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.StateMachine
{
    public class State<T> : AbstractState<T>
    {
        private Action<T> _onEnter;
        private Action<T> _inState;
        private Action<T> _onExit;
        private Func<T, bool> EnterConditionD = _ => true;

        public State(string name, ulong priority)
        {
            Name = name;
            base.Priority = priority;
        }
        public State(string name, ulong priority, Action<T> stateLogic)
        {
            Name = name;
            _inState = stateLogic;
            base.Priority = priority;
        }
        public State(string name, ulong priority, Action<T> stateLogic, Func<T, bool> enterCondition)
        {
            Name = name;
            _inState = stateLogic;
            EnterConditionD = enterCondition;
            base.Priority = priority;
        }
        public State(string name, ulong priority, Action<T> stateLogic, Action<T> onStateEnter, Action<T> onStateExit)
        {
            Name = name;
            _inState = stateLogic;
            _onEnter = onStateEnter;
            _onExit = onStateExit;
            base.Priority = priority;
        }
        public State(string name, ulong priority, Action<T> stateLogic, Action<T> onStateEnter, Action<T> onStateExit, Func<T, bool> enterCondition)
        {
            Name = name;
            _inState = stateLogic;
            _onEnter = onStateEnter;
            _onExit = onStateExit;
            EnterConditionD = enterCondition;
            base.Priority = priority;
        }
        override protected void OnEnterLogic(T entity)
        {
            _onEnter?.Invoke(entity);
        }
        override protected void OnUpdateLogic(T entity)
        {
            _inState?.Invoke(entity);
        }
        override protected void OnExitLogic(T entity)
        {
            _onExit?.Invoke(entity);
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
            _onEnter = enterLogic;
            return this;
        }
        public State<T> SetStateLogic(Action<T> stateLogic)
        {
            _inState = stateLogic;
            return this;
        }
        public State<T> SetOnStateExit(Action<T> onStateExit)
        {
            _onExit = onStateExit;
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
