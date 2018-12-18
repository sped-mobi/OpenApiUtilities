// -----------------------------------------------------------------------
// <copyright file="TagFactory.cs" company="sped.mobi">
//     Copyright © 2018 Brad R. Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.OpenApi.Models;

namespace OpenApiGenerator.InternalUtilities
{
    internal static class TagFactory
    {
        public static IList<OpenApiTag> DataSetManagerTags()
        {
            return Tags(Tag(ApiStrings.DataSetsManagerTagName));
        }


        public static IList<OpenApiTag> Tags(params OpenApiTag[] tags)
        {
            return new List<OpenApiTag>(tags);
        }

        public static OpenApiTag Tag(string name, string description = null)
        {
            return new OpenApiTag
            {
                Name = name,
                Description = description
            };
        }
    }
}
