using Microsoft.AspNetCore.Mvc;
using PromptTesting.Application.Prompts.Queries;
using PromptTesting.Application.Prompts.Commands;
using PromptTesting.Application.Scope.Commands;
using MediatR;
using PromptTesting.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using PromptTesting.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetPromptsQuery>());
builder.Services.AddDbContext<AppDbContext>(o =>
	o.UseSqlServer(builder.Configuration.GetConnectionString("Default") ?? "Server=(local);Database=PromptTesting;Trusted_Connection=True;TrustServerCertificate=True"));

// T049 OIDC auth placeholder
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.Authority = builder.Configuration["Auth:Authority"] ?? "https://login.example.com";
		options.Audience = builder.Configuration["Auth:Audience"] ?? "prompt-testing-api";
		options.RequireHttpsMetadata = false;
	});
builder.Services.AddAuthorization();

Log.Logger = new LoggerConfiguration().WriteTo.Console().Enrich.FromLogContext().CreateLogger();
builder.Host.UseSerilog();

builder.Services.AddSingleton<CorrelationIdMiddleware>();

var app = builder.Build();
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ProblemDetailsMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

// Mediator wired endpoints (simplified placeholder logic still)
app.MapGet("/prompts", async ([FromQuery] string team,[FromQuery] string brokingSegment,[FromQuery] string globalLineOfBusiness,[FromQuery] string product, IMediator mediator) =>
{
	var result = await mediator.Send(new GetPromptsQuery(team, brokingSegment, globalLineOfBusiness, product));
	return Results.Ok(result);
});
app.MapGet("/prompts/{id:guid}/context", async (Guid id, IMediator mediator) =>
{
	var ctx = await mediator.Send(new GetPromptContextQuery(id));
	return ctx is null ? Results.NotFound() : Results.Ok(new { promptId = id, content = ctx });
});
app.MapPost("/prompts/{id:guid}/test", async (Guid id, [FromBody] ExecutePromptTestBody body, IMediator mediator, HttpContext http) =>
{
	var user = http.User?.Identity?.Name ?? "test-user"; // placeholder
	var testId = await mediator.Send(new ExecutePromptTestCommand(id, body.Context, body.Scope.Team, body.Scope.BrokingSegment, body.Scope.GlobalLineOfBusiness, body.Scope.Product, user));
	return Results.Accepted($"/tests/{testId}/results", new { testId, status = "Running" });
});
app.MapGet("/tests/{id:guid}/results", (Guid id) => Results.Ok(new { accuracy = 0, completedAt = DateTime.UtcNow, status = "Running", failureReason = (string?)null }));
app.MapPost("/scope/validate", async ([FromBody] ScopeBody scope, IMediator mediator) =>
{
	var valid = await mediator.Send(new ValidateScopeCommand(scope.Team, scope.BrokingSegment, scope.GlobalLineOfBusiness, scope.Product));
	return valid ? Results.Ok() : Results.UnprocessableEntity();
});

// Seed data (dev only)
using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
	await db.Database.EnsureCreatedAsync();
	await db.SeedAsync();
}

app.Run();

record ExecutePromptTestBody(string Context, ScopeBody Scope);
record ScopeBody(string Team,string BrokingSegment,string GlobalLineOfBusiness,string Product);

class CorrelationIdMiddleware
{
	private readonly RequestDelegate _next;
	private const string HeaderName = "X-Correlation-ID";
	public CorrelationIdMiddleware(RequestDelegate next) => _next = next;
	public async Task Invoke(HttpContext context)
	{
		if (!context.Request.Headers.TryGetValue(HeaderName, out var cid))
		{
			cid = Guid.NewGuid().ToString();
			context.Response.Headers[HeaderName] = cid;
		}
		using (Serilog.Context.LogContext.PushProperty("CorrelationId", cid.ToString()))
		{
			await _next(context);
		}
	}
}
