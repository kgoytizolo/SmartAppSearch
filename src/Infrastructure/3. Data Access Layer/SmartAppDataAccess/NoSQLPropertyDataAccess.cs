using Nest;
using Elasticsearch.Net;
using SmartAppModels;
using DBModels = SmartAppDataAccess.Models;
using SmartAppDataAccess.Interfaces;
using System.Collections.Generic;
using System;

namespace SmartAppDataAccess
{
    public class NoSQLPropertyDataAccess : INoSQLPropertyDataAccess
    {
        private ElasticClient _elasticCnxNESTClient;

        public NoSQLPropertyDataAccess(ElasticClient elasticCnxNESTClient)
        {
            _elasticCnxNESTClient = elasticCnxNESTClient;
        }

        public ISearchResponse<DBModels.Properties> GetSerializableSearchResponseForProperty(string searchPhrase, int limit, string[] markets)
        {

            var marketsFilter = new List<Func<QueryContainerDescriptor<DBModels.Properties>, QueryContainer>>();

            marketsFilter.Add(fq => fq.Terms(t => t.Field( f => f.Property.Market).Terms(markets)));

            var searchResponse =  _elasticCnxNESTClient.Search<DBModels.Properties>(s => s
                    .Index("property")
                    .TypedKeys(null)
                    .From(0)
                    .Size(limit)
                    .Query(q => q
                        .Bool(qb => qb.Filter(marketsFilter) ) && q
                        .Bool(qb2 => qb2
                            .Must( mq => mq
                                .Match(m1 => m1
                                    .Field(f => f.Property.Name)
                                    .Query(searchPhrase)
                                ) || mq 
                                .Match(m2 => m2
                                    .Field(f2 => f2.Property.FormerName)
                                    .Query(searchPhrase)              
                                )
                            )                            
                        )
                    )
                );

            return searchResponse;
        }

        public PostData GetSerializablePostDataForProperties(string searchPhrase, int limit, string[] markets)
        {
            PostData postPropertiesIndex = PostData.Serializable(new
            {
                from = 0,
                size = limit,
                query = new {
                    match = new { 
                        property = new {
                            name = new{
                                query = searchPhrase
                            }
                        }
                    }
                }
            }
            );
            return postPropertiesIndex;
        }        

    }
}