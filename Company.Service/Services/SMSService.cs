using Company.Service.Helper;
using Company.Service.Interfaces;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Company.Service.Services
{
    public class SMSService : ISMSService
    {
        private readonly TwilioSettings _options;

        public SMSService(IOptions<TwilioSettings> options)
        {
            _options = options.Value;
        }
        public MessageResource SendSMS(SMS sms)
        {
            TwilioClient.Init(_options.AccountSID, _options.AuthToken);
            var result = MessageResource.Create(
                    body: sms.Body,
                    to: sms.ToPhone,
                    from: new Twilio.Types.PhoneNumber(_options.FromPhoneNumber)
                );
            return result;
        }
    }
}
