using Microsoft.AspNetCore.Mvc;

namespace MicroserviceBase.Infrastructure.CrossCutting.HealthChecks
{
    public interface IWarmUpController
    {
        IActionResult WarmUp();
    }
}