using SRMC.Internal.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRMC.Internal
{
    //name could use some work...
    public class JoinSubSemilattice<T> : IJoinSemilattice<T>
    {
        private IJoinSemilattice<T> m_universe;
        private IList<T> m_generators;

        public JoinSubSemilattice(IJoinSemilattice<T> universe, IEnumerable<T> generators)
        {
            this.m_universe = universe;
            this.m_generators = generators.ToList();
        }

        public T Top
        {
            get
            {
                return m_universe.Top;
            }
        }

        public T SubSemilatticeElement(T universalElement)
        {
            return m_generators.Aggregate(this.Top,
                (acc, elt) =>
                this.IsLTE(universalElement, elt)
                ? Join(acc, elt)
                : acc);
        }

        public IJoinSemilattice<T> Universe
        {
            get
            {
                return m_universe;
            }
        }

        public T Join(T left, T right)
        {
            return m_universe.Join(left, right);
        }

        public bool AreEquivalent(T left, T right)
        {
            return m_universe.AreEquivalent(left, right);
        }
    }
}
