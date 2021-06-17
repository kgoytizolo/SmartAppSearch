using Nest;
using SmartAppModels;
using DBModels = SmartAppDataAccess.Models;
using SmartAppDataAccess.Interfaces;
using System.Collections.Generic;
using System;
using System.Linq;

namespace SmartAppDataAccess
{
    public class NoSQLManagementDataAccess : INoSQLManagementDataAccess
    {
        private ElasticClient _elasticCnxNESTClient;

        public NoSQLManagementDataAccess(ElasticClient elasticCnxNESTClient)
        {
            _elasticCnxNESTClient = elasticCnxNESTClient;
        }

        public ISearchResponse<DBModels.Managements> GetSerializableSearchResponseForManagement(string searchPhrase, int limit, string[] markets)
        {
            var marketsFilter = new List<Func<QueryContainerDescriptor<DBModels.Managements>, QueryContainer>>();

            if(markets.Any())
                marketsFilter.Add(fq => fq.Terms(t => t.Field( f => f.Mgmt.Market).Terms(markets) ));

            var searchResponse = _elasticCnxNESTClient.Search<DBModels.Managements>(s => s
                    .Index("management")
                    .TypedKeys(null)
                    .From(0)
                    .Size(limit)
                    .Query(q => q
                        .Bool(qb => qb.Filter(marketsFilter) ) && q
                        .Bool(qb2 => qb2
                            .Must( mq => mq
                                .Match(m1 => m1
                                    .Field(f => f.Mgmt.Name)
                                    .Query(searchPhrase)
                                )            
                            )
                        )
                    )
                );
            return searchResponse;
        }

    }
}