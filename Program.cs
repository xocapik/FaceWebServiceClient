using System;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.Text;

namespace servicioFaceWCF
{
    class Program
    {
        static void Main(string[] args)
        {
            EndpointAddress address;
            X509Certificate2 serverCertificate;
            bool stagingMode = true;

            if (stagingMode) //staging mode
            {
                address = new EndpointAddress(
                    new Uri("https://se-face-webservice.redsara.es/facturasspp2"),
                    EndpointIdentity.CreateDnsIdentity("DTIC AGE PRUEBAS"));
                serverCertificate = new X509Certificate2(@"C:\temp\DTICAGEPRUEBAS.cer");
            }
            else //production mode
            {
                address = new EndpointAddress(
                new Uri("https://webservice.face.gob.es/facturasspp2"),
                EndpointIdentity.CreateDnsIdentity("DTIC AGE"));
                serverCertificate = new X509Certificate2(@"C:\temp\DTICAGE.cer");
            }
            
            //Our certificate, must be
            var clientCertificate = new X509Certificate2(@"C:\temp\myawensomecertificate.pfx", "myawensomepass");

            //Custombinding for interop with FACE java web service
            CustomBinding customBinding = new CustomBinding();

            //security
            var sec = SecurityBindingElement.CreateMutualCertificateDuplexBindingElement(
                MessageSecurityVersion.WSSecurity10WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10);
            sec.AllowSerializedSigningTokenOnReply = true;
            sec.MessageProtectionOrder = MessageProtectionOrder.SignBeforeEncrypt;
            sec.SecurityHeaderLayout = SecurityHeaderLayout.LaxTimestampLast;
            sec.IncludeTimestamp = false;

            //message
            var textBindingElement = new TextMessageEncodingBindingElement(MessageVersion.Soap11, Encoding.UTF8);

            //transport
            var httpsTransport = new HttpsTransportBindingElement();
            httpsTransport.RequireClientCertificate = true;

            //Bind in order (Security layer, message layer, transport layer)
            customBinding.Elements.Add(sec);
            customBinding.Elements.Add(textBindingElement);
            customBinding.Elements.Add(httpsTransport);

            ChannelFactory<facturasspp2.FacturaSSPPWebServiceProxyPort> factory = new ChannelFactory<facturasspp2.FacturaSSPPWebServiceProxyPort>(customBinding, address);

            factory.Credentials.ClientCertificate.Certificate = clientCertificate;
            factory.Credentials.ServiceCertificate.DefaultCertificate = serverCertificate;  
            factory.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;

            var service = factory.CreateChannel();

            try
            {
                var c = service.consultarEstados();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
