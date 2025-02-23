﻿global using MediatR;
global using AutoMapper;
global using Ambev.DeveloperEvaluation.Domain.Enums;
global using Ambev.DeveloperEvaluation.Domain.Entities;
global using Ambev.DeveloperEvaluation.Domain.Repositories;
global using Ambev.DeveloperEvaluation.Domain.ValueObjects;
global using Ambev.DeveloperEvaluation.Domain.Exceptions;
global using Ambev.DeveloperEvaluation.Common.Security;
global using Ambev.DeveloperEvaluation.Domain.Aggregates;
global using Ambev.DeveloperEvaluation.Domain.Validation;
global using static Ambev.DeveloperEvaluation.Application.Users.NameValidator;
global using FluentValidation;
global using Ambev.DeveloperEvaluation.Domain.Events;
global using Microsoft.Extensions.Logging;
global using Ambev.DeveloperEvaluation.Common.Validation;
