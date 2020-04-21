using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Auth.Service.Models.DB;
using Microsoft.Azure.Cosmos;

namespace Auth.Service.Services
{
    public class MemberService: IMemberService
    {
        private readonly Container _container;
        private readonly string _databaseName;
        private readonly string _containerName;

        public MemberService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            _container = dbClient.GetContainer(databaseName, containerName);
            _databaseName = databaseName;
            _containerName = containerName;
        }

        public async Task<Member> GetMemberWithCredentialsAsync(string userId, string password) 
        {
            var sqlQuery = $"SELECT * FROM c WHERE c.id=\"{userId}\" AND c.password=\"{password}\"";
            var queryDefinition = new QueryDefinition(sqlQuery);
            var query = _container.GetItemQueryIterator<Member>(queryDefinition);
            Member member = null;
            while (query.HasMoreResults)
            {
                var result = await query.ReadNextAsync();
                foreach (var m in result)
                    member = m;
            }
            return member;
        }


        public async Task<bool> CheckMemberExistsAsync(string id) 
        {
                var sqlQuery = $"SELECT * FROM c WHERE c.id=\"{id}\"";
            var queryDefinition = new QueryDefinition(sqlQuery);
            var query = _container.GetItemQueryIterator<Member>(queryDefinition);
            int count = 0;
            while (query.HasMoreResults)
            {
                var result = await query.ReadNextAsync();
                count += result.Count;
            }
            return count > 0;
        }

        public async Task<bool> AddMemberAsync(AddMember member) {
            if (await CheckMemberExistsAsync(member.Id))
                return false;
            await _container.CreateItemAsync<AddMember>(member);
            return true;
        }

        public async Task<Member> GetMemberAsync(string id) {
            var sqlQuery = $"SELECT * FROM c WHERE c.id=\"{id}\"";
            var queryDefinition = new QueryDefinition(sqlQuery);
            var query = _container.GetItemQueryIterator<Member>(queryDefinition);
            Member member = null;
            while (query.HasMoreResults)
            {
                var members = await query.ReadNextAsync();
                foreach (var m in members) 
                    member = m;
            }
            return member;
        }
    }
}