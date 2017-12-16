using Microsoft.Automata.Z3.Interfaces;
using SRMC.Internal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRMC.Internal
{

    //Allow for tagging of edges of a Boolean Algebra
    internal class TaggedBooleanAlgebra<S> : IInterpolatingBooleanAlgebra<ITagged<S>>
    {
        private IInterpolatingBooleanAlgebra<S> m_underlyingAlgebra;
        private ITagged<S> m_singletonTrue;
        private ITagged<S> m_singletonFalse;
        public TaggedBooleanAlgebra(IInterpolatingBooleanAlgebra<S> ba)
        {
            this.m_underlyingAlgebra = ba;
            this.m_singletonTrue = new Tagged<S>(this.m_underlyingAlgebra.True);
            this.m_singletonFalse = new Tagged<S>(this.m_underlyingAlgebra.False);
        }

        public ITagged<S> NewTrue()
        {
            return new Tagged<S>(this.m_underlyingAlgebra.True);
        }

        public ITagged<S> NewFalse()
        {
            return new Tagged<S>(this.m_underlyingAlgebra.False);
        }

        #region IBooleanAlgebra members
        public bool IsExtensional
        {
            get
            {
                return this.m_underlyingAlgebra.IsExtensional;
            }
        }

        public bool IsAtomic
        {
            get
            {
                return this.m_underlyingAlgebra.IsAtomic;
            }
        }

        public ITagged<S> True
        {
            get
            {
                return this.m_singletonTrue;
            }
        }

        public ITagged<S> False
        {
            get
            {
                return this.m_singletonFalse;
            }
        }

        public bool AreEquivalent(ITagged<S> predicate1, ITagged<S> predicate2)
        {
            return this.m_underlyingAlgebra.AreEquivalent(
                predicate1.Value,
                predicate2.Value);
        }

        public bool CheckImplication(ITagged<S> lhs, ITagged<S> rhs)
        {
            return this.m_underlyingAlgebra.CheckImplication(
                lhs.Value,
                rhs.Value);
        }

        public bool EvaluateAtom(ITagged<S> atom, ITagged<S> psi)
        {
            return this.m_underlyingAlgebra.EvaluateAtom(
                atom.Value,
                psi.Value);
        }

        public IEnumerable<Tuple<bool[], ITagged<S>>> GenerateMinterms(params ITagged<S>[] constraints)
        {
            var m_underlyingConstraints = constraints.Select(c => c.Value).ToArray();
            return this.m_underlyingAlgebra.GenerateMinterms(m_underlyingConstraints)
                .Select(bt => new Tuple<bool[], ITagged<S>>(bt.Item1, new Tagged<S>(bt.Item2)));
        }

        public ITagged<S> GetAtom(ITagged<S> psi)
        {
            return new Tagged<S>(this.m_underlyingAlgebra.GetAtom(psi.Value));
        }

        public bool IsSatisfiable(ITagged<S> predicate)
        {
            return this.m_underlyingAlgebra.IsSatisfiable(predicate.Value);
        }

        public ITagged<S> MkAnd(IEnumerable<ITagged<S>> predicates)
        {
            return new Tagged<S>(this.m_underlyingAlgebra.MkAnd(predicates.Select(p => p.Value)));
        }

        public ITagged<S> MkAnd(params ITagged<S>[] predicates)
        {
            return MkAnd(predicates);
        }

        public ITagged<S> MkAnd(ITagged<S> predicate1, ITagged<S> predicate2)
        {
            return MkAnd(predicate1, predicate2);
        }

        public ITagged<S> MkDiff(ITagged<S> predicate1, ITagged<S> predicate2)
        {
            return new Tagged<S>(this.m_underlyingAlgebra.MkDiff(predicate1.Value, predicate2.Value));
        }

        public ITagged<S> MkNot(ITagged<S> predicate)
        {
            return new Tagged<S>(this.m_underlyingAlgebra.MkNot(predicate.Value));
        }

        public ITagged<S> MkOr(IEnumerable<ITagged<S>> predicates)
        {
            return new Tagged<S>(this.m_underlyingAlgebra.MkOr(predicates.Select(p => p.Value)));
        }

        public ITagged<S> MkOr(params ITagged<S>[] predicates)
        {
            return this.MkOr(predicates);
        }

        public ITagged<S> MkOr(ITagged<S> predicate1, ITagged<S> predicate2)
        {
            return this.MkOr(predicate1, predicate2);
        }

        public ITagged<S> MkSymmetricDifference(ITagged<S> p1, ITagged<S> p2)
        {
            return new Tagged<S>(
                this.m_underlyingAlgebra.MkSymmetricDifference(
                    p1.Value,
                    p2.Value));
        }

        public ITagged<S> Simplify(ITagged<S> predicate)
        {
            return new Tagged<S>(
                this.m_underlyingAlgebra.Simplify(predicate.Value));
        }
        #endregion
        

        #region IInterpolatingBooleanAlgebra members
        public ITagged<S> ComputeInterpolant(ITagged<S> predicate1, ITagged<S> predicate2)
        {
            return new Tagged<S>(
                this.m_underlyingAlgebra.ComputeInterpolant(
                    predicate1.Value,
                    predicate2.Value));
        }
        #endregion
    }
}
