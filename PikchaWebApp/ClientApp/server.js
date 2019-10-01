const express = require("express");
const connectDB = require("./config/db");
const cors = require("cors");

var corsOptions = {
  origin: "*",
  methods: "GET,HEAD,PUT,PATCH,POST, DELETE, OPTIONS",
  preflightContinue: true,
  optionsSuccessStatus: 204,
  exposedHeaders: "x-auth-token"
};

const app = express();
app.use(cors(corsOptions));

// Connect Database
connectDB();

// Init Middleware
app.use(express.json({ extended: false }));

app.get("/", (req, res) => {
  res.json({ msg: "Welcome to the backend" });
});

// Define Routes
app.use("/api/users", require("./routes/users"));
app.use("/api/auth", require("./routes/auth"));

const PORT = process.env.PORT || 5000;

app.listen(PORT, () => console.log(`Server started on port ${PORT}`));
