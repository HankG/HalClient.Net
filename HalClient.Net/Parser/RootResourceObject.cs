using System;
using System.Collections.Generic;
using HalClient.Net.External;
using Newtonsoft.Json.Linq;

namespace HalClient.Net.Parser
{
    internal class RootResourceObject : ResourceObjectBase, IRootResourceObject
    {
        public RootResourceObject(IEnumerable<ILinkObject> links, IEnumerable<IEmbeddedResourceObject> embedded, JObject embeddedJObject, List<IStateValue> state) 
            : base(state, embedded, embeddedJObject, links)
        {
        }

        public RootResourceObject()
        {
        }

        public Uri GetDocumentationUri(IHaveLinkRelation subject)
        {
            if (!Links.ContainsKey("curies"))
                return null;

            foreach (var link in Links["curies"])
            {
                var prefix = string.Format("{0}:", link.Name);

                if (!subject.Rel.StartsWith(prefix)) 
                    continue;

                var parts = subject.Rel.Split(':');

                if (parts.Length > 2)
                    throw new FormatException("Invalid, named link relation:" + subject.Rel);

                return ResolveDocumentationUri(link, parts[1]);
            }

            return null;
        }

        private static Uri ResolveDocumentationUri(ILinkObject link, string rel)
        {
            var template = new UriTemplate(link.Href.ToString());

            template.SetParameter("rel", rel);

            return new Uri(template.Resolve());
        }
    }
}