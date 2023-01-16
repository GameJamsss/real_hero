using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.StateMachine
{
    public abstract class StateModifier<T> : MonoBehaviour
    {
        abstract public void UpdateModify(T entity);
        abstract public void EnterModify(T entity);
        abstract public void ExitModify(T entity);
    }
}
