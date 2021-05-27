namespace OCDS_Mapper.src.Utils
{
    /* Enumerado descriptor del estado de terminación proceso */
    public enum EStatusCodes
    {
        OK              = 0,
        DISPLAY_HELP    = 1,
        INVALID_DIR     = 2,
        WRONG_CODE      = 3,
        PATH_UNPROVIDED = 4,
        FAILED          = 5
    }


    /* Enumerado descriptor de los modos de operación de provisión */

    public enum EProviderOperationCode
    {
        PROVIDE_ALL,        // Provee todos los documentos accesibles
        PROVIDE_DAILY,      // Provee los documentos publicados en el día
        PROVIDE_LATEST,     // Provee sólo el último documento disponible
        PROVIDE_SPECIFIC    // Provee un documento pasado como parámetro
    }


    /* Enumerado descriptor de los niveles de severidad para el logging */

    public enum ELogLevel
    {
        DEBUG,      // Nivel más bajo
        INFO,
        WARN,
        ERROR,
        FATAL       // Nivel más alto
    }
}
