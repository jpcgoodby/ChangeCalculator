using ConsoleTables;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;
using FluentValidation;
using ChangeCalculator.Service.Behaviours;
using ChangeCalculator.Service.Validators;
using ChangeCalculator.Domain.Commands;
using ChangeCalculator.Domain.Data;
using ChangeCalculator.Domain.Handlers;

public class Program
{
    private readonly ILogger<Program> _logger;
    private readonly IMediator _mediator;

    public Program(ILogger<Program> logger, IMediator mediator) => (_logger, _mediator) = (logger, mediator);

    public void Run(string[] args)
    {
        try
        {
            var result = _mediator.Send(new CalculateChangeCommandRequest(args)).Result;

            Display(result);

        }
        catch (Exception ex)
        {
            Display(new CalculateChangeCommandResponse { Error = ex.InnerException.Message });
        }

    }

    static void Main(string[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Please enter change calculator parameter values.");
            Console.Read();
        }

        var host = CreateHostBuilder(args).Build();
        host.Services.GetRequiredService<Program>().Run(args);
    }

    static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddMediatR(Assembly.GetAssembly(typeof(ChangeCalculatorHandler)));
                services.AddScoped(typeof(IPipelineBehavior<CalculateChangeCommandRequest, CalculateChangeCommandResponse>), typeof(RulesBehaviour<CalculateChangeCommandRequest, CalculateChangeCommandResponse>));
                services.AddScoped(typeof(IPipelineBehavior<CalculateChangeCommandRequest, CalculateChangeCommandResponse>), typeof(LoggingBehaviour<CalculateChangeCommandRequest, CalculateChangeCommandResponse>));
                services.AddScoped<IValidator<CalculateChangeCommandRequest>, CalculateChangeValidator>();
                services.AddScoped<SterlingReferenceData> ();
                services.AddScoped<DollarReferenceData>();
                services.AddScoped<Program>();
            });
    }

    static void Display(CalculateChangeCommandResponse changeResponse)
    {
        var tableHeader = new ConsoleTable("Change Calculator Success", "Change Amount", "Change Calculator Failure Reason");

        tableHeader.AddRow(changeResponse.Error == null ? "Successful" : "Failure", 
                            changeResponse.Error == null ? changeResponse.ChangeAmount : "N/A", 
                            changeResponse.Error != null ? changeResponse.Error : "N/A");

        tableHeader.Write();

        if (changeResponse.Error == null)
        {
            var tableFooter = new ConsoleTable("Change Denomination", "Denomination Quantity");

            foreach(var change in changeResponse.Change)
            {
                tableFooter.AddRow(change.Item1.UnitName, change.Item2);
            }

            tableFooter.Write();

        }

    }
}
