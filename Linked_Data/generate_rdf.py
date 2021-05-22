#########################################################
## generate_rdf.py
##   Script for RDF data generation from JSON sources
##   Stores in ./rdf folder the data generated
##   Uses Node.js module RocketRML to process data
##   Usage: ./python3 generate_rdf.py <dir>
##       <dir> : directory with JSON files to be processed
#########################################################


#########
# Imports
#########

import io
import logging
import json
import os
import pathlib
import re
import subprocess
import sys
import xmltodict


###########
# Functions
###########

def get_json_files(directory:pathlib.Path):
    '''
        function get_json_files() -> dict<str, io.TextIOWrapper>
            @param directory    : directory to search json files into
            @return             : mapping between json filenames and their text streams
    '''

    json_files = {}
    for path in directory.iterdir():
        if path.is_file() and path.suffix == '.json':
            json_files[path.name.replace('.json', '')] = open(path, 'r')
        # END IF
    # END FOR

    return json_files
# END FUNCTION

def json_to_xml(json_file:io.TextIOWrapper):
    '''
        function json_to_xml() -> str
            @param json_file    : json file text stream
            @return             : filename of XML temporary file to be removed
    '''

    json_data = json.loads(json_file.read())
    json_data = { 'root' : json_data }

    xml_data = xmltodict.unparse(json_data, pretty=True)
    xml_data = re.compile(u'[\x00-\x08\x0b-\x1f\x7f-\x84\x86-\x9f\ud800-\udfff\ufdd0-\ufddf\ufffe-\uffff]').sub('', xml_data)

    xml_filename = 'input.xml'
    xml_file = open(xml_filename, 'w+', encoding='utf8', errors='ignore')
    xml_file.write(xml_data)

    json_file.close()
    xml_file.close()

    return xml_filename
# END FUNCTION


############
# Entrypoint
############

if __name__ == '__main__':

    logger = logging.getLogger()
    logging.basicConfig(level=logging.INFO)

    if len(sys.argv) != 2:
        logger.error('Please provide an argument describing input directory')
        logger.info('Usage: python3 generate_rdf.py <dir>')
        exit(1)
    # END IF

    input_directory = sys.argv[1]
    if not os.path.isdir(input_directory):
        logger.error('Please provide a valid input directory')
        logger.info('Usage: python3 generate_rdf.py <dir>')
        exit(1)
    # END IF
    
    json_files = get_json_files(pathlib.Path(input_directory))
    if len(json_files) == 0:
        logger.error('Directory provided didn\'t contain json documents to process')
        exit(2)
    # END IF
    
    for json_filename, json_filecontent in json_files.items():
        xml_filename = json_to_xml(json_filecontent)
        return_code = subprocess.call("node index.js %s" % json_filename, shell=True)

        if return_code == 0:
            logger.info('File %s processed, RDF data generated at %s' % ('%s.json' % json_filename, './rdf/%s.n3' % json_filename))
            os.remove(xml_filename)
        else:
            logger.error('An error occurred while executing Node.js RocketRML application')
            exit(3)
        # END IF-ELSE
    # END FOR
    
    exit(0)
# END MAIN
