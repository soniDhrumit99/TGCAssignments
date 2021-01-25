const express = require('express');
const db = require('./db');

// Initializing Express
const app = express();
const port = 3000;

// Setting routes
app.get('/', (req, resp) => {
    resp.status(200).json({
        status: `Server is up and running on port ${port}`
    });
});

app.get('/cars', db.getCars);
app.get('/car/:id', db.getCarById);

// Starting server
app.listen(port, () => {
    console.log(`Server is up and running on port ${port}`);
});