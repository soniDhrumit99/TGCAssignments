const mysql = require('mysql');

exports.handler = (event, context, cb) => {
  const conn = mysql.createConnection({
    host: 'customermanagement.cqpug1sw78bk.ap-south-1.rds.amazonaws.com',
    user: 'admin',
    password: 'dhrumitAdmin',
    database: 'customermanagement',
  });
    
  try {
    const q = `SELECT id, name, email, contactnumber, thumbnail, status from Customers WHERE status = true`;
    
    conn.query(q, (error, results, fields) => {
      if (error) {
        conn.destroy();
        throw error;
      } else {
        if(results.length === 0){
          cb(null, {
            "statusCode": 200,
            "headers": {
              "Access-Control-Allow-Origin": "*"
            },
            "body": "There are no customers",
            "isBase64Encoded": false,
          });
          conn.end();
        } else {
          results = results.map((result) => {
            result.thumbnail = 'https://dhrumit-assignment-1.s3.ap-south-1.amazonaws.com/' + result.thumbnail;
            return result;
          });
          cb(null, {
            "statusCode": 200,
            "headers": {
              "Access-Control-Allow-Origin": "*"
            },
            "body": JSON.stringify(results),
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
      "headers": {
        "Access-Control-Allow-Origin": "*"
      },
      "body": "Some error",
      "isBase64Encoded": false
    });
  }
};