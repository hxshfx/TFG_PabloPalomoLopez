using System.Collections.Generic;
using System.Xml.Linq;

namespace OCDS_Mapper.src.Model
{
    /* Clase estática con la información para realizar los mapeos */
    public static class Mappings
    {

        /* Clase estática con los descriptores de mapeo */
        public static class MappingElement
        {
            /* Elementos raíz */
            public const string Tag = "tag";
            public const string OCID = "ocid";

            /* Elementos en 'planning' */
            public const string Planning = "planning";
            public static class Plannings
            {
                /* Elementos en 'planning'.'budget' */
                public const string Budget = "budget";
                public static class Budgets
                {
                    public const string Amount = "amount";
                }
            }

            /* Elementos en 'tender' */
            public const string Tender = "tender";
            public static class Tenders
            {
                public const string MainProcurementCategory = "mainProcurementCategory";
                public const string ProcurementMethod = "procurementMethod";
                public const string ProcurementMethodDetails = "procurementMethodDetails_es";
                public const string SubmissionMethod = "submissionMethod";
                public const string SubmissionMethodDetails = "submissionMethodDetails";
                public const string Title = "title";
                public const string Value = "value";

                /* Elementos en 'tender'.'tenderPeriod' */
                public const string TenderPeriod = "tenderPeriod";
                public static class TenderPeriods
                {
                    public const string StartDate = "startDate";
                    public const string EndDate = "endDate";
                    public const string DurationInDays = "durationInDays";
                }
            }
        }


        /*  función estática GetMappings(IDictionary<string, XNamespace>) => IDictionary<IEnumerable<XName>, IEnumerable<string>>
         *      Obtiene el diccionario con la información necesaria para realizar los mapeos.
         *  @param namespaces : Diccionario con los espacios de nombres
         *  @return : Diccionario con los pares de rutas en los elementos en CODICE y OCDS
         *      @ej : {
         *              {
         *                  [                                                       => cac-place-ext:ContractFolderStatus/ (raíz)
         *                      XName(XNamespace("cac") + "ProcurementProject"),    => cac:ProcurementProject/
         *                      XName(XNamespace("cbc") + "Name")                   => cbc:Name
         *                  ],
         *                  [
         *                      MappingElement.Tender,                              => tender/
         *                      MappingElement.Tenders.Title                        => title
         *                  ]
         *              }, ...
         *            }
         */
        public static IDictionary<IEnumerable<XName>, IEnumerable<string>> GetMappings(IDictionary<string, XNamespace> namespaces)
        {
            IDictionary<IEnumerable<XName>, IEnumerable<string>> mappings = new Dictionary<IEnumerable<XName>, IEnumerable<string>>()
            {
                {
                    new LinkedList<XName>(new XName[]
                    {
                        namespaces["cbc-place-ext"] + "ContractFolderStatusCode"
                    }),
                    new LinkedList<string>(new string[]
                    {
                        MappingElement.Tag
                    })
                },
                {
                    new LinkedList<XName>(new XName[]
                    {
                        namespaces["cbc"] + "ContractFolderID"
                    }),
                    new LinkedList<string>(new string[]
                    {
                        MappingElement.OCID
                    })
                },
                {
                    new LinkedList<XName>(new XName[]
                    {
                        namespaces["cac"] + "ProcurementProject",
                        namespaces["cbc"] + "Name"
                    }),
                    new LinkedList<string>(new string[]
                    {
                        MappingElement.Tender,
                        MappingElement.Tenders.Title
                    })
                },
                {
                    new LinkedList<XName>(new XName[]{
                        namespaces["cac"] + "ProcurementProject",
                        namespaces["cac"] + "BudgetAmount",
                        namespaces["cbc"] + "EstimatedOverallContractAmount"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElement.Tender,
                        MappingElement.Tenders.Value
                    })
                },
                {
                    new LinkedList<XName>(new XName[]{
                        namespaces["cac"] + "ProcurementProject",
                        namespaces["cac"] + "BudgetAmount",
                        namespaces["cbc"] + "TotalAmount"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElement.Planning,
                        MappingElement.Plannings.Budget,
                        MappingElement.Plannings.Budgets.Amount
                    })
                },
                {
                    new LinkedList<XName>(new XName[]{
                        namespaces["cac"] + "ProcurementProject",
                        namespaces["cac"] + "PlannedPeriod",
                        namespaces["cbc"] + "StartDate"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElement.Tender,
                        MappingElement.Tenders.TenderPeriod,
                        MappingElement.Tenders.TenderPeriods.StartDate
                    })
                },
                {
                    new LinkedList<XName>(new XName[]{
                        namespaces["cac"] + "ProcurementProject",
                        namespaces["cac"] + "PlannedPeriod",
                        namespaces["cbc"] + "EndDate"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElement.Tender,
                        MappingElement.Tenders.TenderPeriod,
                        MappingElement.Tenders.TenderPeriods.EndDate
                    })
                },
                {
                    new LinkedList<XName>(new XName[]{
                        namespaces["cac"] + "ProcurementProject",
                        namespaces["cac"] + "PlannedPeriod",
                        namespaces["cbc"] + "DurationMeasure"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElement.Tender,
                        MappingElement.Tenders.TenderPeriod,
                        MappingElement.Tenders.TenderPeriods.DurationInDays
                    })
                },
                {
                    new LinkedList<XName>(new XName[]{
                        namespaces["cac"] + "ProcurementProject",
                        namespaces["cbc"] + "TypeCode"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElement.Tender,
                        MappingElement.Tenders.MainProcurementCategory
                    })
                },
                {
                    new LinkedList<XName>(new XName[]{
                        namespaces["cac"] + "TenderingProcess",
                        namespaces["cbc"] + "ProcedureCode"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElement.Tender,
                        MappingElement.Tenders.ProcurementMethod
                    })
                },
                {
                    new LinkedList<XName>(new XName[]{
                        namespaces["cac"] + "TenderingProcess",
                        namespaces["cbc"] + "ContractingSystemCode"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElement.Tender,
                        MappingElement.Tenders.ProcurementMethodDetails
                    })
                },
                {
                    new LinkedList<XName>(new XName[]{
                        namespaces["cac"] + "TenderingProcess",
                        namespaces["cbc"] + "SubmissionMethodCode"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElement.Tender,
                        MappingElement.Tenders.SubmissionMethod
                    })
                },
                {
                    new LinkedList<XName>(new XName[]{
                        namespaces["cac"] + "TenderingTerms",
                        namespaces["cac"] + "Language",
                        namespaces["cbc"] + "ID"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElement.Tender,
                        MappingElement.Tenders.SubmissionMethodDetails
                    })
                },
                {
                    new LinkedList<XName>(new XName[]{
                        namespaces["cac"] + "TenderingProcess",
                        namespaces["cac"] + "AuctionTerms",
                        namespaces["cbc"] + "AuctionConstraintIndicator"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElement.Tender,
                        MappingElement.Tenders.SubmissionMethod
                    })
                }
            };

            // Devuelve el diccionario de mapeo
            return mappings;
        }
    }
}
