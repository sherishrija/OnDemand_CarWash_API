using Carwash.Models;
using Carwash.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carwash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {

        private readonly IPackage _package;
        public PackageController(IPackage package)
        {
            _package = package;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var packages = await _package.GetAllAsync();
            return Ok(packages);
        }
        [HttpGet]
        [Route("{id:int}")]
        [ActionName("GetCarAsync")]
        public async Task<IActionResult> GetCarAsync(int id)
        {
            var package = await _package.GetAsync(id);
            if (package == null)
            {
                return NotFound();
            }
            return Ok(package);
        }
        [HttpPost]

        public async Task<IActionResult> AddCarAsync(CarPackageModel addpackage)
        {
            var package = new Models.CarPackageModel()
            {
                Name = addpackage.Name,
                Price = addpackage.Price,
                Status = addpackage.Status
            }; await _package.AddAsync(package);
            return Ok();
        }
        #region 
        //delete method  
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var package = await _package.DeleteAsync(id);
                if (package == null)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
            }
            return Ok();
        }
        #endregion
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateCarAsync([FromRoute] int id,
            [FromBody] CarPackageModel updatepackage)
        {
            try
            {
                var package = new Models.CarPackageModel()
                {
                    Name = updatepackage.Name,
                    Price = updatepackage.Price,
                    Status = updatepackage.Status
                };
                package = await _package.UpdateAsync(id, package);
                if (package == null)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            { }
            return Ok();
        }


    }
}
