using Gizza.Data.Net.Sms.Providers;

namespace Gizza.Data.Net.Sms
{
    // iletimerkezi.com için tasarlandı
    public class SmsSender
    {
        // Enumerations
        public enum SmsProvider { IletiMerkezi }

        // Public Properties
        public SmsProvider Provider { get; }
        internal IletiMerkezi IletiMerkezi { get; set; }

        // Ayarlar
        public string Username
        {
            get
            {
                if (this.Provider == SmsProvider.IletiMerkezi)
                {
                    return this.IletiMerkezi.Username;
                }

                return string.Empty;
            }
            set
            {
                if (this.Provider == SmsProvider.IletiMerkezi)
                {
                    this.IletiMerkezi.Username = value;
                }
            }
        }
        public string Password
        {
            get
            {
                if (this.Provider == SmsProvider.IletiMerkezi)
                {
                    return this.IletiMerkezi.Password;
                }

                return string.Empty;
            }
            set
            {
                if (this.Provider == SmsProvider.IletiMerkezi)
                {
                    this.IletiMerkezi.Password = value;
                }
            }
        }
        public string Originator
        {
            get
            {
                if (this.Provider == SmsProvider.IletiMerkezi)
                {
                    return this.IletiMerkezi.Originator;
                }

                return string.Empty;
            }
            set
            {
                if (this.Provider == SmsProvider.IletiMerkezi)
                {
                    this.IletiMerkezi.Originator = value;
                }
            }
        }

        // Sunucu Cevabı
        public string ServerResponse
        {
            get
            {
                if (this.Provider == SmsProvider.IletiMerkezi)
                {
                    return this.IletiMerkezi.ServerResponse;
                }

                return string.Empty;
            }
        }

        // Durum Kodları
        public int StatusCode
        {
            get
            {
                if (this.Provider == SmsProvider.IletiMerkezi)
                {
                    return this.IletiMerkezi.StatusCode;
                }

                return 0;
            }
        }
        public string StatusDesc
        {
            get
            {
                if (this.Provider == SmsProvider.IletiMerkezi)
                {
                    return this.IletiMerkezi.StatusDesc;
                }

                return string.Empty;
            }
        }

        // Bakiye
        public int BalanceCount
        {
            get
            {
                if (this.Provider == SmsProvider.IletiMerkezi)
                {
                    return this.IletiMerkezi.BalanceCount;
                }

                return 0;
            }
        }
        public double BalanceAmount
        {
            get
            {
                if (this.Provider == SmsProvider.IletiMerkezi)
                {
                    return this.IletiMerkezi.BalanceAmount;
                }

                return 0;
            }
        }

        // Order
        public int OrderId
        {
            get
            {
                if (this.Provider == SmsProvider.IletiMerkezi)
                {
                    return this.IletiMerkezi.OrderId;
                }

                return 0;
            }
        }

        // Sms
        public string SmsBody
        {
            get
            {
                if (this.Provider == SmsProvider.IletiMerkezi)
                {
                    return this.IletiMerkezi.SmsBody;
                }

                return string.Empty;
            }
            set
            {
                if (this.Provider == SmsProvider.IletiMerkezi)
                {
                    this.IletiMerkezi.SmsBody = value;
                }
            }
        }

        public SmsSender() : this(SmsProvider.IletiMerkezi)
        {
        }

        public SmsSender(SmsProvider provider)
        {
            this.Provider = provider;
            this.IletiMerkezi = new IletiMerkezi(this.Username, this.Password, this.Originator);
        }

        public bool SendSms(string[] Recipents, string SmsText)
        {
            if (this.Provider == SmsProvider.IletiMerkezi)
            {
                return this.IletiMerkezi.SendSms(Recipents, SmsText);
            }

            return false;
        }

        public void CancelOrder(int orderId)
        {
            if (this.Provider == SmsProvider.IletiMerkezi)
            {
                this.IletiMerkezi.CancelOrder(orderId);
            }
        }

        public void GetBalance()
        {
            if (this.Provider == SmsProvider.IletiMerkezi)
            {
                this.IletiMerkezi.GetBalance();
            }
        }

        public void GetReport(int orderId, int pageNumber = 1, int rowCount = 1000)
        {
            if (this.Provider == SmsProvider.IletiMerkezi)
            {
                this.IletiMerkezi.GetReport(orderId, pageNumber, rowCount);
            }
        }


    }
}