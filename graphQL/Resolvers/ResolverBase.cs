﻿using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Abstractions;
using System.Threading.Tasks;
using System;

namespace graphQL.Resolvers
{
    public abstract class ResolverBase
    {
        public ResolverBase(GraphQLHttpClient graphql_client)
        {
            this.GraphQLClient = graphql_client;
        }

        protected GraphQLHttpClient GraphQLClient { get; set; }

        protected abstract GraphQLRequest QueryString { get; }
        protected abstract GraphQLRequest MutationStringCreate { get; }
        protected abstract GraphQLRequest MutationStringUpdate { get; }
        
        protected async Task<Response> GetList<Response>(Func<Response> defineResponseType)
        {
            var response = await GraphQLClient.SendQueryAsync(QueryString, defineResponseType);
            return (response.Data);
        }

        protected async Task<Response> Create<Response>(object variable, Func<Response> defineResponseType)
        {
            GraphQLRequest add_request = MutationStringCreate;
            add_request.Variables = variable;
            var response = await GraphQLClient.SendQueryAsync(add_request, defineResponseType);
            return (response.Data);
        }

        protected async Task<Response> Update<Response>(object variable, Func<Response> defineResponseType)
        {
            GraphQLRequest Update_request = MutationStringUpdate;
            Update_request.Variables = variable;
            var response = await GraphQLClient.SendQueryAsync(Update_request, defineResponseType);
            return (response.Data);
        }
    }
}