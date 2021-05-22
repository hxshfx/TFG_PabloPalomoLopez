##############################################################
## sparql.py
##   Script for SPARQL queries' battery execution
##   Stores in ./queries folder the data extracted from queries
##   Usage: ./python3 sparql.py <dir>
##       <dir> : directory with RDF data to be queried
##############################################################


#########
# Imports
#########

import io
import logging
import os
import pathlib
import rdflib
import rdflib.plugins.sparql as sparql
import sys


###################
# Data initializing
###################

graph = rdflib.Graph()

DC = rdflib.Namespace('http://purl.org/dc/terms/')
OCDS = rdflib.Namespace('http://data.tbfy.eu/ontology/ocds#')
OCDSEXT = rdflib.Namespace('http://data.tbfy.eu/ontology/ocds_extension#')
ORG = rdflib.Namespace('http://www.w3.org/ns/org#')
SCHEMA = rdflib.Namespace('http://schema.org/')
SKOS = rdflib.Namespace('http://www.w3.org/2004/02/skos/core#')
TBFY = rdflib.Namespace('http://data.tbfy.eu/ontology/tbfy#')

logger = logging.getLogger('generate_rdf')
logging.basicConfig(level=logging.INFO)


#########
# Queries
#########

q1 = sparql.prepareQuery('''
    SELECT ?date ?id ?tag
    WHERE {
            ?release a tbfy:Release .
            ?release tbfy:releaseDate ?date .
            ?release tbfy:releaseId ?id .
            ?release tbfy:releaseTag ?tag .
    }''',
    initNs = { 'tbfy' : TBFY }
)

q2 = sparql.prepareQuery('''
    SELECT ?ocid ?award ?contract ?plan ?release ?tender
    WHERE {
            ?contractingProcess a ocds:ContractingProcess .
            ?contractingProcess ocds:ocid ?ocid .
            OPTIONAL {
                ?contractingProcess ocds:hasAward ?award 
            } .
            OPTIONAL {
                ?contractingProcess ocds:hasContract ?contract
            } .
            OPTIONAL {
                ?contractingProcess ocds:hasPlan ?plan
            } .
            OPTIONAL {
                ?contractingProcess ocds:hasRelease ?release
            } .
            OPTIONAL {
                ?contractingProcess ocds:hasTender ?tender
            } .
    }''',
    initNs = { 'ocds' : OCDS, 'tbfy' : TBFY }
)

q3 = sparql.prepareQuery('''
    SELECT ?awardId ?tender ?awardDate ?awardStatus ?awardDescription ?awardAmount ?awardCurrency ?awardSupplier ?supplierName ?supplierId
    WHERE {
        ?contractingProcess a ocds:ContractingProcess .
        ?contractingProcess ocds:hasAward ?award .
        ?award ocds:awardId ?awardId .
        ?award ocds:isIssuedForTender ?tender .
        ?award ocds:awardDate ?awardDate .
        ?award ocds:awardStatus ?awardStatus .
        OPTIONAL {
            ?award dc:description ?awardDescription
        } .
        OPTIONAL {
            ?award ocds:hasAwardValue ?awardValue
            OPTIONAL {
                ?awardValue ocds:valueAmount ?awardAmount .
                ?awardValue ocds:valueCurrency ?awardCurrency .
            } .
        } .
        OPTIONAL {
            ?awardSupplier ocds:isSupplierFor ?award
            OPTIONAL {
                OPTIONAL {
                    ?awardSupplier ocds:legalName ?supplierName
                } .
                OPTIONAL {
                    ?awardSupplier org:identifier ?supplierId
                } .
            } .
        } .
    }
    ''',
    initNs = { 'dc' : DC, 'ocds' : OCDS, 'org' : ORG }
)

q4 = sparql.prepareQuery('''
    SELECT ?contractId ?award ?contractStartDate ?contractEndDate
    WHERE {
        ?contractingProcess a ocds:ContractingProcess .
        ?contractingProcess ocds:hasContract ?contract .
        ?contract ocds:contractId ?contractId .
        ?contract ocds:isIssuedForAward ?award .
        OPTIONAL {
            ?contract ocds:hasContractPeriod ?contractPeriod
            OPTIONAL {
                OPTIONAL {
                    ?contractPeriod ocds:periodStartDate ?contractStartDate
                } .
                OPTIONAL {
                    ?contractPeriod ocds:periodEndDate ?contractEndDate
                } .
            } .
        } .
    }
    ''',
    initNs = { 'ocds' : OCDS }
)

q5 = sparql.prepareQuery('''
    SELECT ?plan ?planBudgetAmount ?planBudgetCurrency
    WHERE {
        ?contractingProcess a ocds:ContractingProcess .
        ?contractingProcess ocds:hasPlan ?plan .
        ?plan ocds:hasBudget ?planBudget .
        ?planBudget ocds:budgetId ?planBudgetId .
        ?planBudget ocds:hasReservedValue ?planBudgetValue .
        ?planBudgetValue ocds:valueAmount ?planBudgetAmount .
        ?planBudgetValue ocds:valueCurrency ?planBudgetCurrency .
    }
    ''',
    initNs = { 'ocds' : OCDS }
)

q6 = sparql.prepareQuery('''
    SELECT ?tenderId ?tenderTitle ?tenderProcCategory ?tenderProcMethod ?tenderProcMethodDetails ?tenderSubMethod ?tenderSubMethodDetails ?tenderTenderers ?tenderValue ?tenderAmount ?tenderCurrency ?tenderPeriod ?tenderPeriodStartDate ?tenderPeriodEndDate ?tenderPeriodDurationInDays
    WHERE {
        ?contractingProcess a ocds:ContractingProcess .
        ?contractingProcess ocds:hasTender ?tender .
        ?tender ocds:tenderId ?tenderId .
        ?tender dc:title ?tenderTitle .
        OPTIONAL {
            ?tender ocds:mainProcurementCategory ?tenderProcCategory
        } .
        OPTIONAL {
            ?tender ocds:procurementMethod ?tenderProcMethod
        } .
        OPTIONAL {
            ?tender ocds:procurementMethodDetails ?tenderProcMethodDetails
        } .
        OPTIONAL {
            ?tender ocds:submissionMethod ?tenderSubMethod
        } .
        OPTIONAL {
            ?tender ocds:submissionMethodDetails ?tenderSubMethodDetails
        } .
        OPTIONAL {
            ?tender ocds:numberOfTenderers ?tenderTenderers
        } .
        OPTIONAL {
            ?tender ocds:hasMaxEstimatedValue ?tenderValue .
            ?tenderValue ocds:valueAmount ?tenderAmount .
            ?tenderValue ocds:valueCurrency ?tenderCurrency .
        } .
        OPTIONAL {
            ?tender ocds:hasTenderPeriod ?tenderPeriod
            OPTIONAL {
                OPTIONAL {
                    ?tenderPeriod ocds:periodStartDate ?tenderPeriodStartDate
                } .
                OPTIONAL {
                    ?tenderPeriod ocds:periodEndDate ?tenderPeriodEndDate
                } .
                OPTIONAL {
                    ?tenderPeriod ocds:periodDurationInDays ?tenderPeriodDurationInDays
                } .
            } .
        } .
    }
    ''',
    initNs = { 'dc' : DC, 'ocds' : OCDS }
)

q7 = sparql.prepareQuery('''
    SELECT ?tenderId ?tenderItem ?tenderItemClassification ?tenderItemScheme ?tenderItemDescription ?tenderLot ?tenderLotTitle ?tenderLotValue ?tenderLotAmount ?tenderLotCurrency
    WHERE {
        ?contractingProcess a ocds:ContractingProcess .
        ?contractingProcess ocds:hasTender ?tender .
        ?tender ocds:tenderId ?tenderId .
        ?tender ocds:hasItemToBeProcured ?tenderItem .
        ?tenderItem ocds:hasClassification ?tenderItemClassification .
        ?tenderItem ocds:classificationScheme ?tenderItemScheme .
        ?tenderItem dc:description ?tenderItemDescription .
        ?tenderItem ocdsext:relatedLot ?tenderLot .
        ?tenderLot ocdsext:lotId ?tenderLotId .
        ?tenderLot dc:title ?tenderLotTitle .
        OPTIONAL {
            ?tenderLot ocdsext:hasLotValue ?tenderLotValue .
            ?tenderLotValue ocds:valueAmount ?tenderLotAmount .
            ?tenderLotValue ocds:valueCurrency ?tenderLotCurrency .
        } .
    }
    ''',
    initNs = { 'dc': DC, 'ocds' : OCDS, 'ocdsext' : OCDSEXT }
)

q8 = sparql.prepareQuery('''
    SELECT ?party ?partyName ?partyId ?partyIdScheme ?partyAddress ?partyAddressCountry ?partyAddressLocality ?partyAddressPostalCode ?partyAddressStreet ?partyContact ?partyContactEmail ?partyContactName ?partyContactFax ?partyContactTelephone ?partyContactUrl
    WHERE {
        ?contractingProcess a ocds:ContractingProcess .
        ?party ocds:playsRoleIn ?contractingProcess .
        ?party ocds:legalName ?partyName .
        ?party org:identifier ?partyId .
        OPTIONAL {
            ?party skos:notation ?partyIdScheme .
        } .
        OPTIONAL {
            ?party ocds:hasAddress ?partyAddress .
            OPTIONAL {
                ?partyAddress schema:addressCountry ?partyAddressCountry
            } .
            OPTIONAL {
                ?partyAddress schema:addressLocality ?partyAddressLocality
            } .
            OPTIONAL {
                ?partyAddress schema:postalCode ?partyAddressPostalCode
            } .
            OPTIONAL {
                ?partyAddress schema:streetAddress ?partyAddressStreet
            } .
        } .
        OPTIONAL {
            ?party ocds:hasContactPoint ?partyContact .
            OPTIONAL {
                ?partyContact schema:email ?partyContactEmail
            } .
            OPTIONAL {
                ?partyContact schema:name ?partyContactName
            } .
            OPTIONAL {
                ?partyContact schema:faxNumber ?partyContactFax
            } .
            OPTIONAL {
                ?partyContact schema:telephone ?partyContactTelephone
            } .
            OPTIONAL {
                ?partyContact schema:URL ?partyContactUrl
            } .
        } .
    }
    ''',
    initNs = { 'dc' : DC, 'ocds' : OCDS, 'org' : ORG, 'schema' : SCHEMA , 'skos' : SKOS }
)


#################
# Query execution
#################

def execute_query(index:int, output_file:io.TextIOWrapper, query:sparql.parser.Query):
    '''
        function execute_query() -> None
            @param index        : index of query being processed
            @param output_file  : file to store query results into
            @param query        : query object to execute
    '''

    rows = graph.query(query)

    if index == 1:      # q1
        logger.info('Executing releases query')
        for row in rows:
            output_file.write('\nReleaseID: %s, ReleaseDate: %s, ReleaseTag: %s' % (row.id.toPython(), row.date.toPython(), row.tag.toPython()))
        # END FOR
    elif index == 2:    # q2
        logger.info('Executing contracting processes query')
        for row in rows:
            output_file.write('\nResources of contracting process with OCID %s:' % row.ocid.toPython())
            if row.award is not None:
                output_file.write('\tAward: %s' % row.award.toPython())
            if row.contract is not None:
                output_file.write('\tContract: %s' % row.contract.toPython())
            if row.plan is not None:
                output_file.write('\tPlan: %s' % row.plan.toPython())
            if row.release is not None:
                output_file.write('\tRelease: %s' % row.release.toPython())
            if row.tender is not None:
                output_file.write('\tTender: %s' % row.tender.toPython())
            # END IF CLAUSES
        # END FOR
    elif index == 3:    # q3
        logger.info('Executing awards query')
        for row in rows:
            output_file.write('\nAwardID: %s, AwardDate: %s, AwardStatus: %s, TenderRelated: %s'
                % (row.awardId.toPython(), row.awardDate.toPython(), row.awardStatus.toPython(), row.tender.toPython()))
            if row.awardDescription is not None:
                output_file.write('\tDescription: %s' % row.awardDescription.toPython().replace('\n','\n\t\t'))
            if row.awardAmount is not None and row.awardCurrency is not None:
                output_file.write('\tAwardAmount: %s, AwardCurrency: %s' % (row.awardAmount.toPython(), row.awardCurrency.toPython()))
            if row.awardSupplier is not None:
                output_file.write('\tAwardSupplier: %s' % row.awardSupplier.toPython())
                if row.supplierName is not None:
                    output_file.write('\t\tSupplierName: %s' % row.supplierName.toPython())
                if row.supplierId is not None:
                    output_file.write('\t\tSupplierId: %s' % row.supplierId.toPython())
                # END IF INNER CLAUSES
            # END IF OUTER CLAUSES
        # END FOR
    elif index == 4:    # q4
        logger.info('Executing contracts query')
        for row in rows:
            output_file.write('\nContractID: %s, AwardRelated: %s' % (row.contractId.toPython(), row.award.toPython()))
            if row.contractStartDate is not None:
                output_file.write('\tContractStartDate: %s' % row.contractStartDate.toPython())
            if row.contractEndDate is not None:
                output_file.write('\tContractEndDate: %s' % row.contractEndDate.toPython())
            # END IF CLAUSES
        # END FOR
    elif index == 5:    # q5
        logger.info('Executing plannings query')
        for row in rows:
            output_file.write('\nPlanResource: %s,\n\tPlanBudgetAmount: %s, PlanBudgetCurrency: %s' % 
                (row.plan.toPython(), row.planBudgetAmount.toPython(), row.planBudgetCurrency.toPython()))
        # END FOR
    elif index == 6:    # q6
        logger.info('Executing tenders query')
        for row in rows:
            output_file.write('\nTenderID: %s,\n\tTenderTitle: %s' % (row.tenderId.toPython(), row.tenderTitle.toPython()))
            if row.tenderProcCategory is not None:
                output_file.write('\tProcurementCategory: %s' % row.tenderProcCategory.toPython())
            if row.tenderProcMethod is not None:
                output_file.write('\tProcurementMethod: %s' % row.tenderProcMethod.toPython())
            if row.tenderProcMethodDetails is not None:
                output_file.write('\tProcurementMethodDetails: %s' % row.tenderProcMethodDetails.toPython())
            if row.tenderSubMethod is not None:
                output_file.write('\tSubmissionMethod: %s' % row.tenderSubMethod.toPython())
            if row.tenderSubMethodDetails is not None:
                output_file.write('\tSubmissionMethodDetails: %s' % row.tenderSubMethodDetails.toPython())
            if row.tenderTenderers is not None:
                output_file.write('\tNumberOfTenderers: %s' % row.tenderTenderers.toPython())
            if row.tenderValue is not None:
                output_file.write('\tTenderAmount: %s, TenderCurrency: %s' % (row.tenderAmount.toPython(), row.tenderCurrency.toPython()))
            if row.tenderPeriod is not None:
                if row.tenderPeriodStartDate is not None:
                    output_file.write('\tTenderStartDate: %s' % row.tenderPeriodStartDate.toPython())
                if row.tenderPeriodEndDate is not None:
                    output_file.write('\tTenderEndDate: %s' % row.tenderPeriodEndDate.toPython())
                if row.tenderPeriodDurationInDays is not None:
                    output_file.write('\tTenderDurationInDays: %s' % row.tenderPeriodDurationInDays.toPython())
                # END IF INNER CLAUSES
            # END IF OUTER CLAUSES
        # END FOR
    elif index == 7:    # q7
        logger.info('Executing tender items and lots query')
        for row in rows:
            output_file.write('\nTenderID: %s,\n\tTenderedItemResource: %s\n\t\tTenderedItemClassification: %s-%s (%s)'
                % (row.tenderId.toPython(), row.tenderItem.toPython(), row.tenderItemScheme.toPython(), row.tenderItemClassification.toPython(), row.tenderItemDescription.toPython()))
            output_file.write('\tTenderedRelatedLot: %s, TenderedLotTitle: %s' % (row.tenderLot.toPython(), row.tenderLotTitle.toPython()))
            if row.tenderLotValue is not None:
                output_file.write('\t\tTenderedLotAmount: %s, TenderedLotCurrency: %s' % (row.tenderLotAmount.toPython(), row.tenderLotCurrency.toPython()))
            # END IF
        # END FOR
    elif index == 8:    # q8
        logger.info('Executing parties query')
        for row in rows:
            if row.partyIdScheme is not None:
                output_file.write('\nPartyResource: %s,\n\tPartyName: %s, PartyId: %s (%s)'
                    % (row.party.toPython(), row.partyName.toPython(), row.partyId.toPython(), row.partyIdScheme.toPython()))
            else:
                output_file.write('\nPartyResource: %s,\n\tPartyName: %s, PartyId: %s'
                    % (row.party.toPython(), row.partyName.toPython(), row.partyId.toPython()))
            # END IF-ELSE
            if row.partyAddress is not None:
                if row.partyAddressCountry is not None:
                    output_file.write('\tAddressCountry: %s' % row.partyAddressCountry.toPython())
                if row.partyAddressLocality is not None:
                    output_file.write('\tAddressLocality: %s' % row.partyAddressLocality.toPython())
                if row.partyAddressPostalCode is not None:
                    output_file.write('\tAddressPostalCode: %s' % row.partyAddressPostalCode.toPython())
                if row.partyAddressStreet is not None:
                    output_file.write('\tAddressStreet: %s' % row.partyAddressStreet.toPython())
                # END IF ADDRESS INNER CLAUSES
            # END IF
            if row.partyContact is not None:
                if row.partyContactEmail is not None:
                    output_file.write('\tContactEmail: %s' % row.partyContactEmail.toPython())
                if row.partyContactName is not None:
                    output_file.write('\tContactName: %s' % row.partyContactName.toPython())
                if row.partyContactFax is not None:
                    output_file.write('\tContactFax: %s' % row.partyContactFax.toPython())
                if row.partyContactTelephone is not None:
                    output_file.write('\tContactTelephone: %s' % row.partyContactTelephone.toPython())
                if row.partyContactUrl is not None:
                    output_file.write('\tContactUrl: %s' % row.partyContactUrl.toPython())
                # END IF PARTY INNER CLAUSES
            # END IF
        # END FOR
    # END IF-ELIF
# END FUNCTION


############
# Entrypoint
############

if __name__ == '__main__':

    if len(sys.argv) != 2:
        logger.error('Please provide an argument describing input directory')
        logger.info('Usage: python3 sparql.py <dir>')
        exit(1)
    # END IF

    input_directory = sys.argv[1]
    if not os.path.isdir(input_directory):
        logger.error('Please provide a valid input directory')
        logger.info('Usage: python3 sparql.py <dir>')
        exit(1)
    # END IF

    logger.info('RDF data loading')
    for path in pathlib.Path(input_directory).iterdir():
        if path.is_file() and path.suffix == '.n3':
            file = open(path, 'r')
            graph.parse(file, format = 'n3')
            file.close()
            logger.info('Merged RDF data (%s) into process graph' % file.name)
        # END IF
    # END FOR

    output_files = [
        open('./queries/1-releases.txt', 'w'),
        open('./queries/2-contractingProcesses.txt', 'w'),
        open('./queries/3-awards.txt', 'w'),
        open('./queries/4-contracts.txt', 'w'),
        open('./queries/5-plannings.txt', 'w'),
        open('./queries/6-tenders.txt', 'w'),
        open('./queries/7-tendersItemsLots.txt', 'w'),
        open('./queries/8-parties.txt', 'w')
    ]

    queries = [q1, q2, q3, q4, q5, q6, q7, q8]

    for index, (output_file, query) in enumerate(zip(output_files, queries)):
        execute_query(index + 1, output_file, query)
        output_file.close()
    # END FOR
    
    logger.info('Queries execution finished')
    exit(0)
# END MAIN
