// -----------------------------------------------------------------------
// <copyright file="OperationFactory.cs" company="sped.mobi">
//     Copyright © 2018 Brad R. Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.OpenApi.Models;

namespace OpenApiGenerator.InternalUtilities
{
    internal static class OperationFactory
    {
        public static IDictionary<OperationType, OpenApiOperation> OperationMap(OperationType type, OpenApiOperation operation)
        {
            return new ConcurrentDictionary<OperationType, OpenApiOperation>
            {
                [type] = operation
            };
        }

        public static OpenApiOperation Operation(
            string operationId,
            string summary,
            string description,
            string tagName,
            OpenApiResponses responses,
            params OpenApiParameter[] parameters)
        {
            return new OpenApiOperation
            {
                OperationId = operationId,
                Description = description,
                Summary = summary,
                Responses = responses,
                Parameters = ParameterFactory.Parameters(parameters),
                Tags = TagFactory.Tags(TagFactory.Tag(tagName)),
                Servers = Api.Servers(),
                Security = Api.SecurityRequirements()
            };
        }


        public static IDictionary<OperationType, OpenApiOperation> OperationMap(
            params (OperationType, OpenApiOperation)[] operations)
        {
            var map = new ConcurrentDictionary<OperationType, OpenApiOperation>();

            foreach (var kvp in operations)
            {
                if (!map.ContainsKey(kvp.Item1))
                {
                    map[kvp.Item1] = kvp.Item2;
                }
            }

            return map;
        }

        public static IDictionary<OperationType, OpenApiOperation> OperationMap(
            params KeyValuePair<OperationType, OpenApiOperation>[] operations)
        {
            var map = new ConcurrentDictionary<OperationType, OpenApiOperation>();

            foreach (KeyValuePair<OperationType, OpenApiOperation> kvp in operations)
            {
                if (!map.ContainsKey(kvp.Key))
                {
                    map[kvp.Key] = kvp.Value;
                }
            }

            return map;
        }

        public static KeyValuePair<OperationType, OpenApiOperation> OperationPair(
            OperationType operationType,
            OpenApiOperation operation)
        {
            return new KeyValuePair<OperationType, OpenApiOperation>(operationType, operation);
        }

        public static IDictionary<OperationType, OpenApiOperation> GetAllOperationMap(
            string operationId,
            string responseSchemaName
            )
        {
            return OperationMap(
                (OperationType.Get, 
                    AllOperation(operationId, responseSchemaName)));
        }

        public static IDictionary<OperationType, OpenApiOperation> OperationMap(string responseSchemaName)
        {
            return OperationMap(
                (OperationType.Get, GetOperation(responseSchemaName)),
                (OperationType.Post, PostOperation(responseSchemaName)),
                (OperationType.Patch, PatchOperation(responseSchemaName)),
                (OperationType.Delete, DeleteOperation(responseSchemaName)));
        }

        public static IDictionary<OperationType, OpenApiOperation> GetOperationMap(string responseSchemaName)
        {
            return OperationMap(
                (OperationType.Get, CKAN_GetOperation(responseSchemaName)));
        }

        public static OpenApiOperation CKAN_GetOperation(string responseSchemaName)
        {
            return new OpenApiOperation
            {

                OperationId = responseSchemaName,
                Description = string.Format(ApiStrings.GetOperationDescriptionFormat_1, responseSchemaName),
                Summary = string.Format(ApiStrings.GetOperationSummaryFormat, responseSchemaName),
                Deprecated = false,
                Parameters = ParameterFactory.Parameters(),
                Responses = ResponseFactory.CKAN_Responses(responseSchemaName),
                Tags = TagFactory.Tags(),
                Servers = Api.Servers(),
                Security = Api.SecurityRequirements()
            };
        }

        public static IDictionary<OperationType, OpenApiOperation> PutOperationMap(string responseSchemaName)
        {
            return OperationMap(
                (OperationType.Put, CKAN_PutOperation(responseSchemaName)));
        }

        public static OpenApiOperation CKAN_PutOperation(string responseSchemaName)
        {
            return new OpenApiOperation
            {

                OperationId = responseSchemaName,
                Description = string.Format(ApiStrings.PutOperationDescription, responseSchemaName),
                Summary = string.Format(ApiStrings.PutOperationSummary, responseSchemaName),
                Deprecated = false,
                Parameters = ParameterFactory.Parameters(),
                Responses = ResponseFactory.CKAN_Responses(responseSchemaName),
                Tags = TagFactory.Tags(),
                Servers = Api.Servers(),
                Security = Api.SecurityRequirements()
            };
        }

        public static IDictionary<OperationType, OpenApiOperation> CreateOperationMap(string responseSchemaName)
        {
            return OperationMap(
                (OperationType.Post, CKAN_PostOperation(responseSchemaName)));
        }

        private static OpenApiOperation CKAN_PostOperation(string responseSchemaName)
        {
            return new OpenApiOperation
            {

                OperationId = responseSchemaName,
                Description = string.Format(ApiStrings.PostOperationDescription, responseSchemaName),
                Summary = string.Format(ApiStrings.PostOperationSummary, responseSchemaName),
                Deprecated = false,
                Parameters = ParameterFactory.Parameters(),
                Responses = ResponseFactory.CKAN_Responses(responseSchemaName),
                Tags = TagFactory.Tags(),
                Servers = Api.Servers(),
                Security = Api.SecurityRequirements()
            };
        }

        public static IDictionary<OperationType, OpenApiOperation> PatchOperationMap(string responseSchemaName)
        {
            return OperationMap(
                (OperationType.Patch, CKAN_PatchOperation(responseSchemaName)));
        }

        private static OpenApiOperation CKAN_PatchOperation(string responseSchemaName)
        {
            return new OpenApiOperation
            {

                OperationId = responseSchemaName,
                Description = string.Format(ApiStrings.PatchOperationDescription, responseSchemaName),
                Summary = string.Format(ApiStrings.PatchOperationSummary, responseSchemaName),
                Deprecated = false,
                Parameters = ParameterFactory.Parameters(),
                Responses = ResponseFactory.CKAN_Responses(responseSchemaName),
                Tags = TagFactory.Tags(),
                Servers = Api.Servers(),
                Security = Api.SecurityRequirements()
            };
        }

        public static IDictionary<OperationType, OpenApiOperation> DeleteOperationMap(string responseSchemaName)
        {
            return OperationMap(
                (OperationType.Delete, CKAN_DeleteOperation(responseSchemaName)));
        }

        private static OpenApiOperation CKAN_DeleteOperation(string responseSchemaName)
        {
            return new OpenApiOperation
            {

                OperationId = responseSchemaName,
                Description = string.Format(ApiStrings.DeleteOperationDescription, responseSchemaName),
                Summary = string.Format(ApiStrings.DeleteOperationSummary, responseSchemaName),
                Deprecated = false,
                Parameters = ParameterFactory.Parameters(),
                Responses = ResponseFactory.CKAN_Responses(responseSchemaName),
                Tags = TagFactory.Tags(),
                Servers = Api.Servers(),
                Security = Api.SecurityRequirements()
            };
        }

        public static OpenApiOperation PostOperation(string responseSchemaName)
        {
            string operationId = string.Format(ApiStrings.PostOperationIdFormat, responseSchemaName);
            return new OpenApiOperation
            {
                OperationId = operationId,
                Description = string.Format(ApiStrings.PostOperationDescription, responseSchemaName),
                Summary = string.Format(ApiStrings.PostOperationSummary, operationId),
                Deprecated = false,
                Parameters = ParameterFactory.Parameters(
                    ParameterFactory.Parameter(ApiStrings.UUIDParameterName, ApiStrings.UUIDParameterDescription)),
                Responses = ResponseFactory.Responses(responseSchemaName),
                Tags = TagFactory.DataSetManagerTags(),
                Servers = Api.Servers(),
                Security = Api.SecurityRequirements()
            };
        }

        public static OpenApiOperation PatchOperation(string responseSchemaName)
        {
            string operationId = string.Format(ApiStrings.PatchOperationIdFormat, responseSchemaName);
            return new OpenApiOperation
            {
                OperationId = operationId,
                Description = string.Format(ApiStrings.PatchOperationDescription, responseSchemaName),
                Summary = string.Format(ApiStrings.PatchOperationSummary, operationId),
                Deprecated = false,
                Parameters = ParameterFactory.Parameters(
                    ParameterFactory.Parameter(ApiStrings.UUIDParameterName, ApiStrings.UUIDParameterDescription)),
                Responses = ResponseFactory.Responses(responseSchemaName),
                Tags = TagFactory.DataSetManagerTags(),
                Servers = Api.Servers(),
                Security = Api.SecurityRequirements()
            };
        }

        public static OpenApiOperation DeleteOperation(string responseSchemaName)
        {
            string operationId = string.Format(ApiStrings.DeleteOperationIdFormat, responseSchemaName);
            return new OpenApiOperation
            {
                OperationId = operationId,
                Description = string.Format(ApiStrings.DeleteOperationDescription, responseSchemaName),
                Summary = string.Format(ApiStrings.DeleteOperationSummary, operationId),
                Deprecated = false,
                Parameters = ParameterFactory.Parameters(
                    ParameterFactory.Parameter(ApiStrings.UUIDParameterName, ApiStrings.UUIDParameterDescription)),
                Responses = ResponseFactory.Responses(responseSchemaName),
                Tags = TagFactory.DataSetManagerTags(),
                Servers = Api.Servers(),
                Security = Api.SecurityRequirements()
            };
        }



        public static OpenApiOperation AllOperation(
            string operationId,
            string responseSchemaName
            )
        {
            return new OpenApiOperation
            {
                OperationId = operationId,
                Description = string.Format(ApiStrings.GetOperationDescriptionFormat_2, responseSchemaName),
                Summary = string.Format(ApiStrings.GetOperationSummaryFormat, operationId),
                Responses = ResponseFactory.Responses(responseSchemaName)
            };
        }

        public static OpenApiOperation GetOperation(
            string responseSchemaName)
        {
            string operationId = string.Format(ApiStrings.GetOperationIdFormat, responseSchemaName);
            return new OpenApiOperation
            {
                
                OperationId = operationId,
                Description = string.Format(ApiStrings.GetOperationDescriptionFormat_1, responseSchemaName),
                Summary = string.Format(ApiStrings.GetOperationSummaryFormat, operationId),
                Deprecated = false,
                Parameters = ParameterFactory.Parameters(
                    ParameterFactory.Parameter(ApiStrings.UUIDParameterName, ApiStrings.UUIDParameterDescription)),
                Responses = ResponseFactory.Responses(responseSchemaName),
                Tags = TagFactory.DataSetManagerTags(),
                Servers = Api.Servers(),
                Security = Api.SecurityRequirements()
            };
        }




    }
}
