using MCComputerAPI.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
  public class ConfigureJwtOptions : IConfigureNamedOptions<JwtBearerOptions>
    {
        private readonly JwtSettingsHelper _jwtHelper;

        public ConfigureJwtOptions(JwtSettingsHelper jwtHelper)
        {
            _jwtHelper = jwtHelper;
        }

        public void Configure(string? name, JwtBearerOptions options)
        {
            Configure(options);
        }

        public void Configure(JwtBearerOptions options)
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = _jwtHelper.ValidIssuer,
                ValidAudience = _jwtHelper.ValidAudience,
                IssuerSigningKey = _jwtHelper.IssuerSigningKey
            };
        }
    }