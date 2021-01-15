import axios from "axios";

export default axios.create({
  //baseURL: "https://localhost:5001/api",
  baseURL: "https://chickencoopapp20210115112222.azurewebsites.net/api",
  headers: {
    "Content-type": "application/json"
  }
});
