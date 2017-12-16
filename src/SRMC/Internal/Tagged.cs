using SRMC.Internal.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRMC.Internal
{
    public class Tagged<S> : ITagged<S>
    {
        private readonly S m_underlyingValue;
        private readonly IImmutableDictionary<string, Object> m_tagDictionary;
        private Tagged(S s, IImmutableDictionary<string, Object> tagDictionary)
        {
            this.m_underlyingValue = s;
            this.m_tagDictionary = tagDictionary;
        }

        public Tagged(S s)
            : this(s, ImmutableDictionary<string, Object>.Empty)
        {
        }

        public static Tagged<S> Make(S s)
        {
            return new Tagged<S>(s);
        }

        public ITagged<S> SetTag(string tagName, Object tagValue)
        {
            return new Tagged<S>(
                this.m_underlyingValue,
                this.m_tagDictionary.Add(tagName, tagValue));
        }

        public bool TryGetTag(string tagName, out Object tagValue)
        {
            return this.m_tagDictionary.TryGetValue(tagName, out tagValue);
        }

        public Object GetTag(string tagName)
        {
            Object value;
            this.m_tagDictionary.TryGetValue(tagName, out value);
            return value;
        }

        public S Value
        {
            get
            {
                return this.m_underlyingValue;
            }
        }
    }
}
