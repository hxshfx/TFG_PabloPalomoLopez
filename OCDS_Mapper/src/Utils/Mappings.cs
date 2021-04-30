using System.Collections.Generic;
using System.Xml.Linq;

namespace OCDS_Mapper.src.Model
{
    /* Clase estática con la información para realizar los mapeos */
    public static class Mappings
    {
        /* Clase estática con los descriptores de mapeo */
        public static class MappingElements
        {
            /* Elementos raíz */
            public const string Tag = "tag";
            public const string OCID = "ocid";

            /* Elementos en 'contracts' */
            public const string Contract = "contracts";
            public static class Contracts
            {
                public const string Id = "id";
                public const string Period = "period";

                /* Elementos en 'contract'.'period' */
                public static class Periods
                {
                    public const string StartDate = "startDate";
                }
            }

            /* Elementos en 'awards' */
            public const string Award = "awards";
            public static class Awards
            {
                public const string Date = "date";
                public const string Description = "description_es";
                public const string Id = "id";
                public const string Status = "status";
                public const string Suppliers = "suppliers";
                public const string Value = "value";
            }

            /* Elementos en 'parties' */
            public const string Party = "parties";
            public static class Parties
            {
                public const string CountryName = "countryName";
                public const string Identifier = "identifier";
                public const string Name = "name";

                /* Elementos en 'parties'.'address' */
                public const string Address = "address";
                public static class Addresses
                {
                    public const string Locality = "locality";
                    public const string PostalCode = "postalCode";
                    public const string StreetAddress = "streetAddress";
                }

                /* Elementos en 'parties'.'contactPoint' */
                public const string ContactPoint = "contactPoint";
                public static class ContactPoints
                {
                    public const string Email = "email";
                    public const string FaxNumber = "faxNumber";
                    public const string Name_ = "name";
                    public const string Telephone = "telephone";
                    public const string Url = "url";
                }
            }

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
                public const string NumberOfTenderers = "numberOfTenderers";
                public const string ProcurementMethod = "procurementMethod";
                public const string ProcurementMethodDetails = "procurementMethodDetails_es";
                public const string SubmissionMethod = "submissionMethod";
                public const string SubmissionMethodDetails = "submissionMethodDetails";
                public const string Title = "title";
                public const string Value = "value";

                /* Elementos en 'tender'.'items' */
                public const string Item = "items";
                public static class Items
                {
                    public const string Classification = "classification";
                }

                /* Elementos en 'tender'.'lots' */
                public const string Lot = "lots";
                public static class Lots
                {
                    public const string Id = "id";
                    public const string Name = "name";
                    public const string Value_ = "value";
                }

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


        /*  función estática GetMappingRules(IDictionary<string, XNamespace>) => IDictionary<IEnumerable<XName>, IEnumerable<string>>
         *      Obtiene el diccionario con la información necesaria para realizar los mapeos.
         *  @param namespaces : Diccionario con los espacios de nombres
         *  @return : Diccionario con los pares de rutas en los elementos en CODICE y OCDS
         *  @ej :
         *      {
         *          {
         *              [                                                       => cac-place-ext:ContractFolderStatus/ (raíz)
         *                  XName(XNamespace("cac") + "ProcurementProject"),    => cac:ProcurementProject/
         *                  XName(XNamespace("cbc") + "Name")                   => cbc:Name
         *              ],
         *              [
         *                  MappingElements.Tender,                              => tender/
         *                  MappingElements.Tenders.Title                        => title
         *              ]
         *          }, ...
         *      }
         */
        public static IDictionary<IEnumerable<XName>, IEnumerable<string>> GetMappingRules(IDictionary<string, XNamespace> namespaces)
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
                        MappingElements.Tag
                    })
                },
                {
                    new LinkedList<XName>(new XName[]
                    {
                        namespaces["cbc"] + "ContractFolderID"
                    }),
                    new LinkedList<string>(new string[]
                    {
                        MappingElements.OCID
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
                        MappingElements.Tender,
                        MappingElements.Tenders.Title
                    })
                },
                {
                    new LinkedList<XName>(new XName[]{
                        namespaces["cac"] + "ProcurementProject",
                        namespaces["cac"] + "BudgetAmount",
                        namespaces["cbc"] + "EstimatedOverallContractAmount"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElements.Tender,
                        MappingElements.Tenders.Value
                    })
                },
                {
                    new LinkedList<XName>(new XName[]{
                        namespaces["cac"] + "ProcurementProject",
                        namespaces["cac"] + "BudgetAmount",
                        namespaces["cbc"] + "TotalAmount"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElements.Planning,
                        MappingElements.Plannings.Budget,
                        MappingElements.Plannings.Budgets.Amount
                    })
                },
                {
                    new LinkedList<XName>(new XName[]{
                        namespaces["cac"] + "ProcurementProject",
                        namespaces["cac"] + "PlannedPeriod",
                        namespaces["cbc"] + "StartDate"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElements.Tender,
                        MappingElements.Tenders.TenderPeriod,
                        MappingElements.Tenders.TenderPeriods.StartDate
                    })
                },
                {
                    new LinkedList<XName>(new XName[]{
                        namespaces["cac"] + "ProcurementProject",
                        namespaces["cac"] + "PlannedPeriod",
                        namespaces["cbc"] + "EndDate"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElements.Tender,
                        MappingElements.Tenders.TenderPeriod,
                        MappingElements.Tenders.TenderPeriods.EndDate
                    })
                },
                {
                    new LinkedList<XName>(new XName[]{
                        namespaces["cac"] + "ProcurementProject",
                        namespaces["cac"] + "PlannedPeriod",
                        namespaces["cbc"] + "DurationMeasure"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElements.Tender,
                        MappingElements.Tenders.TenderPeriod,
                        MappingElements.Tenders.TenderPeriods.DurationInDays
                    })
                },
                {
                    new LinkedList<XName>(new XName[]{
                        namespaces["cac"] + "ProcurementProject",
                        namespaces["cbc"] + "TypeCode"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElements.Tender,
                        MappingElements.Tenders.MainProcurementCategory
                    })
                },
                {
                    new LinkedList<XName>(new XName[]{
                        namespaces["cac"] + "TenderingProcess",
                        namespaces["cbc"] + "ProcedureCode"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElements.Tender,
                        MappingElements.Tenders.ProcurementMethod
                    })
                },
                {
                    new LinkedList<XName>(new XName[]{
                        namespaces["cac"] + "TenderingProcess",
                        namespaces["cbc"] + "ContractingSystemCode"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElements.Tender,
                        MappingElements.Tenders.ProcurementMethodDetails
                    })
                },
                {
                    new LinkedList<XName>(new XName[]{
                        namespaces["cac"] + "TenderingProcess",
                        namespaces["cbc"] + "SubmissionMethodCode"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElements.Tender,
                        MappingElements.Tenders.SubmissionMethod
                    })
                },
                {
                    new LinkedList<XName>(new XName[]{
                        namespaces["cac"] + "TenderingTerms",
                        namespaces["cac"] + "Language",
                        namespaces["cbc"] + "ID"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElements.Tender,
                        MappingElements.Tenders.SubmissionMethodDetails
                    })
                },
                {
                    new LinkedList<XName>(new XName[]{
                        namespaces["cac"] + "TenderingProcess",
                        namespaces["cac"] + "AuctionTerms",
                        namespaces["cbc"] + "AuctionConstraintIndicator"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElements.Tender,
                        MappingElements.Tenders.SubmissionMethod
                    })
                },
                {
                   new LinkedList<XName>(new XName[]{
                        namespaces["cac-place-ext"] + "LocatedContractingParty",
                        namespaces["cac"] + "Party",
                        namespaces["cac"] + "PartyName",
                        namespaces["cbc"] + "Name"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElements.Party,
                        MappingElements.Parties.Name
                    }) 
                },
                {
                   new LinkedList<XName>(new XName[]{
                        namespaces["cac-place-ext"] + "LocatedContractingParty",
                        namespaces["cac"] + "Party",
                        namespaces["cac"] + "PartyIdentification",
                        namespaces["cbc"] + "ID"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElements.Party,
                        MappingElements.Parties.Identifier
                    }) 
                },
                {
                   new LinkedList<XName>(new XName[]{
                        namespaces["cac-place-ext"] + "LocatedContractingParty",
                        namespaces["cac"] + "Party"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElements.Party
                    }) 
                },
                {
                    new LinkedList<XName>(new XName[]{
                        namespaces["cac"] + "TenderResult",
                        namespaces["cac"] + "AwardedTenderedProject"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElements.Award,
                        MappingElements.Awards.Id
                    }) 
                },
                {
                    new LinkedList<XName>(new XName[]{
                        namespaces["cac"] + "TenderResult",
                        namespaces["cbc"] + "ResultCode"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElements.Award,
                        MappingElements.Awards.Status
                    }) 
                },
                {
                   new LinkedList<XName>(new XName[]{
                        namespaces["cac"] + "TenderResult",
                        namespaces["cac"] + "AwardedTenderedProject",
                        namespaces["cac"] + "LegalMonetaryTotal"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElements.Award,
                        MappingElements.Awards.Value
                    }) 
                },
                {
                    new LinkedList<XName>(new XName[]{
                        namespaces["cac"] + "TenderResult",
                        namespaces["cbc"] + "ReceivedTenderQuantity"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElements.Tender,
                        MappingElements.Tenders.NumberOfTenderers
                    })
                },
                {
                    new LinkedList<XName>(new XName[]{
                        namespaces["cac"] + "TenderResult",
                        namespaces["cbc"] + "Description"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElements.Award,
                        MappingElements.Awards.Description
                    })
                },
                {
                    new LinkedList<XName>(new XName[]{
                        namespaces["cac"] + "TenderResult",
                        namespaces["cbc"] + "AwardDate"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElements.Award,
                        MappingElements.Awards.Date
                    })
                },
                {
                    new LinkedList<XName>(new XName[]{
                        namespaces["cac"] + "TenderResult"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElements.Award,
                        MappingElements.Awards.Suppliers
                    })
                },
                {
                    new LinkedList<XName>(new XName[]{
                        namespaces["cac"] + "ProcurementProjectLot",
                        namespaces["cbc"] + "ID"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElements.Tender,
                        MappingElements.Tenders.Lot,
                        MappingElements.Tenders.Lots.Id
                    })
                },
                {
                    new LinkedList<XName>(new XName[]{
                        namespaces["cac"] + "ProcurementProjectLot",
                        namespaces["cac"] + "ProcurementProject",
                        namespaces["cbc"] + "Name"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElements.Tender,
                        MappingElements.Tenders.Lot,
                        MappingElements.Tenders.Lots.Name
                    })
                },
                {
                    new LinkedList<XName>(new XName[]{
                        namespaces["cac"] + "ProcurementProjectLot",
                        namespaces["cac"] + "ProcurementProject",
                        namespaces["cac"] + "BudgetAmount",
                        namespaces["cbc"] + "TotalAmount"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElements.Tender,
                        MappingElements.Tenders.Lot,
                        MappingElements.Tenders.Lots.Value_
                    })
                },
                {
                    new LinkedList<XName>(new XName[]{
                        namespaces["cac"] + "ProcurementProjectLot",
                        namespaces["cac"] + "ProcurementProject",
                        namespaces["cac"] + "RequiredCommodityClassification",
                        namespaces["cbc"] + "ItemClassificationCode"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElements.Tender,
                        MappingElements.Tenders.Item,
                        MappingElements.Tenders.Items.Classification
                    })
                },
                {
                    new LinkedList<XName>(new XName[]{
                        namespaces["cac"] + "TenderResult",
                        namespaces["cac"] + "Contract",
                        namespaces["cbc"] + "ID"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElements.Contract,
                        MappingElements.Contracts.Id
                    })
                },
                {
                    new LinkedList<XName>(new XName[]{
                        namespaces["cac"] + "TenderResult",
                        namespaces["cac"] + "StartDate"
                    }),
                    new LinkedList<string>(new string[]{
                        MappingElements.Contract,
                        MappingElements.Contracts.Period,
                        MappingElements.Contracts.Periods.StartDate
                    })
                }
            };

            // Devuelve el diccionario de mapeo
            return mappings;
        }
    }
}
