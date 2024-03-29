﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace GD6.Common
{
    // Login
    public class ChatProApiLoginRequest
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }

    public class ChatProApiLoginDto
    {
        public bool Success { get; set; }

        public ChatProApiUserDto User { get; set; }

        public string Message { get; set; }
        public string Token { get; set; }
    }

    // Instancias
    public class ChatProApiClienteDto : ChatProApiDto
    {
        public ChatProApiUserDto User { get; set; }
        public List<ChatProApiInstanciaDto> Instances { get; set; }
    }

    public class ChatProApiUserDto
    {
        public string Email { get; set; }
    }

    public class ChatProApiInstanciaDto
    {
        public bool Active { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Token { get; set; }
        public string Webhook { get; set; }
        public string Endpoint { get; set; }
    }

    // Nova Instancia
    public class ChatProApiCriarInstanciaeDto : ChatProApiDto
    {
        public ChatProApiUserDto User { get; set; }
        public ChatProApiInstanciaDto Instance { get; set; }
    }

    // Alterar Nome
    public class ChatProApiRequest
    {
        public string Code { get; set; }
        public string New_name { get; set; }
        public int Value { get; set; }
    }

    public class ChatProApiResponseNew
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
    }

    public class ChatProApiResponse
    {
        public string Content { get; set; }
        public HttpStatusCode ResponseStatusCode { get; set; }
        public bool SuccessRequest { get; set; }
        public string MessageErro { get; set; }
        public string Message { get; set; }
    }

    public class ChatProApiDto : ChatProApiResponse
    {
        public bool Success { get; set; }
    }


    // Retorno de Instancia
    public class ChatProApiResponseInstanciaStatus : ChatProApiResponse
    {
        public bool Connected { get; set; }
        public bool Power_save { get; set; }
    }

    public class ChatProApiResponseInstanciaReload : ChatProApiResponse
    {
        public string Command { get; set; }
        public string Status { get; set; }
    }

    public class ChatProApiResponseInstanciaGenerateQrCode : ChatProApiResponse
    {
        public string Error { get; set; }
        public string Qr { get; set; }
        public bool Status { get; set; }
        public string Ttl { get; set; }
    }

    // Mensagem
    public class ChatProApiMessageRequest
    {
        public ChatProApiMessageRequest()
        {

        }

        public ChatProApiMessageRequest(string number, string message)
        {
            Number = number;
            Message = message;
        }

        public string Message { get; set; }
        public string Number { get; set; }
    }
    public class ChatProApiMessageDto : ChatProApiResponse
    {
        // Erro
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("timestamp")]
        public int Timestamp { get; set; }


        public bool Status { get; set; }

        [JsonProperty("resposeMessage")]
        public ChatProApiMessageResposeMessageDto ResposeMessage { get; set; }

        // Em caso de Erro
        [JsonProperty("erro")]
        public bool Erro { get; set; }
    }

    public class ChatProApiMessageResposeMessageDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
    }

    public class ChatProApiMessageResposeMessageDtoErro
    {
        [JsonProperty("erro")]
        public bool Erro { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }

    // Mensagem Uri
    public class ChatProApiMessageFileRequest
    {
        public ChatProApiMessageFileRequest(string number, string caption, string url)
        {
            Number = number;
            Caption = caption;
            Url = url;
        }

        public string Caption { get; set; }
        public string Number { get; set; }
        public string Url { get; set; }
    }
    public class ChatProApiMessageFileResponse : ChatProApiResponse
    {
        [JsonProperty("requestMenssage")]
        public ChatProApiMessageFileResponseRequestMessage RequestMenssage { get; set; }

        [JsonProperty("resposeMessage")]
        public ChatProApiMessageFileResponseResposeMessage ResposeMessage { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; }

        //// Erro
        //[JsonProperty("id")]
        //public string Id { get; set; }

        //[JsonProperty("timestamp")]
        //public int Timestamp { get; set; }


        //public ChatProApiMessageResposeMessageDto ResposeMessage { get; set; }
    }

    public class ChatProApiMessageFileResponseRequestMessage
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("number")]
        public string Number { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("caption")]
        public string Caption { get; set; }
    }

    public class ChatProApiMessageFileResponseResposeMessage
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("queeId")]
        public string QueeId { get; set; }
    }
}
