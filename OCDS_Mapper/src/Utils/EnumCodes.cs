namespace OCDS_Mapper.src.Utils
{
    /* Enumerado descriptor del estado del proceso */
    public enum EStatusCodes : int
    {
        OK              = 0,
        DISPLAY_HELP    = 1,
        WRONG_ARGUMENTS = 2,
        PATH_UNPROVIDED = 3
    }


    /* Enumerado descriptor de los modos de operación de provisión */

    public enum EProviderOperationCode
    {
        PROVIDE_ALL,        // Provee todos los documentos accesibles
        PROVIDE_LATEST,     // Provee sólo el último documento disponible
        PROVIDE_SPECIFIC    // Provee un documento pasado como parámetro
    }


    /* Enumerado descriptor de los modos de operación de empaquetado */

    public enum EPackagerOperationCode
    {
        PACKAGE_REMOTE, // Empaqueta los datos de manera remota
        PACKAGE_LOCAL,  // Empaqueta los datos de manera local
    }
}
