namespace GD6.Common
{
    public class EntityDtoSelect : IEntityDtoSelect
    {
        public string Id { get; set; }
        public virtual string Nome { get; set; }
        public bool Excluido { get; set; }
    }
}