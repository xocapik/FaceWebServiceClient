# FaceWebServiceClient
Notas para conectar con el Servicio Web de Face.gob.es usando WCF:

Se requiere un custom binding con:

    MessageSecurityVersion = WSSecurity10WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10
    AllowSerializedSigningTokenOnReply = true
    MessageProtectionOrder = MessageProtectionOrder.SignBeforeEncrypt
    SecurityHeaderLayout = SecurityHeaderLayout.LaxTimestampLast

El mensaje SOAP va sin encriptar, se requiere que el contrato del servicio este solo en firma:

    System.ServiceModel.ServiceContractAttribute( [...], ProtectionLevel = System.Net.Security.ProtectionLevel.Sign)


Documentación del servicio:
https://administracionelectronica.gob.es/ctt/face/descargas

Donde darse de alta como proveedor:
https://face.gob.es/es/proveedores


