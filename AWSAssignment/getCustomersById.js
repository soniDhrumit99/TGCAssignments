const mysql = require('mysql');

exports.handler = (event, context, cb) => {
  const conn = mysql.createConnection({
    host: 'customermanagement.cqpug1sw78bk.ap-south-1.rds.amazonaws.com',
    user: 'admin',
    password: 'dhrumitAdmin',
    database: 'customermanagement',
  });
    
  try {
    const id = parseInt(event.pathParameters.id);
    
    const q = `SELECT id, name, email, contactnumber, image, status from Customers WHERE id = ${id} AND status = true`;
    
    conn.query(q, (error, results, fields) => {
      if (error) {
        conn.destroy();
        throw error;
      } else {
        console.log(results);
        if (results.length === 0) {
          cb(null, {
            "statusCode": 400,
            "body": "Customer does not exists",
            "headers": {
              "Access-Control-Allow-Origin": "*",
            },
            "isBase64Encoded": false,
          });
          conn.end();
        } else {
          let customer = results[0];
          customer.image = 'https://dhrumit-assignment-1.s3.ap-south-1.amazonaws.com/' + customer.image;
          cb(null, {
            "statusCode": 200,
            "body": JSON.stringify(customer),
            "headers": {
              "Access-Control-Allow-Origin": "*",
            },
            "isBase64Encoded": false,
          });
          conn.end();
        }
        
      }
    });
  } catch(err) {
    console.log(err.message);
    conn.end();
    cb(null, {
      "statusCode": 500,
      "body": "Some error",
      "headers": {
            "Access-Control-Allow-Origin": "*",
          },
      "isBase64Encoded": false
    });
  }
};