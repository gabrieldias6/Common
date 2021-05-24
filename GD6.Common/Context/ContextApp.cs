using System;

namespace GD6.Common
{
    public class ContextApp
    {
        public ContextApp()
        {
            Guid = Guid.NewGuid();
        }

        public Guid Guid;
        public const string ClaimCliente = "ClienteId";
        public const string ClaimClienteNome = "ClienteNome";
        public const string ClaimPlanoId = "PlanoId";

        public int? UserId { get; set; }
        public int? ClientId { get; set; }
        public bool IsAutenticated { get; set; }
    }
}
