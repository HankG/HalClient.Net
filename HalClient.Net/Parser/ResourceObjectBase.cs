using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace HalClient.Net.Parser
{
    internal abstract class ResourceObjectBase : IResourceObject
    {
        protected ResourceObjectBase(
            IEnumerable<IStateValue> state,
            IEnumerable<IEmbeddedResourceObject> embedded,
            JObject embeddedJObject,
            IEnumerable<ILinkObject> links)
        {
            var linkDict = links.GroupBy(x => x.Rel)
                .ToDictionary(x => x.Key, x => (IEnumerable<ILinkObject>) x.ToArray());

            var embeddedDict = embedded.GroupBy(x => x.Rel)
                .ToDictionary(x => x.Key, x => (IEnumerable<IEmbeddedResourceObject>)x.ToArray());

            var stateDict = state.ToDictionary(x => x.Name);
            
            State = new ReadOnlyDictionary<string, IStateValue>(stateDict);
            Embedded = new ReadOnlyDictionary<string, IEnumerable<IEmbeddedResourceObject>>(embeddedDict);
            this.EmbeddedJObject = embeddedJObject;
            Links = new ReadOnlyDictionary<string, IEnumerable<ILinkObject>>(linkDict);
        }

        protected ResourceObjectBase()
            : this(new IStateValue[0], new IEmbeddedResourceObject[0], new JObject(), new ILinkObject[0])
        {
        }

        public IReadOnlyDictionary<string, IStateValue> State { get; set; }
        public IReadOnlyDictionary<string, IEnumerable<IEmbeddedResourceObject>> Embedded { get; set; }
        public JObject EmbeddedJObject { get; }
        public IReadOnlyDictionary<string, IEnumerable<ILinkObject>> Links { get; set; }
    }
}