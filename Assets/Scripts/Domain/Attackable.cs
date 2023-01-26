using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Domain
{
    public interface Attackable
    {
        public void Hit(int damage);
    }
}
