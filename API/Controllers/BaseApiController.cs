using System;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
// api/{the name of class before "Controller so its 'baseapi'}
[Route("api/[controller]")]
//controllerbase because its mvc without view so it returns json instead of html

public class BaseApiController : ControllerBase
{

}
