namespace AspNetCore_AAD_B2C.Controllers
{
    public class CustomRequest
    {
        public string token { get; set; }
    }

    public class CustomResponse
    {
        public bool valid { get; set; }
        public string encryptedToken { get; set; }
    }
}