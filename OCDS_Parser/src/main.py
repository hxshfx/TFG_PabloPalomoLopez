'''
    Punto de entrada de la aplicación de parseo
'''

import csv
import logging
import json
import pathlib
from lib import parserlib

RESOURCES_PATH = '%s/resources' % pathlib.Path(__file__).resolve().parent.parent

if __name__ == '__main__':

    logging.basicConfig(level=logging.DEBUG)

    occurrences = []
    elements = json.load(open('%s/json/elements.json' % RESOURCES_PATH, 'r'))

    tree = parserlib.get_first_file()
    while tree is not None:
        occurrences.append(parserlib.get_elements_occurrences_from_tree(tree, elements))
        tree = parserlib.get_next_file(tree)
    # END WHILE

    n_rows = sum([len(elem_list) for elem_list in occurrences])
    logging.info('Parseo terminado, %d filas', n_rows)

    with open('%s/csv/occurrences.csv' % RESOURCES_PATH, 'w') as csv_file:
        elements.append('ID')
        writer = csv.DictWriter(csv_file, fieldnames=elements)
        writer.writeheader()
        for tree_occurrences in occurrences:
            writer.writerows(tree_occurrences)
        # END FOR
        csv_file.close()
        logging.info('Resultados escritos en el fichero %s', csv_file.name)
    # END WITH
# END MAIN
