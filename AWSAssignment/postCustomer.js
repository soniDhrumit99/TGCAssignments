// Dependency Imports
const { S3 }= require('aws-sdk');
const s3 = new S3();
const multipart = require("lambda-multipart-parser");
const mysql = require("mysql");
const bcrypt = require('bcryptjs');
const sharp = require('sharp');

// Function to encrypt password using bcrypt
const encryptPassword = (plainTextPassword) => bcrypt.hashSync(plainTextPassword, 8);

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

// Function to upload image to S3 Bucket
const uploadImage = (path, file) =>
  new Promise((resolve, reject) => {
    let params = {
      ACL: "public-read",
      Bucket: "dhrumit-assignment-1",
      Key: path,
      Body: file,
      ContentType: "image/png",
    };

    s3.upload(params)
      .promise()
      .then((data) => {
        resolve(true);
      })
      .catch((err) => {
        reject(err);
      });
  });

// Function to insert data into database
const insertData = (name, email, password, contact, thumbnail, image) => new Promise((resolve, reject) => {
    try {
        const conn = mysql.createConnection({
          host: "customermanagement.cqpug1sw78bk.ap-south-1.rds.amazonaws.com",
          user: "admin",
          password: "dhrumitAdmin",
          database: "customermanagement",
        });
        const insertQuery = `INSERT INTO Customers (name, email, password, contactnumber, thumbnail, status, image)
            VALUES ('${name}', '${email}', '${password}', '${contact}', '${thumbnail}', ${true}, '${image}')`;
        conn.query(insertQuery, (err, result, fields) => {
            if(err) {
                conn.destroy();
                reject(err);
            } else {
                conn.end();
                resolve(true);
            }
        });
    } catch(err) {
       reject(err);
    }

});

exports.handler = async (event, context, callback) => {
  try {

    // Converting multipart data to JSON
    let req = await multipart.parse(event);

    // Extracting fields for database insertion
    let username = req.name.split(" ").join("").toLowerCase();
    let name = req.name;
    let email = req.email;
    let password = encryptPassword(req.password);
    let contact = req.contact;

    // Making filename for saving image in S3
    let timestamp = new Date().toISOString().replace(/-/g, '').replace('T', '').replace(/:/g, '').replace('.', '').replace('Z', '');

    let file = req.files[0];
    let fileExt = ".png";
    let imagePath = "images/" + username + "_" + timestamp + fileExt;
    let thumbnailPath = "thumbnails/" + username + "_" + timestamp + fileExt;
    let decodedImage = await sharp(Buffer.from(file.content, "base64")).png().toBuffer();
    let decodedThumbnail = Buffer.from(file.content, "base64");
    let resizedThumbnail = await sharp(decodedThumbnail).resize(200).png().toBuffer();


    // Uploading image and thumbnail to S3 asynchronously
    let promises = [];
    promises.push(insertData(name, email, password, contact, thumbnailPath, imagePath));
    promises.push(uploadImage(imagePath, decodedImage));
    promises.push(uploadImage(thumbnailPath, resizedThumbnail));

    await Promise.all(promises)
      .then((values) => {
        if (values[0] && values[1] && values[2]) {
          callback(null, {
            statusCode: 201,
            body: "Created",
            headers: {
              "Access-Control-Allow-Origin": '*',
            },
            isBase64Encoded: false,
          });
        } else {
          returnError(new Error("Some error"), callback);
        }
      })
      .catch((err) => {
        returnError(err, callback);
      });
  } catch (err) {
    returnError(err, callback);
  }
};
