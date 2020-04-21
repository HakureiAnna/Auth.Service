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

        public bool CheckValidMember(string userId, string passCode) 
        {
            var sqlQuery = $"SELECT * FROM MEMBER m WHERE m.USER_ID={userId} AND PASS_CODE={passCode}";
            var queryDefinition = new QueryDefinition(sqlQuery);
            var query = _container.GetItemQueryIterator<Member>(queryDefinition);
            int count = 0;
            while (query.HasMoreResults)
            {
                count++;
            }
            return count > 0;
        }

        public async Task<bool> CheckMemberExists(string id) 
        {
            var sqlQuery = $"SELECT * FROM c WHERE c.id=\"{id}\"";
            var queryDefinition = new QueryDefinition(sqlQuery);
            var query = _container.GetItemQueryIterator<Member>(queryDefinition);
            int count = 0;
            Console.WriteLine("test");
            while (query.HasMoreResults)
            {
                var member = await query.ReadNextAsync();
                count += member.Count;
            }
            return count > 0;
        }

        public async Task<bool> AddMemberAsync(Member member) {
            if (await CheckMemberExists(member.Id))
                return false;
            await _container.CreateItemAsync<Member>(member);
            return true;
        }
    }
}