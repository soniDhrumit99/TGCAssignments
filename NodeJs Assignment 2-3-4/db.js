// Requirements
const Pool = require("pg").Pool;

// Connecting to PostgreSQL Database
const pool = new Pool({
  user: "postgres",
  host: "localhost",
  database: "nodePractise",
  password: "admin",
  port: "5432",
});

// GET: /cars
const getCars = async (req, resp) => {
  try {
    const selectResult = await pool.query(
      `SELECT c.id, c.name, mo.name AS "model", mk.name AS "make", 
        (SELECT JSON_AGG(x) AS "images" FROM (SELECT ci.path FROM carimages ci WHERE ci.car_id = c.id) x) 
        FROM cars c LEFT JOIN models mo ON c.model_id = mo.id LEFT JOIN make mk ON c.make_id = mk.id ORDER BY c.id ASC`
    );
    if (selectResult.rowCount < 1) {
      resp.status(200).json({
        result: "There are no cars to show !!",
      });
    } else {
      resp.status(200).json(selectResult.rows);
    }
  } catch (err) {
    resp.status(500).json(err);
  }
};

// GET: /car/:id
const getCarById = async (req, resp) => {
  try {
    const id = checkId(req.params.id);
    if (id != -1) {
      const selectResult = await pool.query(
        `SELECT c.id, c.name, mo.name AS "model", mk.name AS "make", 
          (SELECT JSON_AGG(x) AS "images" FROM (SELECT ci.path FROM carimages ci WHERE ci.car_id = c.id) x) 
          FROM cars c LEFT JOIN models mo ON c.model_id = mo.id LEFT JOIN make mk ON c.make_id = mk.id WHERE c.id = ${id}`
      );
      if (selectResult.rowCount < 1) {
        resp.status(400).json({
          error: "Invalid Car Id",
        });
      } else {
        resp.status(200).json(selectResult.rows[0]);
      }
    }
  } catch (err) {
    resp.status(500).json({
      error: err.message,
    });
  }
};

// POST: /car
const postCar = async (req, resp) => {
  try {
    if (req.body == null || req.body == undefined) {
      // req.body was empty so no data was received
      resp.status(400).json({
        error: "No data to post !!",
      });
    } else {
      const car = req.body;
      checkObject(car);
      await checkName(car.name);
      const model = await checkModel(car.model);
      const make = await checkMake(car.make);
      const insertResult = await pool.query(
        `INSERT INTO cars (name, model_id, make_id) VALUES ('${car.name}', '${model}', '${make}') RETURNING *`
      );
      if (insertResult.rowCount == 1) {
        resp.status(201).json(insertResult.rows[0]);
      } else {
        throw Error("Error in saving the car");
      }
    }
  } catch (err) {
    resp.status(500).json({
      error: err.message,
    });
  }
};

// POST: /car/:id/image
const postCarImage = async (req, resp) => {
  try {
    const id = checkId(req.params.id);
    if (id != -1) {
      const insertResult = await pool.query(
        `INSERT INTO carimages (car_id, path) 
          VALUES (${id}, 'http://localhost:3000/images/${req.file.filename}') RETURNING *`
      );
      if (insertResult.rowCount < 1) {
        throw Error("Error in saving image");
      } else {
        resp.status(201).json(insertResult.rows[0]);
      }
    } else {
      resp.status(400).json({
        error: "Invalid Car Id",
      });
    }
  } catch (err) {
    resp.status(500).json({
      error: err.message,
    });
  }
};

//PUT: /car/:id
const putCar = async (req, resp) => {
  try {
    const id = checkId(req.params.id);
    if (id != -1) {
      if (req.body == null || req.body == undefined) {
        // req.body was empty so no data was received
        resp.status(400).json({
          error: "No data to update !!",
        });
      } else {
        // Retrieve old details
        const oldCar = await fetchCar(id);

        // Get new details from req.body
        const updateDetails = req.body;

        // Check of name is passed. If it is not passed, keep it null.
        const name =
          updateDetails.name == null ||
          updateDetails.name == undefined ||
          updateDetails.name == ""
            ? null
            : updateDetails.name;

        // If it is passed check if it exists or not.
        // If it exists, throw error, else return.
        await checkName(name);

        // Check of model is passed. If it is not passed, keep it null.
        // If it is passed check if it exists or not.
        // It it exists, return id, else make a new entry and return new id.
        const model =
          updateDetails.model == null ||
          updateDetails.model == undefined ||
          updateDetails.model == ""
            ? null
            : await checkModel(updateDetails.model);

        // Check of make is passed. If it is not passed, keep it null.
        // If it is passed check if it exists or not.
        // It it exists, return id, else make a new entry and return new id.
        const make =
          updateDetails.make == null ||
          updateDetails.make == undefined ||
          updateDetails.make == ""
            ? null
            : await checkMake(updateDetails.make);

        // Make a new car object to update
        const newCar = {
          id: oldCar.id,
          name: name == null ? oldCar.name : name,
          model: model == null ? oldCar.model : model,
          make: make == null ? oldCar.make : model,
        };

        const result = await pool.query(
          `UPDATE cars SET name = '${newCar.name}', model_id = ${newCar.model}, make_id = ${newCar.make} WHERE id = ${newCar.id} RETURNING id`
        );
        if (result.rowCount < 1) {
          throw Error("Error in updating car details");
        } else {
          resp.status(200).json({
            success: `Car with id ${result.rows[0].id} updated successfully.`,
          });
        }
      }
    } else {
      resp.status(400).json({
        error: "Invalid Car Id",
      });
    }
  } catch (err) {
    resp.status(500).json({
      error: err.message,
    });
  }
};

// DELETE: /car/:id
const deleteCar = async (req, resp) => {
  try {
    const id = checkId(req.params.id);
    if (id != -1) {
      if (await checkCar(id)) {
        const deleteResult = await pool.query(
          `DELETE FROM cars WHERE cars.id = ${id} RETURNING id`
        );
        if (deleteResult.rowCount < 1) {
          throw Error("Error in deleting the car");
        } else {
          resp.status(200).json({
            result: `Car with id ${deleteResult.rows[0].id} deleted successfully.`,
          });
          return;
        }
      }
    }
    resp.status(400).json({
      error: "Invalid Car Id",
    });
  } catch (err) {
    resp.status(500).json({
      error: err.message,
    });
  }
};

// Helper Functions

// Validating Car Object
const checkObject = (car) => {
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

// Checking if Id passed is integer or not
const checkId = (string) => {
  try {
    const id = parseInt(string);
    if (isNaN(id)) {
      return -1; // Invalid id flag
    } else {
      return id;
    }
  } catch (err) {
    throw Error(err.message);
  }
};

// Checking if Car record exists
const checkCar = async (id) => {
  try {
    const selectResult = await pool.query(
      `SELECT * FROM cars WHERE cars.id = ${id}`
    );
    if (selectResult.rowCount < 1) {
      return false;
    } else {
      return true;
    }
  } catch (err) {
    throw Error(err.message);
  }
};

// Checking if CarName already taken
const checkName = async (name) => {
  try {
    const selectResult = await pool.query(
      `SELECT * FROM cars WHERE cars.name ILIKE '${name}'`
    );
    if (selectResult.rowCount < 1) {
      return;
    } else {
      throw Error("A car with the same name already exists !!!");
    }
  } catch (err) {
    throw Error(err.message);
  }
};

// Checking if Model exists
const checkModel = async (model) => {
  try {
    const selectResult = await pool.query(
      `SELECT id FROM models WHERE models.name ILIKE '${model}'`
    );
    if (selectResult.rowCount < 1) {
      const insertResult = await pool.query(
        `INSERT INTO models (name) VALUES ('${model}') RETURNING id`
      );
      return insertResult.rows[0].id;
    } else {
      return selectResult.rows[0].id;
    }
  } catch (err) {
    throw Error(err.message);
  }
};

// Checking if Make exists
const checkMake = async (make) => {
  try {
    const selectResult = await pool.query(
      `SELECT id FROM make WHERE make.name ILIKE '${make}'`
    );
    if (selectResult.rowCount < 1) {
      const insertResult = await pool.query(
        `INSERT INTO make (name) VALUES ('${make}') RETURNING id`
      );
      return insertResult.rows[0].id;
    } else {
      return selectResult.rows[0].id;
    }
  } catch (err) {
    throw Error(err.message);
  }
};

// Getting car details by id
const fetchCar = async (id) => {
  const result = await pool.query(
    `SELECT c.id AS "id", c.name AS "name", c.model_id AS "model", c.make_id AS "make" 
      FROM cars c WHERE c.id = ${id} 
      `
  );
  if (result.rowCount < 1) {
    throw Error("Invalid Car Id");
  } else {
    return result.rows[0];
  }
};

module.exports = {
  getCars,
  getCarById,
  postCar,
  postCarImage,
  putCar,
  deleteCar,
};
