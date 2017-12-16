using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRMC.Internal.Interfaces
{
    public interface IJoinSemilattice<T>
    {
        T Top { get; }
        T Join(T left, T right);
        bool AreEquivalent(T left, T right);
    }

    public static class JoinSemilatticeExtensions
    {
        public static bool IsLTE<T>(
            this IJoinSemilattice<T> sl,
            T left,
            T right)
        {
            var joined = sl.Join(left, right);
            return sl.AreEquivalent(joined, right);
        }
    }
}
