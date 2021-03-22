# Trabajo de Fin de Grado
## Pablo Palomo López

---
---

<br>

### **Memoria**
> Directorio con [el proyecto de Overleaf](https://www.overleaf.com/project/60369aa56c87ca526baa9dd8) correspondiente a la memoria del trabajo

<br>

**Próximos pasos**

* Comenzar a rellenar la memoria con el trabajo realizado una vez se tenga una versión más avanzada del OCDS_Parser

* Ver como atribuir la autoría de la plantilla (licencia Creative Commons CC BY 4.0)

**Enlaces de interés**

* [Template utilizado](https://www.overleaf.com/latex/templates/upm-thesis-template-latex/wrkfzfwvwctr)

<br>

---

<br>

### **OCDS_Parser**
> Módulo Python de parseo de los datos abiertos publicados en [la plataforma de contratación](https://www.hacienda.gob.es/es-ES/GobiernoAbierto/Datos%20Abiertos/Paginas/licitaciones_plataforma_contratacion.aspx) del sector público por el Ministerio de Hacienda al [Open Contracting Data Standard](https://standard.open-contracting.org/latest/)

<br>

**Estructura**

    /src        -> código fuente del módulo de parseo
    /resources  -> ficheros de recursos del módulo
        /csv/occurences.csv -> contiene el número de ocurrencias de cada elemento XML en cada entrada de los datos recogidos
        /json/elements.json -> lista con todos los elementos XML registrados
        /xml/               -> directorio con algunos de los ficheros XML proporcionados en la plataforma de contratación

**Descripción**

La funcionalidad del módulo, de momento, se limita al parseo de los datos comprimidos ofrecidos en la plataforma de contratación para los meses de [enero](https://contrataciondelestado.es/sindicacion/sindicacion_643/licitacionesPerfilesContratanteCompleto3_202101.zip) y [febrero](https://contrataciondelestado.es/sindicacion/sindicacion_643/licitacionesPerfilesContratanteCompleto3_202102.zip) de 2021. En este repositorio sólo se incluyen los primeros ficheros de cada mes ([..._01.atom](OCDS_Parser/resources/xml/licitacionesPerfilesContratanteCompleto3_01.atom) y [..._02.atom](OCDS_Parser/resources/xml/licitacionesPerfilesContratanteCompleto3_02.atom), respectivamente) por cuestiones de tamaño.

El parseo previamente descrito se ha basado en extraer de cada entrada (tag `<entry>` en los ficheros `.atom`) los elementos XML, llevarlos al fichero [elements.json](OCDS_Parser/resources/json/elements.json) y construir [un archivo CSV](OCDS_Parser/resources/csv/occurrences.csv) con la cuenta de las ocurrencias de cada elemento en cada entrada.

A través de dicho CSV, mediante el uso de <i>facets</i> en OpenRefine, se puede visualizar qué elementos aparecen en todas las entradas analizadas (**sólo válido para datos de enero y febrero de 2021**), qué elementos aparecen 0 o 1 veces, cuáles aparecen múltiples veces, etcétera.

Las notas manuscritas referidas a dicho análisis se encuentran en el fichero [notas.pdf](OCDS_Parser/notas.pdf).

**Próximos pasos**

* Analizar los elementos extraídos en las guías de CODICE para ir construyendo el modelo de mapeo de datos

* Implementar un nuevo módulo capaz de recuperar dinámicamente los datos de la plataforma de contratación pública con el objetivo de:

    * Tener operativa la descarga dinámica de los datos para cuando el módulo de parseo esté completado y poder realizar el mapeo automáticamente

    * Poder realizar los análisis de datos con todos los disponibles y no sólo restringir el alcance a enero y febrero de 2021

* Implementar las funcionalidades capaces de mapear los datos ya analizados, presentes en todos los ficheros parseados (principalmente, páginas 2 y 3 de [notas.pdf](OCDS_Parser/notas.pdf))

    * Comprobar que dichos elementos aparentemente presentes en <i>todos</i> los documentos aparecen efectivamente en la totalidad de ficheros

**Enlaces de interés**

* [Datos abiertos: Plataforma de Contratación Pública](https://www.hacienda.gob.es/es-ES/GobiernoAbierto/Datos%20Abiertos/Paginas/licitaciones_plataforma_contratacion.aspx)
* [Guía de implementación de documentos CODICE 2.0](https://contrataciondelestado.es/codice/2.0/doc/CODICE_2_GuiaImplementacion_v1.3.pdf)
* [Guía de la extensión de CODICE para la prestación de servicios en la Plataforma de Contratación del Sector Público](https://contrataciondelestado.es/codice/extension/doc/CODICE-PLACE-EXT_Guia_de_Implementacion.v.1.0.pdf)
* [Árbol de recursos del estándar CODICE](https://contrataciondelestado.es/codice/)
* [Open Contracting Data Standard](https://standard.open-contracting.org/latest/)
* [Guía básica para el mapeo a datos OCDS](https://docs.google.com/document/d/1VAKw8QCU08__qUnssmbSl_N38Rpd7nhcNChZ80qHOCI)
* [Librería utilizada para el parseo de XML, **licencia BSD**](https://lxml.de/)
