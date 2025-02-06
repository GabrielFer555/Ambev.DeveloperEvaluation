﻿global using Ambev.DeveloperEvaluation.Application.Products;
global using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
global using Ambev.DeveloperEvaluation.Domain.Entities;
global using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
global using Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;
global using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
global using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProducts;
global using Ambev.DeveloperEvaluation.Application.Products.GetProductById;
global using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
global using Ambev.DeveloperEvaluation.Application.Orders.UpdateOrder;
global using Ambev.DeveloperEvaluation.WebApi.Features.Orders.UpdateOrder;
global using Ambev.DeveloperEvaluation.Application.Users;
global using Ambev.DeveloperEvaluation.Domain.Enums;
global using Ambev.DeveloperEvaluation.Domain.Validation;
global using Ambev.DeveloperEvaluation.Application.Carts;
global using Ambev.DeveloperEvaluation.Application.Orders;
global using Ambev.DeveloperEvaluation.Domain.Aggregates;
global using Ambev.DeveloperEvaluation.Domain.Common;
global using Ambev.DeveloperEvaluation.Domain.Exceptions;
global using Microsoft.AspNetCore.Diagnostics;
global using Ambev.DeveloperEvaluation.Domain.ValueObjects;
global using Microsoft.AspNetCore.Authorization;
global using static Ambev.DeveloperEvaluation.Application.Users.NameValidator;
global using AutoMapper;
global using MediatR;
global using Microsoft.AspNetCore.Mvc;
global using FluentValidation;
global using Microsoft.EntityFrameworkCore;
global using Serilog;
