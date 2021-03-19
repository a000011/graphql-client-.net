using GraphQL;
using GraphQL.Client.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ranksClient
{
    class RankResolver: ResolverBase
    {
        
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
                Query = @"mutation($rank: rankInput){AddRank(Rank: $rank){name,id,picture}}"
            };
        }
        protected override GraphQLRequest MutationStringUpdate
        {
            get => new GraphQLRequest
            {
                Query = @"mutation($rank: rankUpdateInput){UpdateRank(Rank: $rank){name,id,picture}}"
            };
        }

        public async Task<List<RankInputType>> GetRanks()
        {
            var response = await GetList(() => new { Ranks = new List<RankInputType> { } });
            return (response.Ranks);
        }
        public async Task<RankInputType> AddRank(RankInputType rank)
        {
            var response = await Create(new { rank = rank }, () => new { AddRank = new RankType() });
            return (response.AddRank);
        }
        public async Task<RankType> UpdateRank(RankType rank)
        {
            var response = await Update(new { rank = rank }, () => new { UpdateRank = new RankType() });
            return (response.UpdateRank);
        }
        
    }
    public class RankType : RankInputType
    {
        public string id { get; set; }
    }
    public class RankInputType
    {
        public string name { get; set; }
        public string picture { get; set; }
    }
}
