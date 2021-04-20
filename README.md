# Trabajo de Fin de Grado
## Pablo Palomo López

---
---

<br>

### **Memoria**
> Directorio con [el proyecto de Overleaf](https://www.overleaf.com/project/60369aa56c87ca526baa9dd8) correspondiente a la memoria del trabajo

<br>

**Próximos pasos**

* ~~Comenzar a rellenar la memoria con el trabajo realizado una vez se tenga una versión más avanzada del OCDS_Mapper~~

* Documentar la aplicación mediante diagramas UML, etc.

* Una vez se tengan más reglas de mapeo revisadas, irlas introduciendo en la memoria

* Ver como atribuir la autoría de la plantilla (licencia Creative Commons CC BY 4.0)

* Ver cómo atribuir la fuente monoespaciada utilizada (licencia Open Font License 1.1)

**Enlaces de interés**

* [Template utilizado](https://www.overleaf.com/latex/templates/upm-thesis-template-latex/wrkfzfwvwctr)
* [Fuente utilizada](https://github.com/tonsky/FiraCode)

<br>

---

<br>

### **OCDS_Mapper**
> Aplicación C# de mapeo de los datos abiertos publicados en [la plataforma de contratación](https://www.hacienda.gob.es/es-ES/GobiernoAbierto/Datos%20Abiertos/Paginas/licitaciones_plataforma_contratacion.aspx) del sector público por el Ministerio de Hacienda al [Open Contracting Data Standard](https://standard.open-contracting.org/latest/)

<br>

<p float="center">
    <img src="OCDS_Mapper/badges/coverage.png" width=auto height="25"/>
    <img src="OCDS_Mapper/badges/loc.png" width=auto height="25"/>
    <img src="OCDS_Mapper/badges/maintainability.png" width=auto height="25"/>
    <img src="OCDS_Mapper/badges/qualitygate.png" width=auto height="25"/>
</p>

<br>

**Estructura**

    /badges     -> reportes de la cobertura de código
    /src        -> código fuente de la aplicación
        /Program.cs -> punto de entrada de la aplicación
        /Examples   -> ficheros auxiliares tanto de muestra como para testing
            /json/* -> directorio con muestras de mapeos realizados por la aplicación
            /xml/*  -> directorio con una muestra de fichero XML proporcionado por la plataforma de contratación, más un fichero para testing
        /Exceptions -> excepciones lanzadas por la aplicación
        /Interfaces -> interfaces que son implementadas por los módulos de la aplicación
        /Model      -> módulos que implementan las funcionalidades de la aplicación
    /test       -> pruebas unitarias y de integración de la aplicación

**Descripción**

La aplicación está compuesta por cinco diferentes módulos que constituyen un pipeline completo desde la extracción hasta el empaquetado de los datos mapeados. Se describen a continuación las principales funcionalides de los módulos:

* ***Provider***: módulo encargado de la provisión de los datos. Admite tres modos de operación: proveer el documento más reciente disponible, proveer un documento específico (ya sea local o en forma de URL), o proveer un flujo continuo de documentos desde el más reciente hasta no encontrar un fichero enlazado más o abortar la aplicación.

* ***Parser***: módulo encargado del parseo de los datos de los documentos de la Plataforma de Contratación Pública. Provee servicios para extraer los espacios de nombres, los elementos dadas sus rutas, etcétera. Ofrece asimismo un servicio para obtener los documentos enlazados a los siendo parseados.

* ***Mappings***: módulo estático que recoge las reglas de mapeo entre los dos esquemas.

* ***Mapper***: módulo encargado del mapeo de los datos al estándar OCDS. A través de los elementos XML provistos por el Parser y las reglas especificadas en Mappings, construye el fichero JSON correspondiente al mapeo de cada entrada XML.

* ***Packager***: módulo encargado del empaquetado de los datos ya mapeados. Actualmente sólo vuelca las entradas mapeadas en un fichero.

**Próximos pasos**

* ~~Analizar los elementos extraídos en las guías de CODICE para ir construyendo el modelo de mapeo de datos~~

* ~~Implementar un nuevo módulo capaz de recuperar dinámicamente los datos de la plataforma de contratación pública con el objetivo de:~~

    * ~~Tener operativa la descarga dinámica de los datos para cuando el módulo de parseo esté completado y poder realizar el mapeo automáticamente~~

    * ~~Poder realizar los análisis de datos con todos los disponibles y no sólo restringir el alcance a enero y febrero de 2021~~

* ~~Implementar las funcionalidades capaces de mapear los datos ya analizados, presentes en todos los ficheros parseados~~

* ~~Llegar al 80% de cobertura del código~~

* Ir mapeando las nuevas reglas revisadas

* Empaquetar los datos mapeados para poder introducirlos en la [Herramienta de Revisión de Datos de OCDS](https://standard.open-contracting.org/review/)

**Enlaces de interés**

***Plataforma de Contratación Pública***
* [Datos abiertos: Plataforma de Contratación Pública](https://www.hacienda.gob.es/es-ES/GobiernoAbierto/Datos%20Abiertos/Paginas/licitaciones_plataforma_contratacion.aspx)
* [Guía de implementación de documentos CODICE 2.0](https://contrataciondelestado.es/codice/2.0/doc/CODICE_2_GuiaImplementacion_v1.3.pdf)
* [Guía de la extensión de CODICE para la prestación de servicios en la Plataforma de Contratación del Sector Público](https://contrataciondelestado.es/codice/extension/doc/CODICE-PLACE-EXT_Guia_de_Implementacion.v.1.0.pdf)
* [Árbol de recursos del estándar CODICE](https://contrataciondelestado.es/codice/)

***Open Contracting Data Standard***
* [Open Contracting Data Standard](https://standard.open-contracting.org/latest/)
* [Guía básica para el mapeo a datos OCDS](https://docs.google.com/document/d/1VAKw8QCU08__qUnssmbSl_N38Rpd7nhcNChZ80qHOCI)

***Documentos de trabajo***
* [Enlace al documento de Google Docs con las tablas de mapeo CÓDICE->OCDS](https://docs.google.com/document/d/1OdDeeeZMnlCsp2YdNgM1hbv4rYlcY3YAuGfxqOZ-3lY/edit?usp=sharing)
* [Checklist con el progreso de los elementos de mapeo](https://docs.google.com/spreadsheets/d/14pikYzS-yzNWjtISyU-bPtoPe7cY1-KjOGGSC0lBhnI/edit?usp=sharing)

***Librerías utilizadas en el código***
* [Enlace a la librería de logging utilizada (licencia Apache)](https://logging.apache.org/log4net/)
* [Enlace a la librería de manejo de JSON utilizada (licencia MIT)](https://www.newtonsoft.com/json)
* [Enlace a la librería de las colecciones asíncronas (licencia MIT)](https://github.com/StephenCleary/AsyncEx)