using MCComputerAPI.Models.DTOs;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MCComputerAPI.Helpers;

    public class JwtSettingsHelper
    {
        public string ValidIssuer { get; }
        public string ValidAudience { get; }
        public SymmetricSecurityKey IssuerSigningKey { get; }

        public JwtSettingsHelper(JwtSettingsDTOs settings)
        {
            ValidIssuer = settings.ValidIssuer;
            ValidAudience = settings.ValidAudience;
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecretKey));
        }
    }