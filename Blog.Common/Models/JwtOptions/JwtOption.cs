namespace Blog.Common.Models.JwtOptions;

public class JwtOptions
{
    public  string Issuer { get; set; }
    public  string Audience { get; set; }
    public  string signinKey { get; set; }
}