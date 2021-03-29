// Dependency Imports
const { S3 } = require('aws-sdk');
const s3 = new S3();
const multipart = require("lambda-multipart-parser");
const mysql = require("mysql");


// Function to return error response
const returnError = (err, callback) => {
  console.log("Error: ", err.message);
  return callback(
    null,
    {
      statusCode: 400,
      body: "" + err.message,
      headers: {
        "Access-Control-Allow-Origin": '*',
      },
      isBase64Encoded: false,
    }
  );
};


// Function to insert data into database
const insertData = (id, name, email, contact, status) => new Promise((resolve, reject) => {
    try {
        const conn = mysql.createConnection({
          host: "customermanagement.cqpug1sw78bk.ap-south-1.rds.amazonaws.com",
          user: "admin",
          password: "dhrumitAdmin",
          database: "customermanagement",
        });
        const insertQuery = `UPDATE Customers SET name='${name}', email='${email}', contactnumber='${contact}', status=${status} WHERE id=${id}`;
        console.log(insertQuery);
        conn.query(insertQuery, (err, result, fields) => {
            console.log(err);
            console.log(result);
            if(err) {
                conn.destroy();
                reject(err);
            } else {
                conn.end();
                resolve(true);
            }
        });
    } catch(err) {
        console.log(err);
       reject(err);
    }

});

exports.handler = async (event, context, callback) => {
  try {

    // Converting multipart data to JSON
    let req = await multipart.parse(event);

    // Extracting fields for database insertion
    let id = event.pathParameters.id;
    let name = req.name;
    let email = req.email;
    let contact = req.contact;
    let status = req.status;
    
    await insertData(id, name, email, contact, status)
       .then((resp) => {
           console.log(resp);
        callback(null, {
            statusCode: 200,
            body: "Updated",
            headers: {
              "Access-Control-Allow-Origin": '*',
            },
            isBase64Encoded: false,
        });
      })
      .catch((err) => {
        returnError(err, callback);
      });

  } catch (err) {
    returnError(err, callback);
  }
};
