using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchService.Domain;
public record SearchEpisodesResponse(IEnumerable<Episode> Episodes, long TotalCount);

public record SearchEpisodesResponse_test(IEnumerable<Episode_test> Episodes, long TotalCount);

