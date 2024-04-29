global using DesignsAndBuild.Core.Entities;
global using DesignsAndBuild.Core.Mail.Contract;
global using DesignsAndBuild.Core.Entities.Identity;
global using DesignsAndBuild.Core.Repositories.Contract;
global using DesignsAndBuild.Core.Entities.Identity.Gmail;
global using DesignsAndBuild.Core.Entities.Identity.Enums;
global using DesignsAndBuild.Repository.Data.Configurations;
global using DesignsAndBuild.Core.Entities.Identity.Facebook;
global using DesignsAndBuild.Core.Specifications.Contact_Specs;
global using DesignsAndBuild.Core.Entities.ContactDomainEntities;
global using DesignsAndBuild.Core.Specifications.OurProject_Specs;
global using DesignsAndBuild.Core.Entities.OurProjectDomainEntity;
global using DesignsAndBuild.Core.Services.Contract.AccountModuleContracts;
global using DesignsAndBuild.Core.Services.Contract.ContactUsDomainContract;

global using Microsoft.Extensions.Options;
global using Microsoft.Extensions.Logging;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.Extensions.Configuration;

global using System.Text;
global using System.Security.Claims;
global using System.IdentityModel.Tokens.Jwt;

global using Serilog;
global using MimeKit;
global using Newtonsoft.Json;
global using MailKit.Net.Smtp;
global using MailKit.Security;
global using StackExchange.Redis;
global using static Google.Apis.Auth.GoogleJsonWebSignature;

