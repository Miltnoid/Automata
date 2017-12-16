using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Automata.Z3.Interfaces;
using Microsoft.Automata;
using System.Diagnostics;

namespace SRMC.Internal
{
    internal interface RefinementResponse
    {
    }

    internal class AbstractedAutomaton<S,T> : RefinementResponse
    {
        public AbstractedAutomaton(IInterpolatingBooleanAlgebraWithHoles<S,T> ba)
        {
            this.BA = ba;
        }

        public IInterpolatingBooleanAlgebraWithHoles<S, T> BA { get; private set; }
    }

    internal class RefinedSemiLattice<S> : RefinementResponse
    {
        public RefinedSemiLattice(IEnumerable<S> semiLattice)
        {
            this.SemiLattice = semiLattice;
        }

        public IEnumerable<S> SemiLattice { get; private set; }
    }

    internal class RefineStateOrCounterExample : RefinementResponse
    {
        public RefineStateOrCounterExample()
        {
        }
    }

    internal class Refiner<T>
    {
        public RefinementResponse Refine(
            JoinSubSemilattice<T> labelAbstraction,
            Automaton<T> badSFA,
            Automaton<T> reachableSFA)
        {
            Debug.Assert(badSFA.Algebra == reachableSFA.Algebra);
            var intersectedAutomaton = badSFA.Intersect(reachableSFA);
            var emptyIntersection = intersectedAutomaton.IsEmpty;
            if (emptyIntersection)
            {
                var taggedBA =
                    new TaggedBooleanAlgebra<T>(
                        //TODO more general automaton that provides
                        //interpolating algebra
                        (IInterpolatingBooleanAlgebra<T>)badSFA.Algebra);
                var abstractedAutomaton =
                    intersectedAutomaton.MapEdges(
                        taggedBA,
                        e =>
                        {
                            var abstractedLabel = labelAbstraction.SubSemilatticeElement(e);
                            var t = new Tagged<T>(abstractedLabel);
                            t.SetTag(Constants.ConcreteLabelKey, e);
                            return t;
                        });
                throw new NotImplementedException();
            }
            else
            {
                //TODO determine how to calculate this
                return new RefineStateOrCounterExample();
            }
        }
    }
}
