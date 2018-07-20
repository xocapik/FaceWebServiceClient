# FaceWebServiceClient
Notas para conectar con el Servicio Web de Face.gob.es usando WCF:

Se requiere un custom binding con:

    MessageSecurityVersion = WSSecurity10WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10
    AllowSerializedSigningTokenOnReply = true
    MessageProtectionOrder = MessageProtectionOrder.SignBeforeEncrypt
    SecurityHeaderLayout = SecurityHeaderLayout.LaxTimestampLast

El mensaje SOAP va sin encriptar, se requiere que el nivel de protección del contrato del servicio esté solo en firma:

    System.ServiceModel.ServiceContractAttribute( [...], ProtectionLevel = System.Net.Security.ProtectionLevel.Sign)


Documentación del servicio:https://administracionelectronica.gob.es/ctt/face/descargas

Donde darse de alta como proveedor:

Entorno de pruebas: https://se-face.redsara.es/es/proveedores

Producción: https://face.gob.es/es/proveedores


# FaceB2B

Notas para conectar con el Servicio Web de https://webservice.faceb2b.gob.es usando WCF:

Se requiere un custom binding igual al anterior pero a parte necesita definir un Custom Algorithm Suite que firme con rsa-sha256 y digest con sha1:
    
    sec.DefaultAlgorithmSuite = new FaceB2BCustomAlgorithmSuite();
    
El mensaje SOAP va sin encriptar, se requiere que el nivel de protección del contrato del servicio esté solo en firma:

    System.ServiceModel.ServiceContractAttribute( [...], ProtectionLevel = System.Net.Security.ProtectionLevel.Sign)
    
El mensaje tiene que ir sin VsDebuggerCausalityData, se puede quitar de varias maneras como por ejemplo crear un endpointBehaviour

    factory.Endpoint.Behaviors.Add(new SimpleEndpointBehavior());
