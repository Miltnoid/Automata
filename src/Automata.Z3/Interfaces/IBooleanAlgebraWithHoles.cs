using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Automata.Z3.Interfaces
{
    public interface IBooleanAlgebraWithHoles<S,T> : IBooleanAlgebra<S>
    {
        T HoleOne { get; }
        T HoleTwo { get; }
        S SymbolicallyApply(S twoHolePredicate, S singleHolePredicate);
        //bool IsHoleOneOnly(S predicate);
    }
}
