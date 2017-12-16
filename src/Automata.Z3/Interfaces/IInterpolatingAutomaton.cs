using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Automata.Z3.Interfaces
{
    public interface IInterpolatingAutomaton<T> : IAutomaton<T>
    {
        IInterpolatingBooleanAlgebra<T> InterpolatingAlgebra { get; }
    }
}
