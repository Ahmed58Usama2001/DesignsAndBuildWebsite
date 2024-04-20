﻿global using DesignsAndBuild.Repository;
global using DesignsAndBuild.APIs.Errors;
global using DesignsAndBuild.APIs.Helpers;
global using DesignsAndBuild.Core.Entities;

global using DesignsAndBuild.APIs.Extensions;
global using DesignsAndBuild.APIs.MiddleWares;
global using DesignsAndBuild.Repository.Identity;
global using DesignsAndBuild.APIs.Dtos.AccountDtos;
global using DesignsAndBuild.Core.Entities.Identity;
global using DesignsAndBuild.Service.AuthModuleService;
global using DesignsAndBuild.Core.Repositories.Contract;
global using DesignsAndBuild.Core.Entities.Identity.Gmail;
global using DesignsAndBuild.Repository.Data.Configurations;
global using DesignsAndBuild.Core.Entities.Identity.Facebook;
global using DesignsAndBuild.Core.Services.Contract.AccountModuleContracts;

global using System.Net;
global using System.Text;
global using System.Net.Mail;
global using System.Text.Json;
global using System.Security.Claims;
global using System.ComponentModel.DataAnnotations;

global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Authentication.JwtBearer;

global using Serilog;
global using AutoMapper;
global using Newtonsoft.Json;
global using StackExchange.Redis;
global using JsonSerializer = System.Text.Json.JsonSerializer;



global using DesignsAndBuild.APIs.Dtos.MaillingDtos;
global using DesignsAndBuild.Core.Entities.MailSettings;

global using DesignsAndBuild.Core.Mail.Contract;
global using DesignsAndBuild.Core.Specifications;
global using DesignsAndBuild.APIs.Dtos.ProjectDtos;
global using DesignsAndBuild.Core.Services.Contract.OurProjectPage.Services;
 