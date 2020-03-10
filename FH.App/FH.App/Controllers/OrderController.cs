using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FH.BLL.Interfaces;
using FH.BLL.VMs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FH.App.Controllers
{
    [Route("api/order")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Get all orders by locationId (get it from current user)
        /// </summary>
        [HttpGet]
        [Route("location")] 
        public IActionResult GetAllOrdersByLocationAsync()
        {
            try
            {
                var meId = User.Claims.First(c => c.Type == "UserID").Value;
                var orderList = _orderService.GetAllLocationOrders(meId);
                if (orderList == null)
                {
                    throw new Exception("Orders not found by id.");
                }
                return Ok(orderList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get all active orders by locationId (get it from current user)
        /// </summary>
        [HttpGet]
        [Route("location/active")] 
        public IActionResult GetAllActiveOrdersByLocationAsync()
        {
            try
            {
                var meId = User.Claims.First(c => c.Type == "UserID").Value;
                var orderList = _orderService.GetAllActiveLocationOrders(meId);
                if (orderList == null)
                {
                    throw new Exception("Orders not found by id.");
                }
                return Ok(orderList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get all no-manager-orders by locationId (get it from current user)
        /// </summary>
        [HttpGet]
        [Route("location/managers/none")] 
        public IActionResult GetAllNoManagersOrdersByLocationAsync()
        {
            try
            {
                var meId = User.Claims.First(c => c.Type == "UserID").Value;
                var orderList = _orderService.GetAllNoManagersLocationOrders(meId);
                if (orderList == null)
                {
                    throw new Exception("Orders not found by id.");
                }
                return Ok(orderList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get order bu orderId
        /// </summary>
        [HttpGet]
        [Route("{id}")] 
        public async Task<IActionResult> GetOrderByIdAsync(int id)
        {
            try
            {
                OrderPageVM order;
                var meId = User.Claims.First(c => c.Type == "UserID").Value;
                if (id == 0)
                { 
                    order = _orderService.GetCurrentOrder(meId);
                    if (order == null)
                    {
                        throw new Exception("Orders not found");
                    }
                    return Ok(order);
                }
                order = await _orderService.GetOrderByIdAsync(id, meId);
                if (order == null)
                {
                    throw new Exception("Order not found by id.");
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get order history for current user (as client)
        /// </summary>
        [HttpGet]
        [Route("history")]
        [Authorize]
        public IActionResult GetOrderHistory()
        {
            try
            {
                var meId = User.Claims.First(c => c.Type == "UserID").Value;
                var orderList = _orderService.GetAllMyOrders(meId);
                if (orderList == null)
                {
                    throw new Exception("Orders not found by id.");
                }
                return Ok(orderList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        } 
    }
}