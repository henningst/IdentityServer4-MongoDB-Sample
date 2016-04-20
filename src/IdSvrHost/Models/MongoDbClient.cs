using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using IdentityServer4.Core.Models;
using MongoDB.Bson;

namespace IdSvrHost.Models
{
    public class MongoDbClient
    {
        public ObjectId Id { get; set; }
        public string ClientId { get; set; }
        public List<string> RedirectUris { get; set; } 
        public List<string> ClientSecrets { get; set; }
        public Flows Flow { get; set; }
        public List<string> AllowedScopes { get; set; }
    }
}
