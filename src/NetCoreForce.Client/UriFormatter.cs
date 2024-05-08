using System;
using System.Collections.Generic;
using NetCoreForce.Client.Models;

namespace NetCoreForce.Client
{
    /*
    In each case, the URI for the resource follows the base URI,
    which you retrieve from the authentication service: http://domain/services/data 
    https://developer.salesforce.com/docs/atlas.en-us.api_rest.meta/api_rest/resources_list.htm
    */
    public static class UriFormatter
    {
        /// <summary>
        /// SF Base URI Path
        /// </summary>
        // trailing slash required in services/data/ so that URI combinations work as expected
        public static string BaseUriPath() => "services/data/";

        /// <summary>
        /// SF Base URI
        /// </summary>
        /// <param name="instanceUrl"></param>
        public static Uri BaseUri(string instanceUrl)
        {
            if (string.IsNullOrEmpty(instanceUrl)) throw new ArgumentNullException("instanceUrl");

            // e.g. https://na99.salesforce.com/services/data
            return new Uri(new Uri(instanceUrl), BaseUriPath());
        }

        /// <summary>
        /// Versions Path
        /// </summary>
        public static string VersionsPath() => "services/data";

        /// <summary>
        /// Versions
        /// </summary>
        /// <param name="instanceUrl"></param>
        public static Uri Versions(string instanceUrl)
        {
            if (string.IsNullOrEmpty(instanceUrl)) throw new ArgumentNullException("instanceUrl");

            return new Uri(new Uri(instanceUrl), VersionsPath());
        }

        //TODO: Resources By Version https://developer.salesforce.com/docs/atlas.en-us.api_rest.meta/api_rest/resources_discoveryresource.htm

        /// <summary>
        /// Limits Resource URL
        /// <para>Use the Limits resource to list limits information for your organization.</para>
        /// </summary>
        public static Uri Limits(string instanceUrl, string apiVersion)
        {
            if (string.IsNullOrEmpty(instanceUrl)) throw new ArgumentNullException("instanceUrl");
            if (string.IsNullOrEmpty(apiVersion)) throw new ArgumentNullException("apiVersion");

            //format: /vXX.X/limits/

            return new Uri(BaseUri(instanceUrl), LimitsResource(apiVersion));
        }

        //split off full URL formatter and resource relative url formatters?
        //batch/tree reqs may need relative url parsers

        /// <summary>
        /// Limits Resource Path
        /// <para>Use the Limits resource to list limits information for your organization.</para>
        /// <para>format: /vXX.X/limits/</para>
        /// </summary>
        public static string LimitsResourcePath(string apiVersion)
        {
            if (string.IsNullOrEmpty(apiVersion)) throw new ArgumentNullException("apiVersion");

            return $"{apiVersion}/limits";
        }

        /// <summary>
        /// Limits Resource
        /// <para>Use the Limits resource to list limits information for your organization.</para>
        /// <para>format: /vXX.X/limits/</para>
        /// </summary>
        public static Uri LimitsResource(string apiVersion)
        {
            if (string.IsNullOrEmpty(apiVersion)) throw new ArgumentNullException("apiVersion");

            return new Uri(LimitsResourcePath(apiVersion), UriKind.Relative);
        }

        /// <summary>
        /// Describe Global Path
        /// Use the Describe Global resource to list the objects available in your org and available to the logged-in user.
        /// This resource also returns the org encoding, as well as maximum batch size permitted in queries.
        /// </summary>
        public static string DescribeGlobalPath(string apiVersion)
        {
            if (string.IsNullOrEmpty(apiVersion)) throw new ArgumentNullException("apiVersion");

            //format: /vXX.X/sobjects/
            return $"{apiVersion}/sobjects";
        }

        /// <summary>
        /// Describe Global
        /// Use the Describe Global resource to list the objects available in your org and available to the logged-in user.
        /// This resource also returns the org encoding, as well as maximum batch size permitted in queries.
        /// </summary>
        public static Uri DescribeGlobal(string instanceUrl, string apiVersion)
        {
            if (string.IsNullOrEmpty(instanceUrl)) throw new ArgumentNullException("instanceUrl");
            if (string.IsNullOrEmpty(apiVersion)) throw new ArgumentNullException("apiVersion");

            //format: /vXX.X/sobjects/
            return new Uri(BaseUri(instanceUrl), DescribeGlobalPath(apiVersion));
        }

        /// <summary>
        /// SObject Basic Information Path
        /// Describes the individual metadata for the specified object. Can also be used to create a new record for a given object.
        /// </summary>
        public static string SObjectBasicInformationPath(string apiVersion, string sObjectName)
        {
            if (string.IsNullOrEmpty(apiVersion)) throw new ArgumentNullException("apiVersion");
            if (string.IsNullOrEmpty(sObjectName)) throw new ArgumentNullException("sObjectName");

            //format: /vXX.X/sobjects/SObjectName/
            return $"{apiVersion}/sobjects/{sObjectName}";
        }

        /// <summary>
        /// SObject Basic Information
        /// Describes the individual metadata for the specified object. Can also be used to create a new record for a given object.
        /// </summary>
        public static Uri SObjectBasicInformation(string instanceUrl, string apiVersion, string sObjectName)
        {
            if (string.IsNullOrEmpty(instanceUrl)) throw new ArgumentNullException("instanceUrl");
            if (string.IsNullOrEmpty(sObjectName)) throw new ArgumentNullException("sObjectName");
            if (string.IsNullOrEmpty(apiVersion)) throw new ArgumentNullException("apiVersion");

            //format: /vXX.X/sobjects/SObjectName/
            return new Uri(BaseUri(instanceUrl), SObjectBasicInformationPath(apiVersion, sObjectName));
        }

        /// <summary>
        /// SObject Describe Path
        /// Completely describes the individual metadata at all levels for the specified object.
        /// </summary>
        public static string SObjectDescribePath(string apiVersion, string sObjectName)
        {
            if (string.IsNullOrEmpty(apiVersion)) throw new ArgumentNullException("apiVersion");
            if (string.IsNullOrEmpty(sObjectName)) throw new ArgumentNullException("sObjectName");

            //format: /vXX.X/sobjects/SObjectName/describe/
            return $"{apiVersion}/sobjects/{sObjectName}/describe";
        }

        /// <summary>
        /// SObject Describe
        /// Completely describes the individual metadata at all levels for the specified object.
        /// </summary>
        public static Uri SObjectDescribe(string instanceUrl, string apiVersion, string sObjectName)
        {
            if (string.IsNullOrEmpty(instanceUrl)) throw new ArgumentNullException("instanceUrl");
            if (string.IsNullOrEmpty(sObjectName)) throw new ArgumentNullException("sObjectName");
            if (string.IsNullOrEmpty(apiVersion)) throw new ArgumentNullException("apiVersion");

            //format: /vXX.X/sobjects/SObjectName/describe/
            return new Uri(BaseUri(instanceUrl), SObjectDescribePath(apiVersion, sObjectName));
        }

        //TODO: SObject Get Deleted

        //TODO: SObject Get Updated

        //TODO: SObject Named Layouts

        /// <summary>
        /// SObject Rows Resource Path
        /// Used for: Update, Delete, Field values
        /// </summary>
        /// <param name="apiVersion">SFDC API version, e.g. "v57.0"</param>
        /// <param name="sObjectName">SObject name, e.g. "Account"</param>
        /// <param name="objectId">SObject ID</param>
        /// <param name="fields">(optional) "fields" parameter, a list of object fields for GET requests</param>
        /// <returns></returns>
        public static string SObjectRowsPath(string apiVersion, string sObjectName, string objectId, List<string> fields = null)
        {
            if (string.IsNullOrEmpty(apiVersion)) throw new ArgumentNullException("apiVersion");
            if (string.IsNullOrEmpty(sObjectName)) throw new ArgumentNullException("sObjectName");
            if (string.IsNullOrEmpty(objectId)) throw new ArgumentNullException("objectId");

            //https://developer.salesforce.com/docs/atlas.en-us.api_rest.meta/api_rest/resources_sobject_retrieve.htm

            // format: /vXX.X/sobjects/SObjectName/id/
            // example with field parameter: services/data/v20.0/sobjects/Account/001D000000INjVe?fields=AccountNumber,BillingPostalCode            

            string path = $"{apiVersion}/sobjects/{sObjectName}/{objectId}";

            if (fields != null && fields.Count > 0)
            {
                string fieldList = string.Join(",", fields);
                path = QueryHelpers.AddQueryString(path, "fields", fieldList);
            }

            return path;
        }

        /// <summary>
        /// SObject Rows Resource
        /// Used for: Update, Delete, Field values
        /// </summary>
        /// <param name="instanceUrl">SFDC instance URL, e.g. "https://na99.salesforce.com"</param>
        /// <param name="apiVersion">SFDC API version, e.g. "v57.0"</param>
        /// <param name="sObjectName">SObject name, e.g. "Account"</param>
        /// <param name="objectId">SObject ID</param>
        /// <param name="fields">(optional) "fields" parameter, a list of object fields for GET requests</param>
        /// <returns></returns>
        public static Uri SObjectRows(string instanceUrl, string apiVersion, string sObjectName, string objectId, List<string> fields = null)
        {
            if (string.IsNullOrEmpty(instanceUrl)) throw new ArgumentNullException("instanceUrl");
            if (string.IsNullOrEmpty(apiVersion)) throw new ArgumentNullException("apiVersion");
            if (string.IsNullOrEmpty(sObjectName)) throw new ArgumentNullException("sObjectName");
            if (string.IsNullOrEmpty(objectId)) throw new ArgumentNullException("objectId");

            //https://developer.salesforce.com/docs/atlas.en-us.api_rest.meta/api_rest/resources_sobject_retrieve.htm

            // format: /vXX.X/sobjects/SObjectName/id/
            // example with field parameter: services/data/v20.0/sobjects/Account/001D000000INjVe?fields=AccountNumber,BillingPostalCode            

            //Uri uri = new Uri(BaseUri(instanceUrl), $"{apiVersion}/sobjects/{sObjectName}/{objectId}");
            return new Uri(BaseUri(instanceUrl), SObjectRowsPath(apiVersion, sObjectName, objectId, fields));
        }

        /// <summary>
        /// SObject Composite Path
        /// Used for: Update multiple
        /// </summary>
        /// <param name="apiVersion">SFDC API version, e.g. "v57.0"</param>
        /// <returns></returns>
        public static string SObjectsCompositePath(string apiVersion)
        {
            if (string.IsNullOrEmpty(apiVersion)) throw new ArgumentNullException("apiVersion");

            //format: /vXX.X/composite/sobjects
            return $"{apiVersion}/composite/sobjects";
        }

        /// <summary>
        /// SObject Composite
        /// Used for: Update multiple
        /// </summary>
        /// <param name="instanceUrl">SFDC instance URL, e.g. "https://na99.salesforce.com"</param>
        /// <param name="apiVersion">SFDC API version, e.g. "v57.0"</param>
        /// <returns></returns>
        public static Uri SObjectsComposite(string instanceUrl, string apiVersion)
        {
            if (string.IsNullOrEmpty(instanceUrl)) throw new ArgumentNullException("instanceUrl");
            if (string.IsNullOrEmpty(apiVersion)) throw new ArgumentNullException("apiVersion");

            //format: /vXX.X/composite/sobjects
            return new Uri(BaseUri(instanceUrl), SObjectsCompositePath(apiVersion));
        }

        /// <summary>
        /// sObject Tree Path
        /// Used for: Create multiple
        /// </summary>
        /// <param name="apiVersion">SFDC API version, e.g. "v57.0"</param>
        /// <param name="sObjectName">sObject name, e.g. "Account"</param>
        /// <returns></returns>
        public static string SObjectTreePath(string apiVersion, string sObjectName)
        {
            if (string.IsNullOrEmpty(apiVersion)) throw new ArgumentNullException("apiVersion");
            if (string.IsNullOrEmpty(sObjectName)) throw new ArgumentNullException("sObjectName");

            //format: /vXX.X/composite/tree/sObjectName
            return $"{apiVersion}/composite/tree/{sObjectName}";
        }

        /// <summary>
        /// sObject Tree
        /// Used for: Create multiple
        /// </summary>
        /// <param name="instanceUrl">SFDC instance URL, e.g. "https://na99.salesforce.com"</param>
        /// <param name="apiVersion">SFDC API version, e.g. "v57.0"</param>
        /// <param name="sObjectName">sObject name, e.g. "Account"</param>
        /// <returns></returns>
        public static Uri SObjectTree(string instanceUrl, string apiVersion, string sObjectName)
        {
            if (string.IsNullOrEmpty(instanceUrl)) throw new ArgumentNullException("instanceUrl");
            if (string.IsNullOrEmpty(apiVersion)) throw new ArgumentNullException("apiVersion");
            if (string.IsNullOrEmpty(sObjectName)) throw new ArgumentNullException("sObjectName");

            //format: /vXX.X/composite/tree/sObjectName
            return new Uri(BaseUri(instanceUrl), SObjectTreePath(apiVersion, sObjectName));
        }

        /// <summary>
        /// SObject Rows by External ID Path
        /// Used for:
        /// Retrieve Records Using sObject Rows by External ID
        /// Upsert Records Using sObject Rows by External ID
        /// Delete Records Using sObject Rows by External ID
        /// Return Headers Using sObject Rows by External ID 
        /// </summary>
        /// <param name="apiVersion">SFDC API version, e.g. "v57.0"</param>
        /// <param name="sObjectName">sObject name, e.g. "Account"</param>
        /// <param name="fieldName"></param>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        public static string SObjectRowsByExternalIdPath(string apiVersion, string sObjectName, string fieldName, string fieldValue)
        {
            if (string.IsNullOrEmpty(apiVersion)) throw new ArgumentNullException("apiVersion");
            if (string.IsNullOrEmpty(sObjectName)) throw new ArgumentNullException("sObjectName");
            if (string.IsNullOrEmpty(fieldName)) throw new ArgumentNullException("fieldName");
            if (string.IsNullOrEmpty(fieldName)) throw new ArgumentNullException("fieldValue");

            //format: /vXX.X/sobjects/SObjectName/fieldName/fieldValue
            return $"{apiVersion}/sobjects/{sObjectName}/{fieldName}/{fieldValue}";
        }

        /// <summary>
        /// SObject Rows by External ID
        /// Used for:
        /// Retrieve Records Using sObject Rows by External ID
        /// Upsert Records Using sObject Rows by External ID
        /// Delete Records Using sObject Rows by External ID
        /// Return Headers Using sObject Rows by External ID 
        /// </summary>
        /// <param name="instanceUrl">SFDC instance URL, e.g. "https://na99.salesforce.com"</param>
        /// <param name="apiVersion">SFDC API version, e.g. "v57.0"</param>
        /// <param name="sObjectName">sObject name, e.g. "Account"</param>
        /// <param name="fieldName"></param>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        public static Uri SObjectRowsByExternalId(string instanceUrl, string apiVersion, string sObjectName, string fieldName, string fieldValue)
        {
            if (string.IsNullOrEmpty(instanceUrl)) throw new ArgumentNullException("instanceUrl");
            if (string.IsNullOrEmpty(apiVersion)) throw new ArgumentNullException("apiVersion");
            if (string.IsNullOrEmpty(sObjectName)) throw new ArgumentNullException("sObjectName");
            if (string.IsNullOrEmpty(fieldName)) throw new ArgumentNullException("fieldName");
            if (string.IsNullOrEmpty(fieldName)) throw new ArgumentNullException("fieldValue");

            //format: /vXX.X/sobjects/SObjectName/fieldName/fieldValue
            return new Uri(BaseUri(instanceUrl), SObjectRowsByExternalIdPath(apiVersion, sObjectName, fieldName, fieldValue));
        }

        /// <summary>
        /// SObjectCollections Upsert Path
        /// </summary>
        /// <param name="apiVersion">SFDC API version, e.g. "v57.0"</param>
        /// <param name="sObjectName">sObject name, e.g. "Account"</param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static string SObjectCollectionsUpsertPath(string apiVersion, string sObjectName, string fieldName)
        {
            if (string.IsNullOrEmpty(apiVersion)) throw new ArgumentNullException("apiVersion");
            if (string.IsNullOrEmpty(sObjectName)) throw new ArgumentNullException("sObjectName");
            if (string.IsNullOrEmpty(fieldName)) throw new ArgumentNullException("fieldName");

            //format: /vXX.X/sobjects/SObjectName/fieldName/fieldValue
            return $"{apiVersion}/sobjects/{sObjectName}/{fieldName}";
        }

        /// <summary>
        /// SObjectCollections Upsert
        /// </summary>
        /// <param name="instanceUrl">SFDC instance URL, e.g. "https://na99.salesforce.com"</param>
        /// <param name="apiVersion">SFDC API version, e.g. "v57.0"</param>
        /// <param name="sObjectName">sObject name, e.g. "Account"</param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static Uri SObjectCollectionsUpsert(string instanceUrl, string apiVersion, string sObjectName, string fieldName)
        {
            if (string.IsNullOrEmpty(instanceUrl)) throw new ArgumentNullException("instanceUrl");
            if (string.IsNullOrEmpty(apiVersion)) throw new ArgumentNullException("apiVersion");
            if (string.IsNullOrEmpty(sObjectName)) throw new ArgumentNullException("sObjectName");
            if (string.IsNullOrEmpty(sObjectName)) throw new ArgumentNullException("sObjectName");
            if (string.IsNullOrEmpty(fieldName)) throw new ArgumentNullException("fieldName");

            //format: /vXX.X/sobjects/SObjectName/fieldName/fieldValue
            return new Uri(BaseUri(instanceUrl), SObjectCollectionsUpsertPath(apiVersion, sObjectName, fieldName));
        }

        //SObject Relationships Resource
        //TODO: Traverse Relationships with Friendly URLs.
        //https://developer.salesforce.com/docs/atlas.en-us.api_rest.meta/api_rest/resources_sobject_relationships.htm#topic-title
        ///format: vXX.X/sobjects/SObject/id/relationship field name

        /// <summary>
        /// SObject Blob Path
        /// </summary>
        public static string SObjectBlobRetrievePath(string apiVersion, string sObjectName, string objectId, string blobField = "body")
        {
            if (string.IsNullOrEmpty(apiVersion)) throw new ArgumentNullException("apiVersion");
            if (string.IsNullOrEmpty(sObjectName)) throw new ArgumentNullException("sObjectName");
            if (string.IsNullOrEmpty(objectId)) throw new ArgumentNullException("objectId");
            if (string.IsNullOrEmpty(blobField)) throw new ArgumentNullException("blobField");

            //format: /vXX.X/sobjects/SObjectName/id/

            // https://yourInstance.salesforce.com/services/data/v20.0/sobjects/Attachment/001D000000INjVe/body
            // https://yourInstance.salesforce.com/services/data/v20.0/sobjects/Document/015D0000000NdJOIA0/body

            return $"{apiVersion}/sobjects/{sObjectName}/{objectId}/{blobField}";
        }


        /// <summary>
        /// SObject Blob
        /// </summary>
        public static Uri SObjectBlobRetrieve(string instanceUrl, string apiVersion, string sObjectName, string objectId, string blobField = "body")
        {
            if (string.IsNullOrEmpty(instanceUrl)) throw new ArgumentNullException("instanceUrl");
            if (string.IsNullOrEmpty(apiVersion)) throw new ArgumentNullException("apiVersion");
            if (string.IsNullOrEmpty(sObjectName)) throw new ArgumentNullException("sObjectName");
            if (string.IsNullOrEmpty(objectId)) throw new ArgumentNullException("objectId");
            if (string.IsNullOrEmpty(blobField)) throw new ArgumentNullException("blobField");

            //format: /vXX.X/sobjects/SObjectName/id/

            // https://yourInstance.salesforce.com/services/data/v20.0/sobjects/Attachment/001D000000INjVe/body
            // https://yourInstance.salesforce.com/services/data/v20.0/sobjects/Document/015D0000000NdJOIA0/body

            return new Uri(BaseUri(instanceUrl), SObjectBlobRetrievePath(apiVersion, sObjectName, objectId, blobField));
        }

        /// <summary>
        /// SOQL Query Path
        /// </summary>
        public static string QueryPath(string apiVersion, string query, bool queryAll = false)
        {
            if (string.IsNullOrEmpty(apiVersion)) throw new ArgumentNullException("apiVersion");
            if (string.IsNullOrEmpty(query)) throw new ArgumentNullException("query");

            string queryType = "query";
            if (queryAll)
            {
                queryType = "queryAll";
            }

            //string path = string.Format("/services/data/{0}/{1}/?q={2}", apiVersion, queryType, query);

            string path = $"{apiVersion}/{queryType}";
            return QueryHelpers.AddQueryString(path, "q", query);
        }

        /// <summary>
        /// SOQL Query
        /// </summary>
        public static Uri Query(string instanceUrl, string apiVersion, string query, bool queryAll = false)
        {
            if (string.IsNullOrEmpty(instanceUrl)) throw new ArgumentNullException("instanceUrl");
            if (string.IsNullOrEmpty(apiVersion)) throw new ArgumentNullException("apiVersion");
            if (string.IsNullOrEmpty(query)) throw new ArgumentNullException("query");

            return new Uri(BaseUri(instanceUrl), QueryPath(apiVersion, query, queryAll));
        }

        /// <summary>
        /// Search Resource Path, for SOSL searches
        /// </summary>
        public static string SearchPath(string apiVersion, string query)
        {
            if (string.IsNullOrEmpty(apiVersion)) throw new ArgumentNullException("apiVersion");
            if (string.IsNullOrEmpty(query)) throw new ArgumentNullException("query");

            string path = $"{apiVersion}/search";
            return QueryHelpers.AddQueryString(path, "q", query);
        }

        /// <summary>
        /// Search Resource, for SOSL searches
        /// </summary>
        public static Uri Search(string instanceUrl, string apiVersion, string query)
        {
            if (string.IsNullOrEmpty(instanceUrl)) throw new ArgumentNullException("instanceUrl");
            if (string.IsNullOrEmpty(apiVersion)) throw new ArgumentNullException("apiVersion");
            if (string.IsNullOrEmpty(query)) throw new ArgumentNullException("query");

            return new Uri(BaseUri(instanceUrl), SearchPath(apiVersion, query));
        }

        /// <summary>
        /// Batch Resource Path
        /// </summary>
        /// <param name="apiVersion"></param>
        public static string BatchPath(string apiVersion)
        {
            if (string.IsNullOrEmpty(apiVersion)) throw new ArgumentNullException("apiVersion");

            //format: /vXX.X/composite/batch
            return $"{apiVersion}/composite/batch";
        }

        /// <summary>
        /// Batch Resource
        /// </summary>
        /// <param name="instanceUrl"></param>
        /// <param name="apiVersion"></param>
        public static Uri Batch(string instanceUrl, string apiVersion)
        {
            if (string.IsNullOrEmpty(instanceUrl)) throw new ArgumentNullException("instanceUrl");
            if (string.IsNullOrEmpty(apiVersion)) throw new ArgumentNullException("apiVersion");

            //format: /vXX.X/composite/batch
            return new Uri(BaseUri(instanceUrl), BatchPath(apiVersion));
        }

        /// <summary>
        /// Formats an authentication URL for the User-Agent OAuth Authentication Flow
        /// </summary>
        /// <param name="loginUrl">(Required) Salesforce authorization endpoint.</param>
        /// <param name="clientId">(Required) The Consumer Key from the connected app definition.</param>
        /// <param name="redirectUrl">(Required) The Callback URL from the connected app definition.</param>
        /// <param name="display">Changes the login page’s display type</param>
        /// <param name="scope">OAuth scope - specifies what data your application can access</param>
        /// <param name="state">Specifies any additional URL-encoded state data to be returned in the callback URL after approval.</param>
        public static Uri UserAgentAuthenticationUrl(
            string loginUrl,
            string clientId,
            string redirectUrl,
            DisplayTypes display = DisplayTypes.Page,
            string state = "",
            string scope = "")
        {
            if (string.IsNullOrEmpty(loginUrl)) throw new ArgumentNullException("loginUrl");
            if (string.IsNullOrEmpty(clientId)) throw new ArgumentNullException("clientId");
            if (string.IsNullOrEmpty(redirectUrl)) throw new ArgumentNullException("redirectUrl");

            const ResponseTypes responseType = ResponseTypes.Token;

            Dictionary<string, string> prms = new Dictionary<string, string>();
            prms.Add("response_type", responseType.ToString().ToLower());
            prms.Add("client_id", clientId);
            prms.Add("redirect_uri", redirectUrl);
            prms.Add("display", display.ToString().ToLower());

            if (!string.IsNullOrEmpty(scope))
            {
                prms.Add("scope", scope);
            }

            if (!string.IsNullOrEmpty(state))
            {
                prms.Add("state", state);
            }            

            string url = QueryHelpers.AddQueryString(loginUrl, prms);

            return new Uri(url);
        }

        //TODO: parser for redirect url result

        /// <summary>
        /// Formats an authentication URL for the Web Server Authentication Flow
        /// </summary>
        /// <param name="loginUrl">Required. Salesforce authorization endpoint.</param>
        /// <param name="clientId">Required. The Consumer Key from the connected app definition.</param>
        /// <param name="redirectUrl">Required. The Callback URL from the connected app definition.</param>
        /// <param name="display">Changes the login page’s display type</param>
        /// <param name="immediate">Determines whether the user should be prompted for login and approval. Default is false.</param>
        /// <param name="scope">OAuth scope - specifies what data your application can access</param>
        /// <param name="state">Specifies any additional URL-encoded state data to be returned in the callback URL after approval.</param>
        public static Uri WebServerAuthenticationUrl(
            string loginUrl,
            string clientId,
            string redirectUrl,
            DisplayTypes display = DisplayTypes.Page,
            bool immediate = false,
            string scope = "",
            string state = ""
            )
        {
            if (string.IsNullOrEmpty(loginUrl)) throw new ArgumentNullException("loginUrl");
            if (string.IsNullOrEmpty(clientId)) throw new ArgumentNullException("clientId");
            if (string.IsNullOrEmpty(redirectUrl)) throw new ArgumentNullException("redirectUrl");

            //TODO: code_challenge, login_hint, nonce, prompt params

            const ResponseTypes responseType = ResponseTypes.Code;

            Dictionary<string, string> prms = new Dictionary<string, string>();
            prms.Add("response_type", responseType.ToString().ToLower());
            prms.Add("client_id", clientId);
            prms.Add("redirect_uri", redirectUrl);
            prms.Add("display", display.ToString().ToLower());
            prms.Add("immediate", immediate.ToString().ToLower());

            if (!string.IsNullOrEmpty(scope))
            {
                prms.Add("scope", scope);
            }

            if (!string.IsNullOrEmpty(state))
            {
                prms.Add("state", state);
            }            

            string url = QueryHelpers.AddQueryString(loginUrl, prms);

            return new Uri(url);
        }

        /// <summary>
        /// Formats a URL to request a OAuth Refresh Token
        /// </summary>
        /// <param name="tokenRefreshUrl"></param>
        /// <param name="refreshToken">The refresh token the client application already received.</param>
        /// <param name="clientId">The Consumer Key from the connected app definition.</param>        
        /// <param name="clientSecret">The Consumer Secret from the connected app definition. Required unless the Require Secret for Web Server Flow setting is not enabled in the connected app definition.</param>
        /// <returns></returns>
        public static Uri RefreshTokenUrl(
            string tokenRefreshUrl,
            string refreshToken,
            string clientId,            
            string clientSecret = "")
        {
            if (tokenRefreshUrl == null) throw new ArgumentNullException("tokenRefreshUrl");
            if (refreshToken == null) throw new ArgumentNullException("refreshToken");
            if (clientId == null) throw new ArgumentNullException("clientId");            

            Dictionary<string, string> prms = new Dictionary<string, string>();
            prms.Add("grant_type", "refresh_token");
            prms.Add("refresh_token", refreshToken);
            prms.Add("client_id", clientId);
            if(!string.IsNullOrEmpty(clientSecret))
            {
                prms.Add("client_secret", clientSecret);
            }            
            prms.Add("format", "json");

            string url = QueryHelpers.AddQueryString(tokenRefreshUrl, prms);

            return new Uri(url);
        }
    }
}