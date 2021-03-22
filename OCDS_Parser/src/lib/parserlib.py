'''
    Fichero de funcionalidades de parseo de los documentos de Contratación Pública del Estado,
    disponibles en la URL https://www.hacienda.gob.es/es-ES/GobiernoAbierto/Datos%20Abiertos/Paginas/licitaciones_plataforma_contratacion.aspx
'''

# Imports

import logging
import main
from lxml import etree


# Constants

FIRST_FILE = 'licitacionesPerfilesContratanteCompleto3_02.atom'


# Funcionalidades del parseo

def get_first_file() -> etree.ElementTree:
    '''Función get_first_file => Devuelve el primer fichero XML'''

    logging.info('Parseando el primer fichero XML')
    with open('%s/xml/%s' % (main.RESOURCES_PATH, FIRST_FILE), 'r') as first_file:
        tree = etree.parse(first_file)
        return tree
    # END WITH
# END FUNCTION


def get_next_file(tree:etree.ElementTree) -> etree.ElementTree:
    '''Función get_next_file => Devuelve el siguiente fichero XML enlazado al previo'''

    root = tree.getroot()
    prefix_map = _get_valid_namespaces(root)

    links = root.findall('.//atom:link', prefix_map)
    try:
        for link in links:
            if 'rel' in link.attrib and 'next' in link.attrib['rel']:
                with open('%s/xml/%s' %  (main.RESOURCES_PATH, link.attrib['href']), 'r') as next_file:
                    tree = etree.parse(next_file)
                    return tree
                # END WITH
            # END IF
        # END FOR
    except FileNotFoundError as ex:
        file_not_found_id = ex.filename.split('/')[-1]
        logging.warning('Enlace sin resolver: %s', file_not_found_id)
        return None
    # END TRY-EXCEPT

    return None
# END FUNCTION


def get_elements_from_tree(tree:etree.ElementTree) -> list:
    '''Función get_elements_from_tree => Devuelve la lista ordenada de elementos presentes en el fichero XML'''

    elem_list = []

    root = tree.getroot()
    prefix_map = _get_valid_namespaces(root)

    entry_set = root.findall('.//atom:entry', prefix_map)
    for entry in entry_set:
        elem_list = _get_elements_from_entry(entry, elem_list)
    # END FOR

    return _sort_and_rename(elem_list, prefix_map)
# END FUNCTION


def get_elements_occurrences_from_tree(tree:etree.ElementTree, elem_list:list) -> list:
    '''Función get_elements_occurrences_from_tree => Devuelve un diccionario con las ocurrencias de los elementos presentes en el fichero XML'''

    occurrences = []

    root = tree.getroot()
    prefix_map = _get_valid_namespaces(root)

    file_id = root.find('.//atom:id',prefix_map).base.split('/')[-1]
    logging.debug('Parseando el XML %s', file_id)

    entry_set = root.findall('.//atom:entry', prefix_map)
    for entry in entry_set:
        occurrences.append(_get_elements_occurrences_from_entry(entry, elem_list, prefix_map))
    # END FOR

    return occurrences
# END FUNCTION


# Funciones protegidas

def _get_valid_namespaces(root:etree.Element) -> dict:
    '''Función _get_valid_namespaces => Devuelve el diccionario de espacios de nombres sustituyendo None por atom'''

    prefix_map = root.nsmap
    prefix_map['atom'] = prefix_map.pop(None)

    return prefix_map
# END FUNCTION


def _get_elements_from_entry(entry:etree.Element, elem_list:list) -> list:
    '''Función _get_elements_from_entry => Devuelve una lista con los elementos presentes en la entrada'''

    for element in entry.iter():
        if element.tag not in elem_list:
            elem_list.append(element.tag)
        # END IF
    # END FOR

    return elem_list
# END FUNCTION


def _get_elements_occurrences_from_entry(entry:etree.Element, elem_list:list, prefix_map:dict) -> dict:
    '''Función _get_elements_from_entry => Devuelve una lista con las ocurrencias de los elementos presentes en la entrada'''

    elem_dict = {elem : 0 for elem in elem_list}
    elem_dict['ID'] = entry.find('.//atom:id', prefix_map).text

    for elem in entry.iter():
        elem = _rename(elem.tag, prefix_map)
        elem_dict[elem] += 1
    # END FOR

    assert len(elem_dict) == 140

    return elem_dict
# END FUNCTION


def _sort_and_rename(elem_list:list, prefix_map:dict) -> list:
    '''Función _sort_and_rename => Devuelve una lista ordenada alfabéticamente con los elementos renombrados sustituyendo el namespace por su alias'''

    renamed_list = []

    for elem in elem_list:
        renamed_list.append(_rename(elem, prefix_map))
    # END FOR

    return sorted(renamed_list)
# END FUNCTION


def _rename(elem:str, prefix_map:dict) -> str:
    '''Función _rename => Devuelve un string con el reemplazo de namespace por alias'''

    for key, value in prefix_map.items():
        if elem.find(value) > 0:
            elem = elem.replace(value, '%s:' % key)
            elem = elem.replace('{', '').replace('}', '')
            return elem
        # END IF
    # END FOR

    return ''
# END FUNCTION
