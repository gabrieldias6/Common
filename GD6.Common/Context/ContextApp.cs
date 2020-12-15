namespace GD6.Common
{
    public class ContextApp
    {
        public const string ClaimCliente = "ClienteId";

        public int? UserId { get; set; }
        public int? ClientId { get; set; }
        public bool IsAutenticated { get; set; }
    }
}
