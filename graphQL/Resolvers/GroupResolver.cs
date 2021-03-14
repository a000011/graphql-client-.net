using GraphQL;
using GraphQL.Client.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphQL.Resolvers
{
    public class GroupResolver : ResolverBase
    {
        public GroupResolver(GraphQLHttpClient client) : base(client) { }
        protected override GraphQLRequest QueryString
        {
            get => new GraphQLRequest
            {
                Query = @"{Groups{id name about}}"
            };
        }
        protected override GraphQLRequest MutationStringCreate
        {
            get => new GraphQLRequest
            {
                Query = @"mutation($group: GroupInput){AddGroup(Group: $group){name,id,picture,about}}"
            };
        }
        protected override GraphQLRequest MutationStringUpdate
        {
            get => new GraphQLRequest
            {
                Query = @"mutation($group: GroupUpdateInput){UpdateGroup(Group: $group){name,id,picture,about}}"
            };
        }

        public async Task<List<GroupInputType>> GetAsync()
        {
            var response = await GetList(() => new { groups = new List<GroupInputType> { } });
            return (response.groups);
        }
        public async Task<GroupInputType> AddGroup(GroupInputType group)
        {
            var response = await Create(new { group = group}, () => new { AddGroup = new GroupType() });
            return (response.AddGroup);
        }
        public async Task<GroupInputType> UpdateGroup(GroupType group)
        {
            var response = await Update(new { group = group }, () => new { UpdateGroup = new GroupType() });
            return (response.UpdateGroup);
        }
    }
    public class GroupInputType
    {
        public string name { get; set; }
        public string picture { get; set; }
        public string about { get; set; }

    }
    public class GroupType : GroupInputType
    {
        public string id { get; set; }

    }

}
