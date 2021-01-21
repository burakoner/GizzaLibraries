using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace Gizza.Data.Net.Sms.Providers
{
    // iletimerkezi.com için tasarlandı
    internal class IletiMerkezi
    {
        // Adresler
        public string UrlSmsGet = "http://api.iletimerkezi.com/v1/send-sms/get/";
        public string UrlSmsPost = "http://api.iletimerkezi.com/v1/send-sms";
        public string UrlGetReport = "http://api.iletimerkezi.com/v1/get-report";
        public string UrlGetBalance = "http://api.iletimerkezi.com/v1/get-balance";
        public string UrlCancelOrder = "http://api.iletimerkezi.com/v1/cancel-order";

        // Ayarlar
        public string Username = "";
        public string Password = "";
        public string Originator = "";

        // Sunucu Cevabı
        public string ServerResponse = "";

        // Durum Kodları
        public int StatusCode = 0;
        public string StatusDesc = "";

        // Bakiye
        public int BalanceCount = 0;
        public double BalanceAmount = 0.0;

        // Order
        public int OrderId = 0;

        // Sms
        public string SmsBody = "";

        public IletiMerkezi(string user, string pass, string originator)
        {
            this.Username = user;
            this.Password = pass;
            this.Originator = originator;
        }

        public bool SendSms(string[] Recipents, string SmsText)
        {
            try
            {
                // Prepare
                this.SmsBody = SmsText;

                // Send Via Get
                // SendSmsViaGet(this.SmsBody, Recipents);

                // Send Via Post
                SendSmsViaPost(Recipents, this.SmsBody);

                // Return
                return this.StatusCode == 200;
            }
            catch
            {
                return false;
            }
        }

        private void SendSmsViaGet(string[] Recipents, string SmsText)
        {
            // Reset Status
            this.StatusCode = 0;
            this.SetStatus();

            // Basic GET Request
            this.DoRequest(UrlSmsGet, "GET", Uri.EscapeUriString("username=" + Username + "&password=" + Password + "&text=" + SmsText + "&receipents=" + String.Join(",", Recipents) + "&sender=" + this.Originator));

            // Console
            Console.WriteLine(this.ServerResponse);
        }

        private void SendSmsViaPost(string[] Recipents, string SmsText)
        {
            // Reset Status
            this.StatusCode = 0;
            this.SetStatus();

            // Prepare POST Request Xml
            string xmlRequest = "";
            xmlRequest += "<request>";
            xmlRequest += "  <authentication>";
            xmlRequest += "    <username>" + this.Username + "</username>";
            xmlRequest += "    <password>" + this.Password + "</password>";
            xmlRequest += "  </authentication>";
            xmlRequest += "  <order>";
            xmlRequest += "    <sender>" + this.Originator + "</sender>";
            // xmlRequest += "    <sendDateTime></sendDateTime>"; // GG/AA/YYYY SS:DD
            xmlRequest += "    <message>";
            xmlRequest += "      <text><![CDATA[" + SmsText + "]]></text>";
            xmlRequest += "      <receipents>";
            for (int i = 0; i < Recipents.Length; i++)
                xmlRequest += "        <number>" + Recipents[i] + "</number>";
            xmlRequest += "      </receipents>";
            xmlRequest += "    </message>";
            xmlRequest += "  </order>";
            xmlRequest += "</request>";

            // Do POST Request
            this.DoRequest(UrlSmsPost, "POST", xmlRequest);

            // Parse Xml
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(this.ServerResponse);
            XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/response/status");
            foreach (XmlNode node in nodeList)
            {
                this.StatusCode = int.Parse(node.SelectSingleNode("code").InnerText);
                this.StatusDesc = node.SelectSingleNode("message").InnerText;
            }

            if (this.StatusCode == 200)
            {
                nodeList = xmlDoc.DocumentElement.SelectNodes("/response/order");
                foreach (XmlNode node in nodeList)
                {
                    this.OrderId = int.Parse(node.SelectSingleNode("id").InnerText);
                }
            }

            // Console
            Console.WriteLine(this.ServerResponse);
        }

        public void CancelOrder(int orderId)
        {
            // Reset Status
            this.StatusCode = 0;
            this.SetStatus();

            // Prepare POST Request Xml
            string xmlRequest = "";
            xmlRequest += "<request>";
            xmlRequest += "  <authentication>";
            xmlRequest += "    <username>" + this.Username + "</username>";
            xmlRequest += "    <password>" + this.Password + "</password>";
            xmlRequest += "  </authentication>";
            xmlRequest += "</request>";

            // Do POST Request
            this.DoRequest(UrlCancelOrder, "POST", xmlRequest);

            // Parse Xml
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(this.ServerResponse);
            XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/response/status");
            foreach (XmlNode node in nodeList)
            {
                this.StatusCode = int.Parse(node.SelectSingleNode("code").InnerText);
                this.StatusDesc = node.SelectSingleNode("message").InnerText;
            }
        }

        public void GetBalance()
        {
            // Reset Status
            this.StatusCode = 0;
            this.SetStatus();

            // Prepare POST Request Xml
            string xmlRequest = "";
            xmlRequest += "<request>";
            xmlRequest += "  <authentication>";
            xmlRequest += "    <username>" + this.Username + "</username>";
            xmlRequest += "    <password>" + this.Password + "</password>";
            xmlRequest += "  </authentication>";
            xmlRequest += "</request>";

            // Do POST Request
            this.DoRequest(UrlGetBalance, "POST", xmlRequest);

            // Parse Xml
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(this.ServerResponse);
            XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/response/status");
            foreach (XmlNode node in nodeList)
            {
                this.StatusCode = int.Parse(node.SelectSingleNode("code").InnerText);
                this.StatusDesc = node.SelectSingleNode("message").InnerText;
            }

            if (this.StatusCode == 200)
            {
                nodeList = xmlDoc.DocumentElement.SelectNodes("/response/balance");
                foreach (XmlNode node in nodeList)
                {
                    this.BalanceCount = int.Parse(node.SelectSingleNode("sms").InnerText);
                    this.BalanceAmount = Double.Parse(node.SelectSingleNode("amount").InnerText);
                }
            }
        }

        public void GetReport(int orderId, int pageNumber = 1, int rowCount = 1000)
        {
            // Reset Status
            this.StatusCode = 0;
            this.SetStatus();

            // Prepare POST Request Xml
            string xmlRequest = "";
            xmlRequest += "<request>";
            xmlRequest += "  <authentication>";
            xmlRequest += "    <username>" + this.Username + "</username>";
            xmlRequest += "    <password>" + this.Password + "</password>";
            xmlRequest += "  </authentication>";
            xmlRequest += "  <order>";
            xmlRequest += "    <id>" + orderId + "</id>";
            xmlRequest += "    <page>" + pageNumber + "</page>";
            xmlRequest += "    <rowCount>" + rowCount + "</rowCount>";
            xmlRequest += "  </order>";
            xmlRequest += "</request>";

            // Do POST Request
            this.DoRequest(UrlGetReport, "POST", xmlRequest);
            Console.WriteLine(this.ServerResponse);
            // Parse Xml
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(this.ServerResponse);
            XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/response/status");
            foreach (XmlNode node in nodeList)
            {
                this.StatusCode = int.Parse(node.SelectSingleNode("code").InnerText);
                this.StatusDesc = node.SelectSingleNode("message").InnerText;
            }


            if (this.StatusCode == 200)
            {

            }

            // Console
            Console.WriteLine(this.ServerResponse);
        }

        private void SetStatus()
        {
            switch (this.StatusCode)
            {
                case 0:
                    this.StatusDesc = "";
                    break;
                case 2:
                    this.StatusDesc = "";
                    break;
                case 110:
                    this.StatusDesc = "Mesaj gönderiliyor";
                    break;
                case 111:
                    this.StatusDesc = "Mesaj gönderildi";
                    break;
                case 112:
                    this.StatusDesc = "Mesaj gönderilemedi";
                    break;
                case 113:
                    this.StatusDesc = "Siparişin gönderimi devam ediyor";
                    break;
                case 114:
                    this.StatusDesc = "Siparişin gönderimi tamamlandı";
                    break;
                case 115:
                    this.StatusDesc = "Sipariş gönderilemedi";
                    break;
                case 200:
                    this.StatusDesc = "İşlem başarılı";
                    break;
                case 400:
                    this.StatusDesc = "İstek çözümlenemedi";
                    break;
                case 401:
                    this.StatusDesc = "Üyelik bilgileri hatalı";
                    break;
                case 402:
                    this.StatusDesc = "Bakiye yetersiz";
                    break;
                case 404:
                    this.StatusDesc = "API istek yapılan yönteme sahip değil ";
                    break;
                case 450:
                    this.StatusDesc = "Gönderilen başlık kullanıma uygun değil";
                    break;
                case 451:
                    this.StatusDesc = "Tekrar eden sipariş";
                    break;
                case 452:
                    this.StatusDesc = "Mesaj alıcıları hatalı";
                    break;
                case 453:
                    this.StatusDesc = "Sipariş boyutu aşıldı";
                    break;
                case 454:
                    this.StatusDesc = "Mesaj metni boş";
                    break;
                case 455:
                    this.StatusDesc = "Sipariş bulunamadı";
                    break;
                case 456:
                    this.StatusDesc = "Sipariş gönderim tarihi henüz gelmedi";
                    break;
                case 457:
                    this.StatusDesc = "Mesaj gönderim tarihinin formatı hatalı";
                    break;
                case 503:
                    this.StatusDesc = "Sunucu geçici olarak servis dışı";
                    break;
                default:
                    this.StatusDesc = "";
                    break;
            }
        }

        private void DoRequest(string requestUrl, string requestMethod, string requestData)
        {
            if (requestMethod == "GET")
            {
                try
                {
                    // Create a web request for an invalid site. Substitute the "invalid site" strong in the Create call with a invalid name.
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl + "?" + requestData);

                    // Get the associated response for the above request.
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    // Check StatusCode
                    if (response.StatusCode.ToString() == "OK") StatusCode = 200;

                    // Close
                    response.Close();
                }
                catch (WebException e)
                {
                    StatusCode = Convert.ToInt32(e.Message.Between("(", ")"));
                }
                catch
                {
                    StatusCode = 0;
                }
                finally
                {
                    SetStatus();
                }
            }
            else if (requestMethod == "POST")
            {
                try
                {
                    // Create a request using a URL that can receive a post. 
                    WebRequest request = WebRequest.Create(requestUrl);

                    // Set the Method property of the request to POST.
                    request.Method = requestMethod;

                    // Create POST data and convert it to a byte array.
                    string postData = requestData;
                    byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                    // Set the ContentType property of the WebRequest.
                    request.ContentType = "application/x-www-form-urlencoded";

                    // Set the ContentLength property of the WebRequest.
                    request.ContentLength = byteArray.Length;

                    // Get the request stream.
                    Stream dataStream = request.GetRequestStream();

                    // Write the data to the request stream.
                    dataStream.Write(byteArray, 0, byteArray.Length);

                    // Close the Stream object.
                    dataStream.Close();

                    // Get the response.
                    WebResponse response = request.GetResponse();

                    // Display the status.
                    Console.WriteLine(((HttpWebResponse)response).StatusDescription);

                    // Get the stream containing content returned by the server.
                    dataStream = response.GetResponseStream();

                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);

                    // Read the content.
                    this.ServerResponse = reader.ReadToEnd();

                    // Clean up the streams.
                    reader.Close();
                    dataStream.Close();
                    response.Close();
                }
                catch (WebException e)
                {
                    StatusCode = Convert.ToInt32(e.Message.Between("(", ")"));
                }
                catch
                {
                    StatusCode = 0;
                }
                finally
                {
                    SetStatus();
                }
            }

        }

    }

    public static partial class Extensions
    {
        public static string Between(this string @this, string firstString, string lastString, bool includeFirst = false, bool includeLast = false)
        {
            int posA = @this.IndexOf(firstString) + firstString.Length;
            if (posA > @this.Length) return "";
            string temp = @this.Substring(posA);
            int posB = posA + temp.IndexOf(lastString);

            if (posA == -1) return "";
            if (posB == -1) return "";
            if (posA >= posB) return "";

            string FinalString = @this.Substring(posA, posB - posA);
            if (includeFirst) FinalString = firstString + FinalString;
            if (includeLast) FinalString += lastString;
            return FinalString;
        }
    }
}