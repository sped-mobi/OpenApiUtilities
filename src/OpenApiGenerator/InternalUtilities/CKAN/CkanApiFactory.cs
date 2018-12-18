// -----------------------------------------------------------------------
// <copyright file="CkanApiFactory.cs" company="sped.mobi">
//     Copyright © 2018 Brad R. Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.OpenApi.Models;

namespace OpenApiGenerator.InternalUtilities.CKAN
{
    internal static class CkanApiFactory
    {
        public static OpenApiDocument Document()
        {
            return new OpenApiDocument
            {
                Info = Api.Info(
                    CkanResources.Document_Info_Title,
                    CkanResources.Document_Info_Version,
                    CkanResources.Document_Info_Description,
                    CkanResources.Document_Info_TermsOfService,
                    Api.Contact(
                            CkanResources.Document_Info_Contact_Email,
                            CkanResources.Document_Info_Contact_Name,
                            CkanResources.Document_Info_Contact_Url),
                    Api.License(
                            CkanResources.Document_Info_License_Name,
                            CkanResources.Document_Info_License_Url)
                        ),
                Servers = Api.Servers(
                    Api.Server(
                        CkanResources.Document_Servers_Server_Description,
                        CkanResources.Document_Servers_Server_Url,
                        Api.Variables(
                            Api.Variable(
                                "scheme",
                                Api.ServerVariable(
                                    CkanResources.Document_Servers_Server_SchemeVariable_Description,
                                    "http",
                                    "http",
                                    "https"
                                )
                            ),
                            Api.Variable(
                                "basePath",
                                Api.ServerVariable(
                                    CkanResources.Document_Servers_Server_BasePathVariable_Description,
                                    "api/3/action"
                                )
                            )
                        )
                    )
                ),
                Components = Components(),
                Paths = Api.Paths(
                    Api.Path("/package_list",
                        Api.PathItem(OperationFactory.OperationMap(OperationType.Get,
                          OperationFactory.Operation("PackageList",
                            "Return a list of the names of the site’s datasets (packages).",
                            "The REST read message request for the PackageList() API call.",
                            "PackageManager",
                            ResponseFactory.CKAN_Responses("PackageList", "list of package names as strings"),
                          ParameterFactory.IntegerQueryParameter("limit",
                            "If given, the list of datasets will be broken into pages of the specified limit of datasets per page and only one page will be returned at a time (optional)"),
                          ParameterFactory.IntegerQueryParameter("offset",
                            "When limit is given, the offset to start returning packages from."))))),
                    Api.Path("/current_package_list_with_resources",
                        Api.PathItem(OperationFactory.OperationMap(OperationType.Get,
                          OperationFactory.Operation("CurrentPackageListWithResources",
                            "Return a list of the site’s datasets (packages) and their resources.The list is sorted most-recently-modified first.",
                            "The REST read message request for the CurrentPackageListWithResources() API call.",
                            "PackageManager",
                            ResponseFactory.CKAN_Responses("PackageListWithResources", "list of the site’s datasets (packages) and their resources."),
                          ParameterFactory.IntegerQueryParameter("limit",
                            "If given, the list of datasets will be broken into pages of at most the specified limit datasets per page and only one page will be returned at a time (optional)"),
                          ParameterFactory.IntegerQueryParameter("offset",
                            "When limit is given, the offset to start returning packages from."),
                          ParameterFactory.IntegerQueryParameter("page", "when limit is given, which page to return.", true)))))
                    ),
                Tags = TagFactory.Tags(
                        TagFactory.Tag("PackageManager", "The set of service operations that manage access to the site's datasets (packages)."))
            };
        }


        private static OpenApiComponents Components()
        {
            return new OpenApiComponents
            {
                Schemas = SchemaFactory.CkanSchemas()
            };
        }


    }
}
