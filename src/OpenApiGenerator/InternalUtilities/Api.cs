// -----------------------------------------------------------------------
// <copyright file="OpenApiFactory.cs" company="sped.mobi">
//     Copyright © 2018 Brad R. Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace OpenApiGenerator.InternalUtilities
{
    internal static class Api
    {
        public static OpenApiContact Contact(string email, string name, string url)
        {
            return new OpenApiContact
            {
                Email = email,
                Name = name,
                Url = new Uri(url)
            };
        }

        public static KeyValuePair<OperationType, OpenApiOperation> DeleteOperation(
            string operationId,
            string schemaName,
            IList<OpenApiSecurityRequirement> security,
            IList<OpenApiServer> servers,
            IList<OpenApiTag> tags,
            IList<OpenApiParameter> parameters,
            OpenApiResponses responses,
            OpenApiRequestBody body
        )
        {
            return new KeyValuePair<OperationType, OpenApiOperation>(
                OperationType.Delete,
                new OpenApiOperation
                {
                    Deprecated = false,
                    Summary = string.Format(ApiStrings.DeleteOperationSummary, operationId),
                    Description = string.Format(ApiStrings.DeleteOperationDescription, schemaName),
                    OperationId = operationId,
                    Parameters = parameters ?? new List<OpenApiParameter>(),
                    Responses = responses ?? new OpenApiResponses(),
                    Tags = tags ?? new List<OpenApiTag>(),
                    Servers = servers ?? new List<OpenApiServer>(),
                    Security = security ?? new List<OpenApiSecurityRequirement>(),
                    RequestBody = body ?? new OpenApiRequestBody()
                });
        }

        public static OpenApiDocument Document()
        {
            return new OpenApiDocument
            {
                Servers = Servers(
                    Server(
                        ApiStrings.Document_Servers_Server_Description,
                        ApiStrings.Document_Servers_Server_Url,
                        Variables(
                            Variable(
                                "scheme",
                                ServerVariable(
                                    ApiStrings.Document_Servers_Server_SchemeVariable_Description,
                                    "http",
                                    "http",
                                    "https"
                                )
                            ),
                            Variable(
                                "basePath",
                                ServerVariable(
                                    ApiStrings.Document_Servers_Server_BasePathVariable_Description,
                                    "api/v1"
                                    )
                                )
                        )
                    )
                ),
                Info = Info(
                    ApiStrings.Document_Info_Title,
                    ApiStrings.Document_Info_Version,
                    ApiStrings.Document_Info_Description,
                    ApiStrings.Document_Info_TermsOfService,
                    Contact(
                        ApiStrings.Document_Info_Contact_Email,
                        ApiStrings.Document_Info_Contact_Name,
                        ApiStrings.Document_Info_Contact_Url),
                    License(
                        ApiStrings.Document_Info_License_Name,
                        ApiStrings.Document_Info_License_Url)
                ),
                Paths = Paths(
                    Path(
                        "/dataset",
                        PathItem(
                            Servers(),
                            OperationFactory.GetAllOperationMap("GetAllDataSets","DataSets"),
                            Parameters()
                        )
                    ),
                    Path(
                        "/dataset/{uuid}",
                        PathItem(
                            Servers(),
                            OperationFactory.OperationMap("DataSet"),
                            Parameters()
                        )
                    )
                ),
                Components = Components(),
                Tags =
                    Tags(
                        Tag(ApiStrings.DataSetsManagerTagName,
                            ApiStrings.DataSetsManagerTagDescription))
            };
        }

        public static OpenApiString String(string value)
        {
            return new OpenApiString(value);
        }

        public static OpenApiInteger Integer(int value)
        {
            return new OpenApiInteger(value);
        }

        public static OpenApiComponents Components()
        {
            return new OpenApiComponents
            {
                Schemas = new ConcurrentDictionary<string, OpenApiSchema>
                {
                    //["DataSets"] = new OpenApiSchema
                    //{
                    //    Type = "object",
                    //    Description = ApiStrings.DataSetsSchemaDescription,
                    //    Properties = new ConcurrentDictionary<string, OpenApiSchema>
                    //    {
                    //        ["total"] = new OpenApiSchema
                    //        {
                    //            Type = "integer"
                    //        },
                    //        ["sets"] = new OpenApiSchema
                    //        {
                    //            Type = "array",
                    //            Items = SchemaReference(
                    //                string.Format(ApiStrings.SchemaReferencePathFormat, "DataSet"))
                    //        }
                    //    }
                    //},
                    //["DataSet"] = new OpenApiSchema
                    //{
                    //    Type = "object",
                    //    Description = ApiStrings.DataSetSchemaDescription,
                    //    Properties = new ConcurrentDictionary<string, OpenApiSchema>
                    //    {
                    //        ["id"] = SchemaReference(
                    //            string.Format(ApiStrings.SchemaReferencePathFormat, "UUID")),
                    //        ["name"] = new OpenApiSchema
                    //        {
                    //            Type = "string"
                    //        },
                    //        ["description"] = new OpenApiSchema
                    //        {
                    //            Type = "string"
                    //        },
                    //        ["category"] = new OpenApiSchema
                    //        {
                    //            Type = "string"
                    //        },
                    //        ["attribution"] = new OpenApiSchema
                    //        {
                    //            Type = "string"
                    //        },
                    //        ["attributionLink"] = SchemaReference(
                    //            string.Format(ApiStrings.SchemaReferencePathFormat, "URL")),
                    //        ["iconLink"] = SchemaReference(
                    //            string.Format(ApiStrings.SchemaReferencePathFormat, "URL"))
                    //    }
                    //},
                    ["URL"] = new OpenApiSchema
                    {
                        Description = "The data-type for establishing a Uniform Resource Locator (URL) as defined by W3C.",
                        Type = "string",
                        Format = "uri"
                    },
                    ["UUID"] = new OpenApiSchema
                    {
                        Description = "The data-type for establishing a Globally Unique Identifier (GUID). The form of the GUID is a Universally Unique Identifier (UUID) of 16 hexadecimal characters (lower case) in the format 8-4-4-4-12. All permitted versions (1-5) and variants (1-2) are supported.",
                        Type = "string",
                        Format = "uuid"
                    },
                    ["ErrorStatusInfo"] = new OpenApiSchema
                    {
                        Type = "object",
                        Description = "This is the container for the status code and associated information returned within the HTTP messages received from the Service Provider. For the CASE service this object will only be returned to provide information about a failed request i.e. it will NOT be in the payload for a successful request. See Appendix B for further information on the interpretation of the information contained within this class.",
                        Properties =  new ConcurrentDictionary<string, OpenApiSchema>
                        {
                            ["codeMajor"] = new OpenApiSchema
                            {
                                Type = "string",
                                Enum = new List<IOpenApiAny>
                                {
                                    new OpenApiString("success"),
                                    new OpenApiString("processing"),
                                    new OpenApiString("failure"),
                                    new OpenApiString("unsupported")
                                }
                            },
                            ["severity"] = new OpenApiSchema
                            {
                                Type = "string",
                                Enum = new List<IOpenApiAny>
                                {
                                    new OpenApiString("status"),
                                    new OpenApiString("warning"),
                                    new OpenApiString("error")
                                }
                            },
                            ["description"] = SchemaFactory.Schema("string"),
                            ["codeMinor"] = SchemaReference(
                                string.Format(ApiStrings.SchemaReferencePathFormat, "ErrorCodeMinor"))
                        },
                        Required = new HashSet<string>
                        {
                            "codeMajor",
                            "severity"
                        }

                    },
                    ["ErrorCodeMinor"] = new OpenApiSchema
                    {
                        Description = "This is the container for the set of code minor status codes reported in the responses from the Service Provider.",
                        Properties = new ConcurrentDictionary<string, OpenApiSchema>
                        {
                            ["codes"] = SchemaFactory.ArraySchema("ErrorCodeMinorField")
                        },
                        Required = new HashSet<string>
                        {
                            "codes",
                        }
                    },
                    ["ErrorCodeMinorField"] = new OpenApiSchema
                    {
                        Description = "This is the container for a single code minor status code.",
                        Properties = new ConcurrentDictionary<string, OpenApiSchema>
                        {
                            ["name"] = SchemaFactory.Schema("string"),
                            ["value"] = new OpenApiSchema
                            {
                                Type = "string",
                                Enum = new List<IOpenApiAny>
                                {
                                    new OpenApiString("fullsuccess"),
                                    new OpenApiString("invalid_sort_field"),
                                    new OpenApiString("invalid_selection_field"),
                                    new OpenApiString("forbidden"),
                                    new OpenApiString("unauthorisedrequest"),
                                    new OpenApiString("internal_server_error"),
                                    new OpenApiString("unknownobject"),
                                    new OpenApiString("server_busy"),
                                    new OpenApiString("invaliduuid"),
                                }
                            },
                        },
                        Required = new HashSet<string>
                        {
                            "name",
                            "value"
                        }
                    },
                }
            };
        }

        public static List<string> Enum(params string[] enums)
        {
            return new List<string>(enums ?? new string[] { });
        }

        public static KeyValuePair<OperationType, OpenApiOperation> GetOperation(
            string operationId,
            string returnSchemaName,
            IList<OpenApiSecurityRequirement> security,
            IList<OpenApiServer> servers,
            IList<OpenApiTag> tags,
            IList<OpenApiParameter> parameters,
            OpenApiResponses responses
        )
        {
            return new KeyValuePair<OperationType, OpenApiOperation>(
                OperationType.Post,
                new OpenApiOperation
                {
                    Deprecated = false,
                    Summary = string.Format(ApiStrings.GetOperationSummaryFormat, operationId),
                    Description = string.Format(ApiStrings.GetOperationDescriptionFormat_1, returnSchemaName),
                    OperationId = operationId,
                    Parameters = parameters ?? new List<OpenApiParameter>(),
                    Responses = responses ?? new OpenApiResponses(),
                    Tags = tags ?? new List<OpenApiTag>(),
                    Servers = servers ?? new List<OpenApiServer>(),
                    Security = security ?? new List<OpenApiSecurityRequirement>()
                });
        }

        public static OpenApiInfo Info(
            string title,
            string version,
            string description,
            string termsOfService,
            OpenApiContact contact,
            OpenApiLicense license)
        {
            return new OpenApiInfo
            {
                Title = title,
                Version = version,
                Description = description,
                TermsOfService = new Uri(termsOfService),
                Contact = contact,
                License = license
            };
        }

        public static OpenApiLicense License(string name, string url)
        {
            return new OpenApiLicense
            {
                Name = name,
                Url = new Uri(url)
            };
        }

        public static IDictionary<OperationType, OpenApiOperation> Operations()
        {
            return new ConcurrentDictionary<OperationType, OpenApiOperation>();
        }


        public static IDictionary<OperationType, OpenApiOperation> Operations(
            KeyValuePair<OperationType, OpenApiOperation> getOperation,
            KeyValuePair<OperationType, OpenApiOperation> postOperation,
            KeyValuePair<OperationType, OpenApiOperation> patchOperation,
            KeyValuePair<OperationType, OpenApiOperation> deleteOperation
        )
        {
            return new ConcurrentDictionary<OperationType, OpenApiOperation>(Pairs(getOperation, postOperation, patchOperation, deleteOperation));
        }

        public static OpenApiParameter Parameter()
        {
            return new OpenApiParameter
            {

            };
        }

        public static IList<OpenApiParameter> Parameters(params OpenApiParameter[] parameters)
        {
            return new List<OpenApiParameter>(parameters);
        }

        public static KeyValuePair<OperationType, OpenApiOperation> PatchOperation(
            string operationId,
            string schemaName,
            IList<OpenApiSecurityRequirement> security,
            IList<OpenApiServer> servers,
            IList<OpenApiTag> tags,
            IList<OpenApiParameter> parameters,
            OpenApiResponses responses,
            OpenApiRequestBody body
        )
        {
            return new KeyValuePair<OperationType, OpenApiOperation>(
                OperationType.Patch,
                new OpenApiOperation
                {
                    Deprecated = false,
                    Summary = string.Format(ApiStrings.PatchOperationSummary, operationId),
                    Description = string.Format(ApiStrings.PatchOperationDescription, schemaName),
                    OperationId = operationId,
                    Parameters = parameters ?? new List<OpenApiParameter>(),
                    Responses = responses ?? new OpenApiResponses(),
                    Tags = tags ?? new List<OpenApiTag>(),
                    Servers = servers ?? new List<OpenApiServer>(),
                    Security = security ?? new List<OpenApiSecurityRequirement>(),
                    RequestBody = body ?? new OpenApiRequestBody()
                });
        }

        public static KeyValuePair<string, OpenApiPathItem> Path(string key, OpenApiPathItem pathItem)
        {
            return new KeyValuePair<string, OpenApiPathItem>(key, pathItem);
        }

        public static OpenApiPathItem PathItem(IDictionary<OperationType, OpenApiOperation> operations)
        {
            return PathItem(Servers(), operations, Parameters());
        }

        public static OpenApiPathItem PathItem(
            IList<OpenApiServer> servers,
            IDictionary<OperationType, OpenApiOperation> operations,
            IList<OpenApiParameter> parameters
        )
        {
            return new OpenApiPathItem
            {
                Servers = servers ?? new List<OpenApiServer>(),
                Operations = operations ?? new ConcurrentDictionary<OperationType, OpenApiOperation>(),
                Parameters = parameters ?? new List<OpenApiParameter>()
            };
        }

        public static OpenApiPaths Paths(IEnumerable<KeyValuePair<string, OpenApiPathItem>> pathItems)
        {
            var paths = new OpenApiPaths();
            foreach (var pathItem in pathItems)
            {
                if (!paths.ContainsKey(pathItem.Key))
                {
                    paths.Add(pathItem.Key, pathItem.Value);
                }
            }

            return paths;
        }

        public static OpenApiPaths Paths(params KeyValuePair<string, OpenApiPathItem>[] pathItems)
        {
            var paths = new OpenApiPaths();
            foreach (var pathItem in pathItems)
            {
                if (!paths.ContainsKey(pathItem.Key))
                {
                    paths.Add(pathItem.Key, pathItem.Value);
                }
            }

            return paths;
        }

        public static KeyValuePair<OperationType, OpenApiOperation> PostOperation(
            string operationId,
            string schemaName,
            IList<OpenApiSecurityRequirement> security,
            IList<OpenApiServer> servers,
            IList<OpenApiTag> tags,
            IList<OpenApiParameter> parameters,
            OpenApiResponses responses,
            OpenApiRequestBody body
        )
        {
            return new KeyValuePair<OperationType, OpenApiOperation>(
                OperationType.Get,
                new OpenApiOperation
                {
                    Deprecated = false,
                    Summary = string.Format(ApiStrings.PostOperationSummary, operationId),
                    Description = string.Format(ApiStrings.PostOperationDescription, schemaName),
                    OperationId = operationId,
                    Parameters = parameters ?? new List<OpenApiParameter>(),
                    Responses = responses ?? new OpenApiResponses(),
                    Tags = tags ?? new List<OpenApiTag>(),
                    Servers = servers ?? new List<OpenApiServer>(),
                    Security = security ?? new List<OpenApiSecurityRequirement>(),
                    RequestBody = body ?? new OpenApiRequestBody()
                });
        }

        public static OpenApiRequestBody RequestBody(string path)
        {
            return new OpenApiRequestBody
            {
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/json"] = new OpenApiMediaType
                    {
                        Schema = SchemaReference(path)
                    }
                }
            };
        }

        public static OpenApiResponses Responses(params KeyValuePair<string, OpenApiResponse>[] response)
        {
            var responses = new OpenApiResponses();
            foreach (var r in response)
            {
                if (!responses.ContainsKey(r.Key))
                {
                    responses.Add(r.Key, r.Value);
                }
            }

            return responses;
        }

        public static OpenApiSchema SchemaReference(string externalResource)
        {
            return new OpenApiSchema
            {
                Reference = new OpenApiReference
                {
                    ExternalResource = externalResource,
                    Type = ReferenceType.Schema
                }
            };
        }

        public static OpenApiSecurityRequirement SecurityRequirement()
        {
            return new OpenApiSecurityRequirement
            {

            };
        }

        public static IList<OpenApiSecurityRequirement> SecurityRequirements(params OpenApiSecurityRequirement[] securityRequirements)
        {
            return new List<OpenApiSecurityRequirement>(securityRequirements);
        }

        public static OpenApiServer Server(string description, string url, IDictionary<string, OpenApiServerVariable> variables)
        {
            return new OpenApiServer
            {
                Description = description,
                Url = url,
                Variables = variables
            };
        }

        public static IList<OpenApiServer> Servers(params OpenApiServer[] servers)
        {
            return new List<OpenApiServer>(servers);
        }

        public static OpenApiServerVariable ServerVariable(string description, string @default, params string[] @enum)
        {
            return new OpenApiServerVariable
            {
                Description = description,
                Default = @default,
                Enum = Enum(@enum)
            };
        }

        public static OpenApiTag Tag(
            string name,
            string description = null
        )
        {
            return new OpenApiTag
            {
                Name = name,
                Description = description
            };
        }

        public static IList<OpenApiTag> Tags(params OpenApiTag[] tags)
        {
            return new List<OpenApiTag>(tags);
        }

        public static KeyValuePair<string, OpenApiServerVariable> Variable(string key, OpenApiServerVariable variable)
        {
            return new KeyValuePair<string, OpenApiServerVariable>(key, variable);
        }

        public static IDictionary<string, OpenApiServerVariable> Variables(params KeyValuePair<string, OpenApiServerVariable>[] variables)
        {
            return new ConcurrentDictionary<string, OpenApiServerVariable>(variables);
        }

        private static IEnumerable<KeyValuePair<TKey, TValue>> Pairs<TKey, TValue>(
            params KeyValuePair<TKey, TValue>[] pairs
        )
        {
            return new List<KeyValuePair<TKey, TValue>>(pairs);
        }
    }
}
