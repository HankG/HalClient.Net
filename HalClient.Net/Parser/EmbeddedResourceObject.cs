using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace HalClient.Net.Parser
{
    internal class EmbeddedResourceObject : ResourceObjectBase, IEmbeddedResourceObject
    {
        public EmbeddedResourceObject(IEnumerable<ILinkObject> links, IEnumerable<IEmbeddedResourceObject> embedded, JObject embeddedJObject, IEnumerable<IStateValue> state, string rel) 
            : base(state, embedded, embeddedJObject, links)
        {
            if (string.IsNullOrEmpty(rel)) 
                throw new ArgumentNullException("rel");

            Rel = rel;
        }
    
        public string Rel { get; set; }
    }
}