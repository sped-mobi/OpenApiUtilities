// -----------------------------------------------------------------------
// <copyright file="SchemaFactory.cs" company="sped.mobi">
//     Copyright © 2018 Brad R. Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace OpenApiGenerator.InternalUtilities
{
    internal static class SchemaFactory
    {
        public static IDictionary<string, OpenApiSchema> CkanSchemas()
        {
            return new ConcurrentDictionary<string, OpenApiSchema>
            {
                //["ApiResult"] = ApiResultSchema(),
                ["PackageList"] = PackageListSchema(),
                ["ApiException"] = ApiExceptionSchema()
                //["URL"] = URLSchema(),
                //["UUID"] = UUIDSchema(),
                //["ErrorStatusInfo"] = ErrorStatusInfoSchema(),
                //["ErrorCodeMinor"] = ErrorCodeMinorSchema(),
                //["ErrorCodeMinorField"] = ErrorCodeMinorFieldSchema(),
            };
        }

        private static OpenApiSchema PackageListSchema()
        {
            return new OpenApiSchema
            {
                Type = "object",
                Description = "Returns a list of strings that represents the names of the site’s datasets (packages).",
                Properties = new ConcurrentDictionary<string, OpenApiSchema>
                {
                    ["help"] = URLSchema(),
                    ["success"] = BooleanSchema(),
                    ["result"] = ListOfStringSchema()
                },
                Required = new HashSet<string>
                {
                    "help",
                    "result",
                    "success"
                }
            };
        }

        private static OpenApiSchema ListOfStringSchema()
        {
            return ArraySchema("A list of strings representing the names of the site's datasets (packages).", "string");
        }

        private static OpenApiSchema ApiExceptionSchema()
        {
            return new OpenApiSchema
            {
                Type = "object",
                Properties = new ConcurrentDictionary<string, OpenApiSchema>
                {
                    ["message"] = new OpenApiSchema
                    {
                        Type = "string"
                    },
                    ["__type"] = new OpenApiSchema
                    {
                        Type = "string"
                    }
                },
                Required = new HashSet<string>
                {
                    "message",
                    "__type"
                }
            };
        }

        private static OpenApiSchema ApiResultSchema()
        {
            return new OpenApiSchema
            {
                Type = "object",
                Description = "Represents an Api Result.",
                Properties = new ConcurrentDictionary<string, OpenApiSchema>
                {
                    ["help"] = URLSchema(),
                    ["success"] = BooleanSchema(),
                    ["result"] = new OpenApiSchema
                    {
                        Type = "object"
                    },
                    ["error"] = new OpenApiSchema
                    {
                        Reference = new OpenApiReference
                        {
                            ExternalResource = "#/components/schemas/ApiException"
                        }
                    }
                }
            };
        }

        public static IDictionary<string, OpenApiSchema> CommonSchemas()
        {
            return new ConcurrentDictionary<string, OpenApiSchema>
            {
                ["URL"] = URLSchema(),
                ["UUID"] = UUIDSchema(),
                ["ErrorStatusInfo"] = ErrorStatusInfoSchema(),
                ["ErrorCodeMinor"] = ErrorCodeMinorSchema(),
                ["ErrorCodeMinorField"] = ErrorCodeMinorFieldSchema(),
            };
        }

        private static OpenApiSchema BooleanSchema()
        {
            return new OpenApiSchema
            {
                Description = "The data-type representing a boolean value.",
                Type = "boolean"
            };
        }

        private static OpenApiSchema URLSchema()
        {
            return Schema(
                "The data-type for establishing a Uniform Resource Locator (URL) as defined by W3C.",
                "string",
                "uri");
        }

        private static OpenApiSchema ArraySchema(string description, string arrayType)
        {
            return new OpenApiSchema
            {
                Type = "array",
                Description = description,
                Items = Schema(arrayType)
            };
        }

        private static OpenApiSchema ArraySchema(string description, string arrayType, string arrayFormat)
        {
            return new OpenApiSchema
            {
                Type = "array",
                Description = description,
                Items = Schema(arrayType, arrayFormat)
            };
        }

        private static OpenApiSchema Schema(string description, string type, string format)
        {
            return new OpenApiSchema
            {
                Description = "The data-type for establishing a Uniform Resource Locator (URL) as defined by W3C.",
                Type = "string",
                Format = "uri"
            };
        }

        private static OpenApiSchema UUIDSchema()
        {
            return new OpenApiSchema
            {
                Description = "The data-type for establishing a Globally Unique Identifier (GUID). The form of the GUID is a Universally Unique Identifier (UUID) of 16 hexadecimal characters (lower case) in the format 8-4-4-4-12. All permitted versions (1-5) and variants (1-2) are supported.",
                Type = "string",
                Format = "uuid"
            };
        }

        private static OpenApiSchema ErrorStatusInfoSchema()
        {
            return new OpenApiSchema
            {
                Type = "object",
                Description = "This is the container for the status code and associated information returned within the HTTP messages received from the Service Provider. For the CASE service this object will only be returned to provide information about a failed request i.e. it will NOT be in the payload for a successful request. See Appendix B for further information on the interpretation of the information contained within this class.",
                Properties = new ConcurrentDictionary<string, OpenApiSchema>
                {
                    ["codeMajor"] = new OpenApiSchema
                    {
                        Type = "string",
                        Enum = new List<IOpenApiAny>
                                {
                                    Api.String("success"),
                                    Api.String("processing"),
                                    Api.String("failure"),
                                    Api.String("unsupported")
                                }
                    },
                    ["severity"] = new OpenApiSchema
                    {
                        Type = "string",
                        Enum = new List<IOpenApiAny>
                                {
                                    Api.String("status"),
                                    Api.String("warning"),
                                    Api.String("error")
                                }
                    },
                    ["description"] = SchemaFactory.Schema("string"),
                    ["codeMinor"] = Api.SchemaReference(
                                            string.Format(ApiStrings.SchemaReferencePathFormat, "ErrorCodeMinor"))
                },
                Required = new HashSet<string>
                        {
                            "codeMajor",
                            "severity"
                        }

            };
        }

        private static OpenApiSchema ErrorCodeMinorSchema()
        {
            return new OpenApiSchema
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
            };
        }

        private static OpenApiSchema ErrorCodeMinorFieldSchema()
        {
            return new OpenApiSchema
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
                                    Api.String("fullsuccess"),
                                    Api.String("invalid_sort_field"),
                                    Api.String("invalid_selection_field"),
                                    Api.String("forbidden"),
                                    Api.String("unauthorisedrequest"),
                                    Api.String("internal_server_error"),
                                    Api.String("unknownobject"),
                                    Api.String("server_busy"),
                                    Api.String("invaliduuid"),
                                }
                    },
                },
                Required = new HashSet<string>
                        {
                            "name",
                            "value"
                        }
            };
        }

        public static IDictionary<string, OpenApiSchema> Properties(params (string, OpenApiSchema)[] properties)
        {
            var map = new ConcurrentDictionary<string, OpenApiSchema>();
            foreach (var property in properties)
            {
                if (!map.ContainsKey(property.Item1))
                {
                    map[property.Item1] = property.Item2;
                }
            }

            return map;
        }

        public static IList<OpenApiSchema> List(params OpenApiSchema[] schemas)
        {
            return new List<OpenApiSchema>(schemas);
        }

        public static OpenApiSchema ArraySchema(string schemaReferenceName)
        {
            return new OpenApiSchema
            {
                Type = "array",
                Items = Api.SchemaReference(
                    string.Format(ApiStrings.SchemaReferencePathFormat, schemaReferenceName))

            };
        }

        public static OpenApiSchema Schema(string type)
        {
            return new OpenApiSchema
            {
                Type = type
            };
        }

        public static OpenApiSchema Schema(string type, string format)
        {
            return new OpenApiSchema
            {
                Type = type,
                Format = format
            };
        }

        public static OpenApiSchema Schema(string type, string description, IDictionary<string, OpenApiSchema> properties)
        {
            return new OpenApiSchema
            {
                Type = type,
                Description = description,
                Properties = properties,
                Required = new HashSet<string>(
                    properties.Select(p => p.Key))
            };
        }

        public static OpenApiSchema Schema(string description, IDictionary<string, OpenApiSchema> properties)
        {
            return new OpenApiSchema
            {
                Type = "object",
                Description = description,
                Properties = properties,
                Required = new HashSet<string>(
                    properties.Select(p => p.Key))
            };
        }
    }
}
