// Connecting to PostgreSQL Database
const Pool = require("pg").Pool;
const pool = new Pool({
  user: "postgres",
  host: "localhost",
  database: "nodePractise",
  password: "admin",
  port: "5432",
});

// API functions for Routes

// GET: /cars
const getCars = (req, resp) => {
  try {
    pool
      .query(
        `
          SELECT c.id AS "Id", c.name AS "Name", mo.name AS "Model", mk.name AS "Make" 
              FROM cars c LEFT JOIN models mo ON c.model_id = mo.id 
              LEFT JOIN make mk on c.make_id = mk.id ORDER BY c.id ASC
        `
      )
      .then((result) => {
        resp.status(200).json(result.rows);
      })
      .catch((err) => {
        resp.status(500).json(err);
      });
  } catch (err) {
    resp.status(500).json(err);
  }
};

// GET: /car/:id
const getCarById = (req, resp) => {
  try {
    const id = parseInt(req.params.id);
    if (isNaN(id)) {
      resp.status(400).json({
        error: "Invalid Car Id",
      });
    } else {
      pool
        .query(
          `
            SELECT c.id AS "Id", c.name AS "Name", mo.name AS "Model", mk.name AS "Make" 
                FROM cars c LEFT JOIN models mo ON c.model_id = mo.id 
                LEFT JOIN make mk on c.make_id = mk.id WHERE c.id = ${id} ORDER BY c.id ASC
          `
        )
        .then((result) => {
          if (result.rows.length == 0) {
            resp.status(400).json({
              error: "Invalid Car Id",
            });
          } else {
            resp.status(200).json(result.rows[0]);
          }
        })
        .catch((err) => {
          resp.status(500).json(err);
        });
    }
  } catch (err) {
    resp.status(500).json(err);
  }
};

// POST: /car
const postCar = (req, resp) => {
  try {
    if (req.body == null || req.body == undefined) {
      // req.body was empty so no data was received
      resp.status(400).json({
        error: "No data to post !!",
      });
    } else {
      const car = req.body;
      validateCar(car);
      const model = checkModel(car);
      checkMake(car);
    }
  } catch (err) {
    resp.status(500).json({
      error: err.message,
    });
  }
};

// Validating Car Object
const validateCar = (car) => {
  if (
    car != null &&
    car.name != null &&
    car.name != "" &&
    car.model != null &&
    car.model != "" &&
    car.make != null &&
    car.make != ""
  ) {
    return;
  }
  throw Error("Invalid Car Details");
};

// Checking if Model exists
const checkModel = async (car) => {
  try {
    const result = await pool.query(
      `SELECT id FROM models WHERE models.name ILIKE '${car.model}'`
    );
    if (result.rowCount < 1) {
      const newModel = await pool.query(
        `INSERT INTO models (name) VALUES ('${car.model}')`
      );
      return newModel;
    } else {
      return result.rows[0];
    }
  } catch (err) {
    resp.status(500).json({
      error: err.message,
    });
  }
};

// Checking if Make exists
const checkMake = (car) => {
  console.log(car.make);
};

module.exports = {
  getCars,
  getCarById,
  postCar,
};
