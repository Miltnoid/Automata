using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Automata.Z3.Interfaces
{
    public interface IInterpolatingBooleanAlgebraWithHoles<S,T>
        : IInterpolatingBooleanAlgebra<S>, IBooleanAlgebraWithHoles<S,T>
    {
    }
}
