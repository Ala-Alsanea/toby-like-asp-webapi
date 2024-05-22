using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch;
using Topy_like_asp_webapi.Domain.Entities.Base;
using Topy_like_asp_webapi.Domain.Repositories.Interfaces;

namespace Topy_like_asp_webapi.Domain.Repositories
{
    public class ElasticsearchRepository<T> : IElasticsearchRepository<T> where T : BaseEntity
    {


        private string IndexName { get; set; }
        private readonly ElasticsearchClient _client;
        private readonly ILogger<T> _logger;



        public ElasticsearchRepository(ElasticsearchClient client, ILogger<T> logger)
        {
            _client = client;
            _logger = logger;
            IndexName = typeof(T).Name.ToLower() + "s";
        }

        public async Task<T> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> SearchAsync(string query)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateAsync(List<T> documents)
        {



            // var documentsWithId = documents.Select((doc, index) => new { Id = index.ToString(), Document = doc });

            // BulkResponse bulk = await _client.BulkAsync(b => b
            //                         .Index(IndexName)
            //                         .IndexMany(documentsWithId, (descriptor, doc) => descriptor
            //                             .Id(doc.Id)
            //                             .Document(doc.Document))
            //                     );
            try
            {
                BulkResponse bulk = await _client.BulkAsync(b => b
                            .Index(IndexName)
                            .IndexMany(documents)
                        );

                if (!bulk.IsValidResponse)
                {
                    _logger.LogError(bulk.DebugInformation); // Log error
                    throw new Exception(bulk.DebugInformation);
                }

                _logger.LogInformation("bulk: " + bulk.Items.Count()); // Log information

                return bulk.IsSuccess();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating documents."); // Log error with exception
                throw;
            }
        }

        public async Task<bool> UpdateAsync(T document)
        {
            IndexResponse indexResponse = await _client.IndexAsync(document, idx => idx.Index(IndexName));

            if (!indexResponse.IsValidResponse)
            {
                throw new Exception(indexResponse.DebugInformation);
            }

            return indexResponse.IsSuccess();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}