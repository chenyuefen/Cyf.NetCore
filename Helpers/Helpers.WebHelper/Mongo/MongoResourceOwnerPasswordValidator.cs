using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class MongoResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IMongoDatabase _mongoDatabase;

        public MongoResourceOwnerPasswordValidator(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("resource_owner", context.UserName);
            var user = await _mongoDatabase.GetCollection<BsonDocument>("User").Find(filter).FirstOrDefaultAsync();
            if (user != null)
            {
                context.Result = new GrantValidationResult(
                 subject: context.UserName,
                 authenticationMethod: OidcConstants.AuthenticationMethods.Password,
                 claims: new Claim[] {
                     new Claim(JwtClaimTypes.Name,context.UserName),
                     new Claim(JwtClaimTypes.FamilyName,context.UserName.Split(':')[0]),
                     //new Claim(JwtClaimTypes.Id,context.UserName),
                 });
            }
            else
            {
#if DEBUG
                var update = Builders<BsonDocument>.Update.Set("resource_owner", context.UserName);
                await _mongoDatabase.GetCollection<BsonDocument>("user").UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });
#endif
                context.Result = new GrantValidationResult(
                    TokenRequestErrors.InvalidGrant,
                    "invalid custom credential"
                    );
            }
        }
    }
}
