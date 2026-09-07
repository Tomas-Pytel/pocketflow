namespace backend.Dtos
{
    public record SavingDto(int Id, string Name, decimal Amount, decimal? Limit);

    public record CreateSavingDto(string Name, decimal? Amount, decimal? Limit);
    public record UpdateSavingDto(string Name, decimal Amount, decimal? Limit);
}
