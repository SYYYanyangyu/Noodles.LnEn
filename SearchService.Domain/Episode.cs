namespace SearchService.Domain;

public record Episode(Guid Id, string CnName, string EngName, string PlainSubtitle, Guid AlbumId);

public record Episode_test(Guid id, string cnName, string engName, string plainSubtitle, Guid albumId);
