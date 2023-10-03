using AuthService.Model;
using Microsoft.Azure.Cosmos;

namespace AuthService
{
    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;

        public CosmosDbService(
            CosmosClient cosmosDbClient,
            string databaseName,
            string containerName)
        {
            _container = cosmosDbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddAsync(Candidate Candidate)
        {
            await _container.CreateItemAsync(Candidate, new PartitionKey(Candidate.CandidateId));
        }

        public async Task DeleteAsync(string id)
        {
            await _container.DeleteItemAsync<Candidate>(id, new PartitionKey(id));
        }

        public async Task<Candidate> GetAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<Candidate>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException) //For handling Candidate not found and other exceptions
            {
                return null;
            }
        }

        public async Task<IEnumerable<Candidate>> GetMultipleAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<Candidate>(new QueryDefinition(queryString));

            var results = new List<Candidate>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateAsync(string id, Candidate Candidate)
        {
            await _container.UpsertItemAsync(Candidate, new PartitionKey(id));
        }
    }
}
