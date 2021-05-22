/*  
 *  index.js
 *  Node.js script for RDF data generation
 *  Requires RocketRML module
 */

const parser = require(`rocketrml`);

const doMapping = async () => {
  const options = {
    toRDF: true,
    verbose: false,
    xmlPerformanceMode: false,
    replace: false,
  };
  await parser.parseFile(`./rml_mappings.ttl`, `./rdf/${process.argv[2]}.n3`, options).catch((err) => { console.log(err); });
};

doMapping();