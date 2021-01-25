const Pool = require('pg').Pool;
const pool = new Pool({
    user: 'postgres',
    host: 'localhost',
    database: 'Node Practise',
    password: 'admin',
    port: '5432',
});

const getCars = (req, resp) => {
    try{
        pool.query('SELECT Car.id, Car."name" , model."Name", make."Name" FROM Car JOIN Model ON Car.modelid = model.id JOIN make ON car.makeid = make.id ')
            .then( data => {
                resp.status(200).json(data.rows);
            })
            .catch( error => {
                resp.status(500).json(error);
            });
    }
    catch(e){
        resp.status(500).json(e);
    }
};

const getCarById = (req, resp) => {
    try{
        pool.query(`SELECT Car.id, Car."name" , model."Name", make."Name" FROM Car JOIN Model ON Car.modelid = model.id JOIN make ON car.makeid = make.id WHERE Car.id = ${req.params.id}`)
            .then( data => {
                resp.status(200).json(data.rows);
            })
            .catch( error => {
                resp.status(500).json(error);
            });
    }
    catch(e){
        resp.status(500).json(e);
    }
};

module.exports = {
    getCars,
    getCarById
}