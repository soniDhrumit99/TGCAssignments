// Requirments
const express = require("express");
const multer = require("multer");
const path = require("path");
const db = require("./db");

// Initializing Express
const app = express();
const port = 3000;

// Configuring Express
app.use(express.json());
app.use(express.urlencoded({ extended: true }));

// Configuring Multer
const storage = multer.diskStorage({
  destination: (req, file, cb) => {
    cb(null, "./uploads");
  },
  filename: (req, file, cb) => {
    let filetype = file.mimetype.replace("image/", "");
    cb(null, "image_" + Date.now() + "." + filetype);
  },
});

const upload = multer({
  storage: storage,
  fileFilter: (req, file, cb) => {
    if (file.mimetype == "image/jpeg" || file.mimetype == "image/png") {
      cb(null, true);
    } else {
      cb(null, false);
    }
  },
});

// Setting routes
app.get("/", (req, resp) => {
  resp.status(200).json({
    status: `Server is up and running on port ${port}`,
  });
});
app.get("/cars", db.getCars);
app.get("/car/:id", db.getCarById);
app.post("/car", db.postCar);
app.post("/upload/:id", upload.single("image"), db.postCarImage);
app.put("/car/:id", db.putCar);
app.delete("/car/:id", db.deleteCar);

// Serving images
app.use("/images", express.static(path.join(__dirname, "uploads")));

// Starting server
app.listen(port, () => {
  console.log(`Server is up and running on port ${port}`);
});
