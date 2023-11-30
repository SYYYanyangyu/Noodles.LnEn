using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchService.Domain;


public interface ISearchRepository
{
    public Task UpsertAsync(Episode episode);
    public Task DeleteAsync(Guid episodeId);
    public Task<SearchEpisodesResponse> SearchEpisodes(string keyWord, int pageIndex, int PageSize);
}
