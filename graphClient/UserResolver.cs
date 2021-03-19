using GraphQL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ranksClient
{
    public class UserResolver : ResolverBase
    {
        protected override GraphQLRequest QueryString
        {
            get => new GraphQLRequest
            {
                Query = @"{Users{name, secname,picture,groupId,rankId,about}}"
            };
        }
        protected override GraphQLRequest MutationStringCreate
        {
            get => new GraphQLRequest
            {
                Query = @"mutation($user: UserInput){AddUser(User: $user){
                        id,name,secname,groupId,rankId,isAdmin,password,picture,about}}"
            };
        }
        protected override GraphQLRequest MutationStringUpdate
        {
            get => new GraphQLRequest
            {
                Query = @"mutation($user: UserInput){UpdateUser(User: $user){
                        id,name,secname,groupId,rankId,isAdmin,password,picture,about}}"
            };
        }

        public async Task<List<UserInputType>> GetUsers()
        {
            var response = await GetList(() => new { Users = new List<UserInputType> { } });
            return (response.Users);
        }
        public async Task<UserInputTypeWithId> AddUser(UserInputType user)
        {
            var response = await Create(new{user=user}, () => new { AddUser = new UserInputTypeWithId() });
            return (response.AddUser);
        }
        public async Task<UserInputType> UpdateUser(UserInputTypeWithId user)
        {
            var response = await Update(new { user = user }, () => new { UpdateUser = new UserInputType() });
            return (response.UpdateUser);
        }
    }
    public class BigUserType: User
    { 
        public GroupType groupInfo { get; set; }
        public RankType rankInfo { get; set; }
    }
    public class UserInputType : User
    {
        public string groupId { get; set; }
        public string rankId { get; set; }
    }
    public class UserInputTypeWithId: User
    {

        public string id { get; set; }
        public string groupId { get; set; }
        public string rankId { get; set; }
    }
    public class User
    {
        public string name { get; set; }
        public string secname { get; set; }
        public string isAdmin { get; set; }
        public string password { get; set; }
        public string picture { get; set; }
        public string about { get; set; }
    }

}