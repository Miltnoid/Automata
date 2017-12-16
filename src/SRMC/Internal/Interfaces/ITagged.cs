using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRMC.Internal.Interfaces
{
    public interface ITagged<S>
    {
        ITagged<S> SetTag(string tagName, Object tagValue);

        bool TryGetTag(string tagName, out Object tagValue);

        Object GetTag(string tagName);

        S Value { get; }
    }
}
