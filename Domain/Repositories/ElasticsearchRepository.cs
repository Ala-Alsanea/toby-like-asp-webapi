using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Topy_like_asp_webapi.Domain.Entities.Base;
using Topy_like_asp_webapi.Domain.Repositories.Interfaces;

namespace Topy_like_asp_webapi.Domain.Repositories
{
    public class ElasticsearchRepository<T> : IElasticsearchRepository<T> where T : BaseEntity
    {


        public string IndexName { get; set; }

        public readonly ElasticsearchClient _client;
        private readonly ILogger<T> _logger;



        public ElasticsearchRepository(ElasticsearchClient client, ILogger<T> logger)
        {
            _client = client;
            _logger = logger;
            IndexName = typeof(T).Name.ToLower() + "s";
        }



        public async Task<IEnumerable<T>> SearchAsync(string query)
        {
            try
            {
                SearchResponse<T> SearchAsync = await _client.SearchAsync<T>(s => s
                    .Index(IndexName) // Specify the index
                    ); // Match all documents

                return SearchAsync.Documents;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all documents."); // Log error with exception
                throw;
            }
        }

        public async Task<bool> CreateBulkAsync(List<T> documents)
        {
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
                _logger.LogError(ex, "An error occurred while creating bulk documents."); // Log error with exception
                throw;
            }
        }

        public async Task<bool> CreateOrUpdateAsync(T document)
        {
            try
            {

                IndexResponse indexResponse = await _client.IndexAsync(document, idx => idx.Index(IndexName));

                if (!indexResponse.IsValidResponse)
                {
                    _logger.LogError(indexResponse.DebugInformation); // Log error
                    throw new Exception(indexResponse.DebugInformation);
                }

                return indexResponse.IsSuccess();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while Create Or Update documents."); // Log error with exception
                throw;
            }
        }

        public async Task<bool> DeleteAsync(T document)
        {
            try
            {
                DeleteResponse deleteResponse = await _client.DeleteAsync<T>(document, idx => idx.Index(IndexName));

                if (!deleteResponse.IsValidResponse)
                {
                    _logger.LogError(deleteResponse.DebugInformation); // Log error
                    throw new Exception(deleteResponse.DebugInformation);
                }

                return deleteResponse.IsSuccess();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting documents."); // Log error with exception
                throw;
            }
        }
    }
}