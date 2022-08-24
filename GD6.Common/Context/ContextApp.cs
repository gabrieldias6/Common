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
        public const string ClaimUsuarioPermissoes = "UsuarioPermissoes";

        public int? UserId { get; private set; }
        public int? ClientId { get; private set; }
        public bool IsAutenticated { get; set; }

        public void SetCliente(int clientId)
        {
            ClientId = clientId;
        }

        public void SetUserId(int? userId)
        {
            UserId = userId;
            if (UserId.HasValue)
                IsAutenticated = true;
        }
    }
}
