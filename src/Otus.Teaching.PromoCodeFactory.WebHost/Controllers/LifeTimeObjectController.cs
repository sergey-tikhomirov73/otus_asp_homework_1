using Microsoft.AspNetCore.Mvc;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Controllers
{
    
    
    /// <summary>
    /// Сотрудники
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    // [Route("api/v1/LifeTime")]  // непосредственное указание контроллера
    public class LifeTimeObjectController : ControllerBase
    {
        //ITestEntity _testEntity ;
        ITestEntityTransient _transEntity;
        ITestEntityScoped _scopedEntity;
        ITestEntitySingleton _singletonEntity;

        public LifeTimeObjectController(ITestEntityTransient transEntity, ITestEntityScoped scopedEntity, ITestEntitySingleton singletonEntity) 
        {
            _transEntity=transEntity;
            _scopedEntity=scopedEntity;
            _singletonEntity=singletonEntity;

        }   


        public IActionResult Index()
        {
            string message = $"Trans: {_transEntity.Code} Scoped: {_scopedEntity.Code}  Singleton: {_singletonEntity.Code}";
            string text=$"<!DOCTYPE HTML><html><head></head><body> <h1>TransCode:{message} </h1> <br><a href=\"https://localhost:5001/api/v1/LifeTimeObject\" > GO </a></body></html>";

            return Content(text,"text/html");
        }
    }
}
