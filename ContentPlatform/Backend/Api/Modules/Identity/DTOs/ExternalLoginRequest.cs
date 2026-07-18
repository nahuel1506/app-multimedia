namespace Api.Modules.Identity.DTOs
{
    public struct ExternalLoginRequest
    {
        public string Token { get; set; }
        public string Provider { get; set; }
    }
}