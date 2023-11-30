using Nest;
using SearchService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Noodles.Common;

namespace SearchService.Infrastructure
{
    public class SearchRepository : ISearchRepository
    {
        private readonly IElasticClient elasticClient;
        
        public SearchRepository(IElasticClient elasticClient)
        {
            this.elasticClient = elasticClient;
        }

        public Task DeleteAsync(Guid episodeId)
        {
            try
            {
                elasticClient.DeleteByQuery<Episode>(q => q
               .Index("episodes")
               .Query(rq => rq.Term(f => f.Id, "elasticsearch.pm")));
                //因为有可能文档不存在，所以不检查结果
                //如果Episode被删除，则把对应的数据也从Elastic Search中删除
                return elasticClient.DeleteAsync(new DeleteRequest("episodes", episodeId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<SearchEpisodesResponse> SearchEpisodes(string keyword , int pageIndex , int pageSize)
        {
            try
            {
                int from = pageSize * (pageIndex - 1);
                string kw = keyword;
                Func<QueryContainerDescriptor<Episode>, QueryContainer> query = (q) =>
                                    q.Match(mq => mq.Field(f => f.CnName).Query(kw)) ||
                                    q.Match(mq => mq.Field(f => f.EngName).Query(kw)) ||
                                    q.Match(mq => mq.Field(f => f.PlainSubtitle).Query(kw));
                Func<HighlightDescriptor<Episode> , IHighlight> highlightSelector = h =>
                                    h.Fields(fs => fs.Field(f => f.PlainSubtitle));

                var result = await this.elasticClient.SearchAsync<Episode>(s => s.Index("episodes").From(from)
                .Size(pageSize).Query(query).Highlight(highlightSelector));

                if (!result.IsValid)
                {
                    throw result.OriginalException;
                }
                List<Episode> episodes = new List<Episode>();
                foreach (var hit in result.Hits)
                {
                    string highlightedSubtitle;
                    //如果没有预览内容，则显示前50个字
                    if (hit.Highlight.ContainsKey("plainSubtitle"))
                    {
                        highlightedSubtitle = string.Join("\r\n", hit.Highlight["plainSubtitle"]);
                    }
                    else
                    {
                        highlightedSubtitle = hit.Source.PlainSubtitle.Cut(50);
                    }
                    var episode = hit.Source with { PlainSubtitle = highlightedSubtitle };
                    episodes.Add(episode);
                }

                return new SearchEpisodesResponse(episodes, result.Total);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpsertAsync(Episode episode)
        {
            var response = await elasticClient.IndexAsync(episode, idx => idx.Index("episodes").Id(episode.Id));//Upsert:Update or Insert
            if (!response.IsValid)
            {
                throw new ApplicationException(response.DebugInformation);
            }
        }
    }
}
