using System;
using System.Collections.Generic;

namespace Microsoft.Automata.Z3.Interfaces
{

    /// <summary>
    /// Generic Boolean Algebra solver.
    /// Provides operations for conjunction, disjunction, and negation.
    /// Allows to decide if a predicate is satisfiable and if two predicates are equivalent.
    /// </summary>
    /// <typeparam name="S">predicates</typeparam>
    public interface IInterpolatingBooleanAlgebra<S> : IBooleanAlgebra<S>
    {
        S ComputeInterpolant(S predicate1, S predicate2);
    }
}
