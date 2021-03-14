using GraphQL;
using GraphQL.Client.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphQL.Resolvers
{
    public class UserResolver : ResolverBase
    {
        public UserResolver(GraphQLHttpClient client) : base(client) { }
        protected override GraphQLRequest QueryString
        {
            get => new GraphQLRequest
            {
                Query = @"{
	                Users{
                    id
                    name
                    secname
                    picture
                    about
                  }
                }"
            };
        }
        protected override GraphQLRequest MutationStringCreate
        {
            get => new GraphQLRequest
            {
                Query = @"mutation($user: UserInput){AddUser(User: $user){
                            name,id, secname,userGroup,rank,isAdmin,password,picture,about}}"
            };
        }
        protected override GraphQLRequest MutationStringUpdate
        {
            get => new GraphQLRequest
            {
                Query = @"mutation($user: UserUpdateInput){UpdateUser(User: $user){name,secname,userGroup,id,rank,isAdmin,password,picture,about}}"
            };
        }

        public async Task<List<UserType>> GetUsers()
        {
            var response = await GetList(() => new { Users = new List<UserType> { } });
            return (response.Users);
        }
        public async Task<UserType> AddUser(UserInputType user)
        {
            var response = await Create(new{User=user}, () => new { AddUser = new UserType() });
            return (response.AddUser);
        }
        public async Task<UserType> UpdateUser(UserType user)
        {
            var response = await Update(new { User = user }, () => new { UpdateUser = new UserType() });
            return (response.UpdateUser);
        }
    }
    public class UserType: UserInputType
    { 
        public string id { get; set; }
    }
    public class UserInputType
    {
        public string name { get; set; }
        public string secname { get; set; }
        public string userGroup { get; set; }
        public string rank { get; set; }
        public string isAdmin { get; set; }
        public string password { get; set; }
        public string picture { get; set; }
        public string about { get; set; }
    }


}
