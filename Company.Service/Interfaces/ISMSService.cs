using Company.Service.Helper;
using Twilio.Rest.Api.V2010.Account;

namespace Company.Service.Interfaces
{
    public interface ISMSService
    {
        MessageResource SendSMS(SMS sms);
    }
}
