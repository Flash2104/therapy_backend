using BusinessLogic.Config;
using System;
using System.Configuration;

namespace Utils.SberbankAcquiring.Models.Request
{
    public class RegisterDORequest
    {
        public string UserName { get; set; } = "denischernakov-api";
        public string Password { get; set; } = "denischernakov";
        public string OrderNumber { get; set; }
        public long Amount { get; set; }
        public long Currency { get; set; } = 643;
        public string ReturnUrl { get; set; }
        public string FailUrl { get; set; }

        public RegisterDORequest(string apiUrl, string orderNumber, long amount)
        {
            OrderNumber = orderNumber;
            Amount = amount;
            ReturnUrl = $"{apiUrl}/payments/success";
            FailUrl = $"{apiUrl}/payments/fail";
        }

        public RegisterDORequest(string apiUrl, string orderNumber, long amount, long sessionID)
        {
            OrderNumber = orderNumber;
            Amount = amount;
            ReturnUrl = $"{apiUrl}/payments/success?sessionId={sessionID}";
            FailUrl = $"{apiUrl}/payments/fail?sessionId={sessionID}";
        }

        public void SetSessionID(string apiUrl, long sessionID)
        {
            ReturnUrl = $"{apiUrl}/payments/success?sessionId={sessionID}";
            FailUrl = $"{apiUrl}/payments/fail?sessionId={sessionID}";
        }
    }
}
