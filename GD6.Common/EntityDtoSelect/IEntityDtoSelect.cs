namespace GD6.Common
{
    public interface IEntityDtoSelect
    {
        string Id { get; set; }
        string Nome { get; set; }
        bool Excluido { get; set; }
    }
}