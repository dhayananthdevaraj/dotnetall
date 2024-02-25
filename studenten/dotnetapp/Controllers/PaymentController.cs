using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnetapp.Controllers
{
    [Route("api/payment")]
    [ApiController]
    // [Authorize(Roles = "Admin")] // Adjust authorization based on your requirements
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;

        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetAllPayments()
        {
            try
            {
                var payments = await _paymentService.GetAllPayments();
                return Ok(payments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("{paymentId}")]
        public async Task<ActionResult<Payment>> GetPaymentById(int paymentId)
        {
            try
            {
                var payment = await _paymentService.GetPaymentById(paymentId);

                if (payment == null)
                {
                    return NotFound(new { message = "Cannot find the payment" });
                }

                return Ok(payment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddPayment([FromBody] Payment newPayment)
        {
            try
            {
                var addedPayment = await _paymentService.AddPayment(newPayment);
                return CreatedAtAction(nameof(GetPaymentById), new { paymentId = addedPayment.PaymentID }, addedPayment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
