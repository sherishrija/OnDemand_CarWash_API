using Carwash.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace Carwash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailProcessController : ControllerBase
    {
        [HttpPost("EmailSendings")]
        public IActionResult EmailSending(List<OrderModel> data_table)
        {
            double totalamount = 0;
            string textBody = "<p> Hello User, </p> <p>Thank you for ordering from Ondemand Car Wash system.</p> <p>Once the order is approved by admin, we will process it</p>";
            textBody += " <table border=" + 1 + " cellpadding=" + 0 + " cellspacing=" + 0 + " width = " + 400 + "><tr bgcolor='#4da6ff'><td><b>washingInstructions</b></td> <td> <b> Date</b> </td> <td> <b>Status</b> </td> <td> <b>packageName</b> </td><td> <b>price</b> </td><td> <b>city</b> </td><td> <b>pincode</b> </td></tr>";
            for (int loopCount = 0; loopCount < data_table.Count; loopCount++)
            {
                textBody += "<tr><td>" + data_table[loopCount].WashingInstructions + "</td><td> " + data_table[loopCount].Date + "</td><td> " + data_table[loopCount].status + "</td><td> " + data_table[loopCount].packageName + "</td><td>" + Convert.ToInt32(data_table[loopCount].price) + "</td> <td>" + data_table[loopCount].city + "</td><td>" + data_table[loopCount].pincode + "</td> <td>";
                totalamount += data_table[loopCount].price;
            }
            textBody += "</table> <br>";
            textBody += "<strong>Order Date :</strong>";
            textBody += data_table[0].Date.ToShortDateString();
            textBody += "<br><strong>Total Order Amount :</strong>";
            textBody += totalamount;
            textBody += "<br>";
            textBody += "<br><i>If you have any questions, contact us here on <b>vajjaanuja12@gmail.com</b>! " +
            "We are here to help you! </i>";
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Admin", "shrijasheri9@gmail.com"));
            message.To.Add(new MailboxAddress("Customer", "shrijasheri@gmail.com"));
            message.Subject = "Order Placed - Ondemand Car Wash System";
            message.Body = new TextPart("html")
            {
                Text = textBody
            };
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("shrijasheri9@gmail.com", "etxacbpukilchevf");
                client.Send(message);
                client.Disconnect(true);
            }
            return Ok("Email sent Successfully");
        }
        [HttpPost("AdminEmail/OrderConfirmation")]
        public IActionResult AdminEmailSending(OrderModel data_table)
        {
            double totalamount = 0;
            string textBody = "<p> Hello User, </p> <p>Thank you for ordering from Ondemand Car Wash system.</p> <p>We’re happy to let you know that we’ve received your order.</p> <p>Your order was approved by admin, and you will receive your order shortly.</p>";
            textBody += " <table border=" + 1 + " cellpadding=" + 1 + " cellspacing=" + 1 + " width = " + 600 + "><tr><td><b>Drug Name</b></td> <td> <b> Drug Quantity</b> </td> <td> <b> Unit Price</b> </td> <td> <b>Total Amount</b> </td></tr>";
            textBody += "<tr><td>" + data_table.WashingInstructions + "</td><td> " + data_table.Date + "</td><td> " + data_table.status + "</td><td> " + data_table.packageName + "</td><td>" + Convert.ToInt32(data_table.price) + "</td> <td>" + data_table.city + data_table.pincode + "</td> <td>";
            totalamount += data_table.price;
            textBody += "</table> <br>";
            textBody += "<strong>Order Date :</strong>";
            textBody += data_table.Date.ToShortDateString();
            textBody += "<br><strong>Total Order Amount :</strong>";
            textBody += totalamount;
            textBody += "<br><i>If you have any questions, contact us here on <b>vajjaanuja12@gmail.com</b>! ";
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Admin", "shrijasheri@gmail.com"));
            message.To.Add(new MailboxAddress("doctor", "shrijasheri@gmail.com"));
            message.Subject = "Order Placed - Ondemand Car Wash System";
            message.Body = new TextPart("html")
            {
                Text = textBody
            };
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("shrijasheri9@gmail.com", "etxacbpukilchevf");
                client.Send(message);
                client.Disconnect(true);
            }
            return Ok("Email sent Successfully");
        }         //Supplier Email Sending
        //[HttpPost("SupplierMail/StockRelated")]
        //public IActionResult EmailSendingToSupplier(string email)
        //{
        //    string textBody = "<p> Hello Supplier, We got a order greater than your supplies. please update your drugs </p>";
        //    var message = new MimeMessage();
        //    message.From.Add(new MailboxAddress("Pharmacy", "vajjarajaiah@gmail.com"));
        //    message.To.Add(new MailboxAddress("doctor", email));
        //    message.Subject = "Order Confirmed - Pharmacy Management System";
        //    message.Body = new TextPart("html")
        //    {
        //        Text = textBody
        //    };
        //    using (var client = new SmtpClient())
        //    {
        //        client.Connect("smtp.gmail.com", 587, false);
        //        client.Authenticate("vajjarajaiah@gmail.com", "usyfubldxvakaxvc");
        //        client.Send(message);
        //        client.Disconnect(true);
        //    }
        //    return Ok("Email sent Successfully");
        //}
    }
}
    
