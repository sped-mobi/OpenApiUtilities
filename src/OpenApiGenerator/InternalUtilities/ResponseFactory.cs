// -----------------------------------------------------------------------
// <copyright file="CommonResponses.cs" company="sped.mobi">
//     Copyright © 2018 Brad R. Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Concurrent;
using Microsoft.OpenApi.Models;

namespace OpenApiGenerator.InternalUtilities
{
    internal static class ResponseFactory
    {

        public static OpenApiResponses CKAN_Responses()
        {
            OpenApiResponses responses = new OpenApiResponses();
            responses[ApiStrings.Response200Key] = Response200("ApiResult");
            responses[ApiStrings.ResponseDefaultKey] = ResponseDefault();
            return responses;
        }

        public static OpenApiResponses CKAN_Responses(string successSchemaName)
        {
            OpenApiResponses responses = new OpenApiResponses();
            responses[ApiStrings.Response200Key] = Response200(successSchemaName);
            responses[ApiStrings.ResponseDefaultKey] = ResponseDefault();
            return responses;
        }

        public static OpenApiResponses CKAN_Responses(string successSchemaName, string successSchemaDescription)
        {
            OpenApiResponses responses = new OpenApiResponses();
            responses[ApiStrings.Response200Key] = Response200(successSchemaName, successSchemaDescription);
            responses[ApiStrings.ResponseDefaultKey] = ResponseDefault();
            return responses;
        }

        public static OpenApiResponses Responses(OpenApiResponse successResponse, OpenApiResponse errorResponse)
        {
            OpenApiResponses api_responses = new OpenApiResponses();
            api_responses.Add("200", successResponse);
            api_responses.Add("default", errorResponse);
            return api_responses;
        }
        public static OpenApiResponses Responses(string successSchemaName, string errorSchemaName = "ErrorStatusInfo")
        {
            OpenApiResponses responses = new OpenApiResponses();
            responses[ApiStrings.Response200Key] = Response200(successSchemaName);
            responses[ApiStrings.Response400Key] = Response400(errorSchemaName);
            responses[ApiStrings.Response401Key] = Response401(errorSchemaName);
            responses[ApiStrings.Response403Key] = Response403(errorSchemaName);
            responses[ApiStrings.Response404Key] = Response404(errorSchemaName);
            responses[ApiStrings.Response429Key] = Response429(errorSchemaName);
            responses[ApiStrings.Response500Key] = Response500(errorSchemaName);
            //responses[ApiStrings.ResponseDefaultKey] = ResponseDefault(errorSchemaName);
            return responses;
        }

        public static OpenApiResponse Response400(string schemaName)
        {
            return new OpenApiResponse
            {
                Description = ApiStrings.Response400DescriptionFormat,
                Content = Content(schemaName)
            };
        }

        public static OpenApiResponse Response401(string schemaName)
        {
            return new OpenApiResponse
            {
                Description = ApiStrings.Response401DescriptionFormat,
                Content = Content(schemaName)
            };
        }

        public static OpenApiResponse Response403(string schemaName)
        {
            return new OpenApiResponse
            {
                Description = ApiStrings.Response403DescriptionFormat,
                Content = Content(schemaName)
            };
        }

        public static OpenApiResponse Response404(string schemaName)
        {
            return new OpenApiResponse
            {
                Description = ApiStrings.Response404DescriptionFormat,
                Content = Content(schemaName)
            };
        }

        public static OpenApiResponse Response429(string schemaName)
        {
            return new OpenApiResponse
            {
                Description = ApiStrings.Response429DescriptionFormat,
                Content = Content(schemaName)
            };
        }

        public static OpenApiResponse Response500(string schemaName)
        {
            return new OpenApiResponse
            {
                Description = ApiStrings.Response500DescriptionFormat,
                Content = Content(schemaName)
            };
        }

        public static OpenApiResponse ResponseDefault()
        {
            return new OpenApiResponse
            {
                Description = "This is the response if an error surfaces while processing a RESTful http request.",
                Content = Content("ApiException")
            };
        }

        public static OpenApiResponse Response200(string schemaName)
        {
            return new OpenApiResponse
            {
                Description = string.Format(ApiStrings.Response200DescriptionFormat_0, schemaName),
                Content = Content(schemaName)
            };
        }

        public static OpenApiResponse Response200(string schemaName, string schemaDescription)
        {
            return Response(schemaName, string.Format(ApiStrings.Response200DescriptionFormat_0, schemaDescription));
        }

        public static OpenApiResponse Response(string schemaName, string description)
        {
            return new OpenApiResponse
            {
                Description = description,
                Content = Content(schemaName)
            };
        }

        private static ConcurrentDictionary<string, OpenApiMediaType> Content(string schemaName)
        {
            return new ConcurrentDictionary<string, OpenApiMediaType>
            {
                ["application/json"] = MediaType(schemaName)
            };
        }

        private static OpenApiMediaType MediaType(string schemaName)
        {
            return new OpenApiMediaType
            {
                Schema = Schema(schemaName)
            };
        }

        private static OpenApiSchema Schema(string schemaName)
        {
            return new OpenApiSchema
            {
                Reference = SchemaReference(schemaName)
            };
        }

        private static OpenApiReference SchemaReference(string schemaName)
        {
            return new OpenApiReference
            {
                Type = ReferenceType.Schema,
                ExternalResource = $"#/components/schemas/{schemaName}"
            };
        }
    }
}
