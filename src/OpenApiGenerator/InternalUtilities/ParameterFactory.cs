// -----------------------------------------------------------------------
// <copyright file="ParameterFactory.cs" company="sped.mobi">
//     Copyright © 2018 Brad R. Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace OpenApiGenerator.InternalUtilities
{
    internal static class ParameterFactory
    {
        public static OpenApiParameter LimitParameter()
        {
            return IntegerQueryParameter("limit",
                "If given, the list of datasets will be broken into pages of at most _limit " +
                "datasets per page and only one page will be returned at a time (optional)");
        }

        public static OpenApiParameter OffsetParameter()
        {
            return IntegerQueryParameter("offset", "When _limit is given, the offset to start returning packages from.");
        }
        public static IList<OpenApiParameter> Parameters(params OpenApiParameter[] parameters)
        {
            return new List<OpenApiParameter>(parameters);
        }

        public static OpenApiParameter Parameter(string name, string description, bool depreciated = false)
        {
            return new OpenApiParameter
            {
                Name = name,
                Description = description,
                Deprecated = depreciated,
                In = ParameterLocation.Path,
                AllowEmptyValue = false,
                AllowReserved = false
            };
        }

        public static OpenApiParameter StringQueryParameter(string name, string format, string description, bool depreciated = false)
        {
            return new OpenApiParameter
            {
                Name = name,
                Description = description,
                Deprecated = depreciated,
                In = ParameterLocation.Query,
                AllowEmptyValue = false,
                AllowReserved = false,
                Schema = SchemaFactory.Schema("string", format)
            };
        }

        public static OpenApiParameter StringQueryParameter(string name, string description, bool depreciated = false)
        {
            return new OpenApiParameter
            {
                Name = name,
                Description = description,
                Deprecated = depreciated,
                In = ParameterLocation.Query,
                AllowEmptyValue = false,
                AllowReserved = false,
                Schema = SchemaFactory.Schema("string")
            };
        }


        public static OpenApiParameter IntegerQueryParameter(string name, string description, bool depreciated = false)
        {
            return new OpenApiParameter
            {
                Name = name,
                Description = description,
                Deprecated = depreciated,
                In = ParameterLocation.Query,
                AllowEmptyValue = false,
                AllowReserved = false,
                Schema = SchemaFactory.Schema("integer", "int32")
            };
        }

        public static OpenApiParameter QueryParameter(string name, string type, string format, string description, bool depreciated = false)
        {
            return new OpenApiParameter
            {
                Name = name,
                Description = description,
                Deprecated = depreciated,
                In = ParameterLocation.Query,
                AllowEmptyValue = false,
                AllowReserved = false,
                Schema = SchemaFactory.Schema(type, format)
            };
        }
    }
}
