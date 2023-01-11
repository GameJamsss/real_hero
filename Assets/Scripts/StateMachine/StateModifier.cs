using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.StateMachine
{
    public interface StateModifier<T>
    {
        public void UpdateModify(T entity);
        public void EnterModify(T entity);
        public void ExitModify(T entity);
    }
}
