global using DesignsAndBuild.Core.Entities.Identity;
global using DesignsAndBuild.Core.Entities.Identity.Enums;
global using DesignsAndBuild.Core.Entities.Identity.Gmail;
global using DesignsAndBuild.Repository.Data.Configurations;
global using DesignsAndBuild.Core.Entities.Identity.Facebook;
global using DesignsAndBuild.Core.Services.Contract.AccountModuleContracts;

global using Microsoft.Extensions.Options;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.Extensions.Configuration;

global using System.Text;
global using System.Security.Claims;
global using System.IdentityModel.Tokens.Jwt;

global using Serilog;
global using Newtonsoft.Json;
global using static Google.Apis.Auth.GoogleJsonWebSignature;