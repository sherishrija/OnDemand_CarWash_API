﻿using Carwash.Models;
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
    public class OrderController : ControllerBase
    {
        private readonly IOrder _order;
        public OrderController(IOrder order)
        {
            _order = order;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _order.GetAllAsync();
            return Ok(orders);
        }
        [HttpGet]
        [Route("{id:int}")]
        [ActionName("GetOrderAsync")]
        public async Task<IActionResult> GetOrderAsync(int id)
        {
            var package = await _order.GetAsync(id);
            if (package == null)
            {
                return NotFound();
            }
            return Ok(package);
        }
        [HttpPost]
        public async Task<IActionResult> AddCarAsync(OrderModel addorder)
        {
            var order = new Models.OrderModel()
            {
                WashingInstructions = addorder.WashingInstructions,
                Date = addorder.Date,
                status = addorder.status,
                packageName = addorder.packageName,
                price = addorder.price,
                city = addorder.city,
                pincode = addorder.pincode,
            }; await _order.AddAsync(order);
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
                var order = await _order.DeleteAsync(id);
                if (order == null)
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
        public async Task<IActionResult> UpdateOrderAsync([FromRoute] int id, [FromBody] OrderModel updateorder)
        {
            try
            {
                var order = new Models.OrderModel()
                {
                    WashingInstructions = updateorder.WashingInstructions,
                    Date = updateorder.Date,
                    status = updateorder.status,
                    packageName = updateorder.packageName,
                    price = updateorder.price,
                    city = updateorder.city,
                    pincode = updateorder.pincode,
                };
                order = await _order.UpdateAsync(id, order);
                if (order == null)
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
    
