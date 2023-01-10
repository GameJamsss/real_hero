using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.StateMachine
{
    
    public abstract class AbstractState<T>
    {
        public HashSet<AbstractState<T>> BlackSet = new HashSet<AbstractState<T>>();
        public HashSet<AbstractState<T>> WhiteSet = new HashSet<AbstractState<T>>();

        private ulong _priority = 0;
        public ulong priority
        {
            get => _priority;
            set
            {
                _priority = value + 1;
            }
        }
        public string Name;
        abstract public void OnEnter(T entity);
        abstract public void InState(T entity);
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
