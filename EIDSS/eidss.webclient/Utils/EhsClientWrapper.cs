using bv.common.Configuration;
using eidss.webclient.EhsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Xml;

namespace eidss.webclient.Utils
{
    public class EhsClientWrapper : IDisposable
    {
        private readonly EhsFacadeClient m_Client;

        public EhsClientWrapper()
        {
            var address = new EndpointAddress(Config.GetSetting("EhsServiceHostURL", "http://localhost:50120/"));
            m_Client = bv.common.Core.Utils.IsCalledFromUnitTest()
                           ? new EhsFacadeClient(CreateTestBinding(), address)
                           : new EhsFacadeClient("BasicHttpBinding_IEhsFacade", address);


        }

        public EhsFacadeClient Client
        {
            get { return m_Client; }
        }

        public void Dispose()
        {
            m_Client.Close();
        }

        private BasicHttpBinding CreateTestBinding()
        {
            var binding = new BasicHttpBinding
            {
                Name = "BasicHttpBinding_IEhsFacade",
                CloseTimeout = new TimeSpan(0, 3, 0),
                OpenTimeout = new TimeSpan(0, 3, 0),
                ReceiveTimeout = new TimeSpan(0, 10, 0),
                SendTimeout = new TimeSpan(0, 3, 0),
                MaxBufferSize = 2147483647,
                MaxBufferPoolSize = 2147483647,
                MaxReceivedMessageSize = 2147483647,
                TextEncoding = Encoding.UTF8,
                TransferMode = TransferMode.Buffered,
                UseDefaultWebProxy = true,
                ReaderQuotas = new XmlDictionaryReaderQuotas
                {
                    MaxDepth = 32,
                    MaxStringContentLength = 2147483647,
                    MaxArrayLength = 2147483647,
                    MaxBytesPerRead = 2147483647,
                    MaxNameTableCharCount = 2147483647,
                },
            };
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
            return binding;
        }
    }
}