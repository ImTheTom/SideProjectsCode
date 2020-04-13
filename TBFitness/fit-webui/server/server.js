import path from "path";
import express from "express";

const app = express();
const __dirname = path.resolve();
const publicPath = path.join(__dirname, "..", "public");
const port = 3000;

app.use(express.static(publicPath));

app.get("*", (req, res) => {
	res.sendFile(path.join(publicPath, "index.html"));
});

app.listen(port, () => {
	console.log("Server is up!");
});
